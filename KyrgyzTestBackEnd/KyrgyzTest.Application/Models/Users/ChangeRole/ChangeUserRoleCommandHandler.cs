using KyrgyzTest.Core.Models.Users;
using KyrgyzTest.Application.Abstractions;
using KyrgyzTest.Application.Extensions.Result;
using KyrgyzTest.Application.Services;
using SibersCRM.Application.Models.Users;

namespace KyrgyzTest.Application.Models.Users.ChangeRole;

public class ChangeUserRoleCommandHandler(
    IUserRepository userRepository,
    IUnitOfWork unitOfWork
    ) :  ICommandHandler<ChangeUserRoleCommand, Result<string>>
{
    public async Task<Result<string>> Handle(ChangeUserRoleCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(request.UserId);

        if (user == null)
        {
            return new Error(UserErrorCode.UserNotFound, $"Operation failed user id: {request.UserId} not found");
        }

        user.Role = request.Role;

        userRepository.Update(user);
        await unitOfWork.SaveChangesAsync();

        return "Role changed";
    }
}