using KyrgyzTest.Application.Abstractions;
using KyrgyzTest.Application.Contracts.Users;
using KyrgyzTest.Application.Extensions.Result;
using MediatR;

namespace KyrgyzTest.Application.Models.Users.ChangeLogin;

public record ChangeLoginCommand(ChangeLoginDto Args) : ICommand<Result<Unit>>;
