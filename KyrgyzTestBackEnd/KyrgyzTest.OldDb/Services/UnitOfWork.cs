using KyrgyzTest.Application.Services;
using KyrgyzTest.OldDb.Models;

namespace KyrgyzTest.OldDb.Services;

public class UnitOfWork(LegacyDbContext dbContext) : IUnitOfWork
{
    public Task<int> SaveChangesAsync()
        => dbContext.SaveChangesAsync();

}