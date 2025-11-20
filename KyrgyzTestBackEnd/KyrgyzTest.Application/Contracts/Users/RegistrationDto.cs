using System.ComponentModel.DataAnnotations;
using KyrgyzTest.Application.Validations;

namespace KyrgyzTest.Application.Contracts.Users;

public class RegistrationDto
{
    [Required(AllowEmptyStrings = false, ErrorMessage = Message.REQUIRED)]
    [MaxLength(30, ErrorMessage = Message.MAX_LENGTH)]
    [RegularExpression(@"^[a-zA-Z][a-zA-Z\s]*[a-zA-Z]$", ErrorMessage = Message.ONLY_LETTERS)]
    public required string FirstName { get; set; }   
    
    [Required(AllowEmptyStrings = false, ErrorMessage = Message.REQUIRED)]
    [MaxLength(30, ErrorMessage = Message.MAX_LENGTH)]
    [RegularExpression(@"^[a-zA-Z][a-zA-Z\s]*[a-zA-Z]$", ErrorMessage = Message.ONLY_LETTERS)]
    public required string LastName { get; set; } 
    
    [MaxLength(30, ErrorMessage = Message.MAX_LENGTH)]
    [RegularExpression(@"^[a-zA-Z][a-zA-Z\s]*[a-zA-Z]$", ErrorMessage = Message.ONLY_LETTERS)]
    public string? MiddleName { get; set; } 
    
    [MaxLength(80, ErrorMessage = Message.MAX_LENGTH)]
    public required string Login { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = Message.REQUIRED)]
    [MaxLength(30, ErrorMessage = Message.MAX_LENGTH)]
    public required string Password { get; set; }
}