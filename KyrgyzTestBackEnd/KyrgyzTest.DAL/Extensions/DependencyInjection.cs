using KyrgyzTest.Core.Models.Users;
using KyrgyzTest.Application.Abstractions;
using KyrgyzTest.Application.Abstractions.MeilisearchAbstractions;
using KyrgyzTest.Application.Services;
using KyrgyzTest.Core.Abstractions;
using KyrgyzTest.Core.Models.CertificateRecords;
using KyrgyzTest.DAL.Models.CertificateRecords;
using KyrgyzTest.DAL.Models.Users;
using KyrgyzTest.DAL.Seeds;
using KyrgyzTest.DAL.Services;
using KyrgyzTest.DAL.Services.Meilisearchs;
using Meilisearch;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace KyrgyzTest.DAL.Extensions;

public static class DependencyInjection
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var meilisearchUrl = configuration["Meilisearch:Url"]
                             ?? throw new InvalidOperationException("Meilisearch:Url is not configured.");
        var meilisearchApiKey = configuration["Meilisearch:ApiKey"];

        services.AddSingleton(_ => string.IsNullOrWhiteSpace(meilisearchApiKey)
            ? new MeilisearchClient(meilisearchUrl)
            : new MeilisearchClient(meilisearchUrl, meilisearchApiKey));

        //register sql server and run existing migrations
        services.AddDbContext<AppDbContext>(optionsBuilder =>
        {
            optionsBuilder.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection"),
                sqlOptions => sqlOptions.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)
            );
        });

        //registering services in di
        services.InitRepositories();
    }
    private static void InitRepositories(this IServiceCollection services)
    {
         // services.AddScoped<IRepository, Repository>;
         services.AddScoped<IUserRepository, UserRepository>();
         services.AddScoped<ICertificateRecordRepository, CertificateRecordRepository>();
         services.AddScoped<IUnitOfWork, UnitOfWork>();
         services.AddTransient<IPasswordService, PasswordService>();
         services.AddScoped<IDatabaseInitializer, DatabaseInitializer>();
         services.AddScoped<ISearchService, MeiliSearchService>();
         services.AddScoped<TestResultIndexer>();
         services.AddSingleton<ICertificateRecordIndexer, CertificateRecordIndexer>();
    }
}
