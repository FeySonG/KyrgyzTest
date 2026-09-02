using KyrgyzTest.Core.Models.Users;
using KyrgyzTest.DAL.Services;
using Microsoft.EntityFrameworkCore;

namespace KyrgyzTest.DAL.Models.Users;

public class UserRepository(AppDbContext dbContext) : Repository<User>(dbContext), IUserRepository
{
    private readonly AppDbContext _dbContext = dbContext;
    public async Task<bool> CheckUniqueLogInAsync(string login) => await _dbContext.Users.AnyAsync(u => u.Login == login);

    public Task<User?> GetUserByLogInAsync(string email) => _dbContext.Users.FirstOrDefaultAsync(u => u.Login == email);
}