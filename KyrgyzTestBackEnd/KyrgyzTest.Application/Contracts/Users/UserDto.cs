using KyrgyzTest.Core.Models.Users;

namespace KyrgyzTest.Application.Contracts.Users;

public class UserDto
{
    public long Id { get; set; }
    
    public required string FirstName { get; set; }   // имя
    
    public required string LastName { get; set; }    // фамилия
    
    public string? MiddleName { get; set; } // отчество (nullable, т.к. не всегда есть)
    
    public required string Login { get; set; }

    public string? Password { get; set; }

    public string? Role { get; set; }
    
    
    public static UserDto Create(User user)
    {
        return new UserDto
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            MiddleName = user.MiddleName,
            Login = user.Login,
            Password = user.Password,
            Role = user.Role.ToString()
        };
    }
}