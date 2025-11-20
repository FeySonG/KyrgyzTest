using KyrgyzTest.Core.Abstractions;

namespace KyrgyzTest.Core.Models.Users;

public interface IUserRepository : IRepository<User>
{
    public Task<bool> CheckUniqueLogInAsync(string login);
    public Task<User?> GetUserByLogInAsync(string email);
}