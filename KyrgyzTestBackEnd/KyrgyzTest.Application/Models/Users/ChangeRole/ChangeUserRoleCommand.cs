using KyrgyzTest.Core.Models.Users;
using KyrgyzTest.Application.Abstractions;
using KyrgyzTest.Application.Extensions.Result;

namespace KyrgyzTest.Application.Models.Users.ChangeRole;

public record ChangeUserRoleCommand(UserRole Role, long UserId) :  ICommand<Result<string>>;
