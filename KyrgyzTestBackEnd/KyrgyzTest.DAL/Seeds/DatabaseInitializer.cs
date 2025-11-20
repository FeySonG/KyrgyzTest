using KyrgyzTest.Application.Abstractions;
using KyrgyzTest.Application.Services;
using Microsoft.EntityFrameworkCore;

namespace KyrgyzTest.DAL.Seeds;

public class DatabaseInitializer(AppDbContext context, IPasswordService passwordService) : IDatabaseInitializer
{
    public void Initialize()
    {
        // application of migrations
        context.Database.Migrate();

        // launch the seeder
       DbUserSeed.UserSeed(context, passwordService);
    }
}