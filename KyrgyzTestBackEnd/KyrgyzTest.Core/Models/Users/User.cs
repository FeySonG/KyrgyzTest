using KyrgyzTest.Core.Abstractions;

namespace KyrgyzTest.Core.Models.Users;

public class User : Entity
{
    public required string FirstName { get; set; }   // имя
    
    public required string LastName { get; set; }    // фамилия
    
    public string? MiddleName { get; set; } // отчество (nullable, т.к. не всегда есть)
    
    public required string Login { get; set; }
    
    public required string Password { get; set; }
    
    public UserRole Role { get; set; } = UserRole.Employee;
}