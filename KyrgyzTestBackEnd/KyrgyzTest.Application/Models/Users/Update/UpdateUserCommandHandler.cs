using KyrgyzTest.Core.Models.Users;
using KyrgyzTest.Application.Abstractions;
using KyrgyzTest.Application.Contracts.Users;
using KyrgyzTest.Application.Extensions.Result;
using KyrgyzTest.Application.Services;
using SibersCRM.Application.Models.Users;

namespace KyrgyzTest.Application.Models.Users.Update;

public class UpdateUserCommandHandler(
    IUserRepository userRepository,
    IUnitOfWork unitOfWork,
    IHttpAccessorService httpContext) :  ICommandHandler<UpdateUserCommand,  Result<UserDto>>
{
    public async Task<Result<UserDto>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var userId = httpContext.GetUserId();
        
        var user = await userRepository.GetByIdAsync(userId);
        if (user == null)
        {
            return new Error(UserErrorCode.UserNotFound, $"Failed to make changes, user id: {userId} not found");
        }
        
        var editUser = EditUser(request.UpdateArgs, user);
        
        userRepository.Update(editUser);
        
        await unitOfWork.SaveChangesAsync();
        return Result.Ok(UserDto.Create(editUser));
        
    }
    
    private User EditUser(UpdateUserDto updateArgs, User user)
    {
        if (!string.IsNullOrWhiteSpace(updateArgs.FirstName))
            user.FirstName = updateArgs.FirstName;
        
        if (!string.IsNullOrWhiteSpace(updateArgs.LastName))
            user.LastName = updateArgs.LastName;
        
        if (!string.IsNullOrWhiteSpace(updateArgs.MiddleName))
            user.MiddleName = updateArgs.MiddleName;
        
        return user;
    }
}