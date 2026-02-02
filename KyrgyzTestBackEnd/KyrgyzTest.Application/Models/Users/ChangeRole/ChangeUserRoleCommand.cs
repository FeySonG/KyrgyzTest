using KyrgyzTest.Core.Models.Users;
using KyrgyzTest.Application.Abstractions;
using KyrgyzTest.Application.Contracts.Users;
using KyrgyzTest.Application.Extensions.Result;

namespace KyrgyzTest.Application.Models.Users.ChangeRole;

public record ChangeUserRoleCommand(ChangeUserRoleDto Args) :  ICommand<Result<string>>;
