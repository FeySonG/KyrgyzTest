using KyrgyzTest.Application.Services;
using KyrgyzTest.Core.Models.Users;

namespace KyrgyzTest.DAL.Seeds;

public static class DbUserSeed
{
    public static void UserSeed(AppDbContext context, IPasswordService  passwordService)
    {
        if (!context.Users.Any(u => u.Login == "Daniel@mail.com"))
        {
            context.Users.Add(new User
            {
                FirstName = "Daniel",
                LastName = "Ajiev",
                Login = "Daniel@mail.com",
                Password = passwordService.Hash("123"),
                Role = UserRole.Manager
            });
        }
        context.SaveChanges();
    }
}