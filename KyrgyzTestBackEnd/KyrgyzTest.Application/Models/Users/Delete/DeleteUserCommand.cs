using KyrgyzTest.Application.Abstractions;
using KyrgyzTest.Application.Contracts.Users;
using KyrgyzTest.Application.Extensions.Result;

namespace KyrgyzTest.Application.Models.Users.Delete;

public record DeleteUserCommand(long Id) : ICommand<Result<UserDto>>;
