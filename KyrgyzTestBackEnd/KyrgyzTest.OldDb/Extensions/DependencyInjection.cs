using KyrgyzTest.Application.Abstractions.OldDbAbstractions.Regulations;
using KyrgyzTest.Application.Abstractions.OldDbAbstractions.Students;
using KyrgyzTest.Application.Abstractions.OldDbAbstractions.TestResults;
using KyrgyzTest.OldDb.Repositories.Regulations;
using KyrgyzTest.OldDb.Repositories.Students;
using KyrgyzTest.OldDb.Repositories.TestResultRepository;
using KyrgyzTest.OldDb.Seeds;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace KyrgyzTest.OldDb.Extensions;

public static class DependencyInjection
{
    public static void AddOldDbLayer(this IServiceCollection services, IConfiguration configuration)
    {
        var conn = configuration.GetConnectionString("OldDbConnection")
                   ?? throw new Exception("OldDbConnection not found");
        
        services.AddDbContext<LegacyDbContext>(options =>
        {
            options.UseSqlServer(
                conn,
                sqlOptions => sqlOptions.MigrationsAssembly(
                    typeof(LegacyDbContext).Assembly.FullName)
            );
        });

        services.InitRepositories();
    }
    
    private static void InitRepositories(this IServiceCollection services)
    {
        services.AddScoped<ITestResultRepository, TestResultRepository>();
        services.AddScoped<IStudentRepository, StudentRepository>();
        services.AddScoped<IRegulationRepository, RegulationRepository>();
        services.AddScoped<MeiliSearchSeeder>();
    }
}