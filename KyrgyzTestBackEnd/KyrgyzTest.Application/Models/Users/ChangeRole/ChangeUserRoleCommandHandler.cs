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
        var user = await userRepository.GetByIdAsync(request.Args.UserId);

        if (user == null)
        {
            return new Error(UserErrorCode.UserNotFound, $"Operation failed user id: {request.Args.UserId} not found");
        }

        user.Role = request.Args.Role;

        userRepository.Update(user);
        await unitOfWork.SaveChangesAsync();

        return "Role changed";
    }
}