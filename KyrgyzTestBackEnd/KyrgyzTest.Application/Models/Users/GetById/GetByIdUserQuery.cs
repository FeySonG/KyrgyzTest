using KyrgyzTest.Application.Abstractions;
using KyrgyzTest.Application.Contracts.Users;
using KyrgyzTest.Application.Extensions.Result;

namespace KyrgyzTest.Application.Models.Users.GetById;

public record GetByIdUserQuery(long Id) :  IQuery<Result<UserDto>>;
