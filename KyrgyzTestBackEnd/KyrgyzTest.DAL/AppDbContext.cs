using System.Reflection;
using KyrgyzTest.Core.Models.Users;
using Microsoft.EntityFrameworkCore;

namespace KyrgyzTest.DAL;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    
    public DbSet<User> Users => Set<User>();
    
    
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}

// dotnet ef migrations add FirstMigration `
//     >> --project "FrequencyDictionary.DAL/FrequencyDictionary.DAL.csproj" `
//     >> --startup-project "FrequencyDictionary.Api/FrequencyDictionary.Api.csproj"

// dotnet ef database update
//      >> --project "FrequencyDictionary.DAL/FrequencyDictionary.DAL.csproj" `
//      >> --startup-project "FrequencyDictionary.Api/FrequencyDict