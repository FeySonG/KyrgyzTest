using System.ComponentModel.DataAnnotations;
using KyrgyzTest.Application.Validations;

namespace KyrgyzTest.Application.Contracts.Users;

public class ChangeUserPasswordDto
{
    [Required(AllowEmptyStrings = false, ErrorMessage = Message.REQUIRED)]
    [MaxLength(30, ErrorMessage = Message.MAX_LENGTH)]
    public required string OldPassword { get; set; }
    
    [Required(AllowEmptyStrings = false, ErrorMessage = Message.REQUIRED)]
    [MaxLength(30, ErrorMessage = Message.MAX_LENGTH)]
    public required string NewPassword { get; set; }
}