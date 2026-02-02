using KyrgyzTest.Application.Abstractions;
using KyrgyzTest.Application.Contracts.Users;
using KyrgyzTest.Application.Extensions.Result;
using KyrgyzTest.Application.Services;
using KyrgyzTest.Core.Models.Users;
using SibersCRM.Application.Models.Users;


namespace KyrgyzTest.Application.Models.Users.Auth.CheckUser;

public class CheckUserQueryHandler(
    IHttpAccessorService httpContext,
    IUserRepository userRepository) : IQueryHandler<CheckUserQuery, Result<UserDto>>
{
    public async Task<Result<UserDto>> Handle(CheckUserQuery request, CancellationToken cancellationToken)
    {
        var userLogin = httpContext.GetUserRole();
        var userId = httpContext.GetUserId();
        
        var user = await userRepository.GetByIdAsync(userId);
        if (user is null)
        {
            return new Error(UserErrorCode.UserNotFound, $"Operation failed user id: {userId} not found");
        }
        
        if (userLogin is null) throw new ArgumentNullException(nameof(request), "Пользователь не авторизован");
        
        return UserDto.Create(user);
    }
}