using KyrgyzTest.Application.Services;

namespace KyrgyzTest.OldDbRegion.Services;

public class UnitOfWork(LegacyDbRegionContext dbRegionContext) : IUnitOfWork
{
    public Task<int> SaveChangesAsync()
        => dbRegionContext.SaveChangesAsync();

}