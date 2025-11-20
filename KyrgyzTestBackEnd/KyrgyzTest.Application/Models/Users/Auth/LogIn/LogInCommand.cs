using KyrgyzTest.Application.Abstractions;
using KyrgyzTest.Application.Contracts.Users;
using KyrgyzTest.Application.Extensions.Result;

namespace KyrgyzTest.Application.Models.Users.Auth.LogIn;

public record LogInCommand(LogInDto Dto) :  ICommand<Result<string>>;