using KyrgyzTest.Application.Abstractions.OldDbAbstractions.TestResults;
using KyrgyzTest.OldDb.Models;
using KyrgyzTest.OldDb.Repositories.TestResultRepository;
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
            options.UseSqlServer(conn);
        });

        services.InitRepositories();
    }
    
    private static void InitRepositories(this IServiceCollection services)
    {
        services.AddScoped<ITestResultRepository, TestResultRepository>();
    }
}