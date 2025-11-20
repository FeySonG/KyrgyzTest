using KyrgyzTest.Application.Abstractions;
using KyrgyzTest.Application.Contracts.Users;
using KyrgyzTest.Application.Extensions.Result;

namespace KyrgyzTest.Application.Models.Users.Auth.Registration;

public record RegistrationCommand(RegistrationDto Args) : ICommand<Result<UserDto>>;