using KyrgyzTest.Core.Models.Users;

namespace KyrgyzTest.Application.Contracts.Users;

public class CheckUserDto
{
    public required string LogIn { get; set; }
    
    public required string Role { get; set; }
}