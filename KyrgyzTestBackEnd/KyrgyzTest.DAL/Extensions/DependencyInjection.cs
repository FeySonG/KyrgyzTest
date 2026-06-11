using KyrgyzTest.Core.Models.Users;
using KyrgyzTest.Application.Abstractions;
using KyrgyzTest.Application.Abstractions.MeilisearchAbstractions;
using KyrgyzTest.Application.Services;
using KyrgyzTest.DAL.Models.Users;
using KyrgyzTest.DAL.Seeds;
using KyrgyzTest.DAL.Services;
using KyrgyzTest.DAL.Services.Meilisearchs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace KyrgyzTest.DAL.Extensions;

public static class DependencyInjection
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
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
         services.AddScoped<IUserRepository, UserRepository>();
         services.AddScoped<IUnitOfWork, UnitOfWork>();
         services.AddTransient<IPasswordService, PasswordService>();
         services.AddScoped<IDatabaseInitializer, DatabaseInitializer>();
         services.AddScoped<ISearchService, MeiliSearchService>();
         services.AddScoped<TestResultIndexer>();    }
}