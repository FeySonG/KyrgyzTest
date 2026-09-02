using System.Reflection;
using KyrgyzTest.Core.Models.CertificateRecords;
using KyrgyzTest.Core.Models.Users;
using Microsoft.EntityFrameworkCore;

namespace KyrgyzTest.DAL;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    
    public DbSet<User> Users => Set<User>();
    public DbSet<CertificateRecord> CertificateRecords => Set<CertificateRecord>();
    
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}

// dotnet ef migrations add cert-migration -p KyrgyzTest.DAL -s KyrgyzTest.Api --context AppDbContext
// dotnet ef database update -p KyrgyzTest.DAL -s KyrgyzTest.Api --context AppDbContext
