using KyrgyzTest.Application.Abstractions;
using KyrgyzTest.Application.Contracts.Users;
using KyrgyzTest.Application.Extensions.Result;

namespace KyrgyzTest.Application.Models.Users.Update;

public record UpdateUserCommand(UpdateUserDto UpdateArgs) : ICommand<Result<UserDto>>;
