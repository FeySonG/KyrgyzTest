using KyrgyzTest.Application.Services;

namespace KyrgyzTest.DAL.Services;

//Unit of work implementation
public class UnitOfWork(AppDbContext dbContext) : IUnitOfWork
{
    public Task<int> SaveChangesAsync() => dbContext.SaveChangesAsync();
}