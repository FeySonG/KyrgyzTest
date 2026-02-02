namespace KyrgyzTest.Application.Contracts.Users;

public class ChangeLoginDto
{
    public required long Id { get; set; }
    public required string Login { get; set; }
}