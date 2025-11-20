using KyrgyzTest.Application.Abstractions;
using KyrgyzTest.Application.Contracts.Users;
using KyrgyzTest.Application.Extensions.Result;

namespace KyrgyzTest.Application.Models.Users.ChangePassword;

public record ChangeUserPasswordCommand(ChangeUserPasswordDto Dto) : ICommand<Result<string>>;
