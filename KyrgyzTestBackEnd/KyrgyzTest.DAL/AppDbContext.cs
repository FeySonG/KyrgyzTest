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
//      >> --project "KyrgyzTest.DAL/KyrgyzTest.DAL.csproj" `
//      >> --startup-project "KyrgyzTest.Api/KyrgyzTest.Api.csproj"

// dotnet ef database update
//      >> --project "KyrgyzTest.DAL/KyrgyzTest.DAL.csproj" `
//      >> --startup-project "KyrgyzTest.Api/KyrgyzTest.Api.csproj"

// dotnet ef migrations remove `
// --project "KyrgyzTest.DAL/KyrgyzTest.DAL.csproj" `
// --startup-project "KyrgyzTest.Api/KyrgyzTest.Api.csproj"