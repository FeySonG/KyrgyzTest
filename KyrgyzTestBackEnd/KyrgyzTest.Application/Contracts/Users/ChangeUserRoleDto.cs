using KyrgyzTest.Core.Models.Users;

namespace KyrgyzTest.Application.Contracts.Users;

public class ChangeUserRoleDto
{
    public long UserId { get; set; }
    public UserRole Role { get; set; }
}