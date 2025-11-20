using KyrgyzTest.Core.Models.Users;
using KyrgyzTest.Application.Abstractions;
using KyrgyzTest.Application.Contracts.Users;
using KyrgyzTest.Application.Extensions.Result;
using SibersCRM.Application.Models.Users;

namespace KyrgyzTest.Application.Models.Users.GetById;

public class GetByIdUserQueryHandler(
    IUserRepository userRepository
    ) :  IQueryHandler<GetByIdUserQuery, Result<UserDto>>
{
    public async Task<Result<UserDto>> Handle(GetByIdUserQuery request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(request.Id);
        if (user == null)
        {
            return new Error(UserErrorCode.UserNotFound, $"Operation failed user id: {request.Id} not found");
        }
        return Result.Ok(UserDto.Create(user));
    }
}