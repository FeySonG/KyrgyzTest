using System.ComponentModel.DataAnnotations;
using KyrgyzTest.Application.Validations;

namespace KyrgyzTest.Application.Contracts.Users;

public class LogInDto
{
    [Required(AllowEmptyStrings = false, ErrorMessage = Message.REQUIRED)]
    [MaxLength(100, ErrorMessage = Message.MAX_LENGTH)]
    [EmailAddress(ErrorMessage = Message.EMAIL)]
    public required string Login { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = Message.REQUIRED)]
    [MaxLength(30, ErrorMessage = Message.MAX_LENGTH)]
    public required string Password { get; set; }
}