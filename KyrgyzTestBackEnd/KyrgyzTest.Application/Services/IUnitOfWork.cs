namespace KyrgyzTest.Application.Services;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync();
}