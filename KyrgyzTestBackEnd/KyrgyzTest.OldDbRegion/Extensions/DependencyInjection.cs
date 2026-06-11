using KyrgyzTest.Application.Abstractions.OldDbRegionAbstractions.Regulations;
using KyrgyzTest.Application.Abstractions.OldDbRegionAbstractions.TestResults;
using KyrgyzTest.OldDbRegion.Repositories.Regulations;
using KyrgyzTest.OldDbRegion.Repositories.TestResultRepository;
using KyrgyzTest.OldDbRegion.Seeds;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace KyrgyzTest.OldDbRegion.Extensions;

public static class DependencyInjection
{
    public static void AddOldDbRegionLayer(this IServiceCollection services, IConfiguration configuration)
    {
        var conn = configuration.GetConnectionString("OldDbRegionConnection")
                   ?? throw new Exception("OldDbRegionConnection not found");
        
        services.AddDbContext<LegacyDbRegionContext>(options =>
        {
            options.UseSqlServer(conn);
        });

        services.InitRepositories();
    }
    
    private static void InitRepositories(this IServiceCollection services)
    {
        services.AddScoped<ITestResultRegionRepository, TestResultRepository>();
        services.AddScoped<IRegulationRegionRepository, RegulationRepository>();
        services.AddScoped<MeiliSearchSeeder>();
    }
}