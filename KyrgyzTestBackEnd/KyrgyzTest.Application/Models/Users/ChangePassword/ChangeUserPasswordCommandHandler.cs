using KyrgyzTest.Core.Models.Users;
using KyrgyzTest.Application.Abstractions;
using KyrgyzTest.Application.Extensions.Result;
using KyrgyzTest.Application.Services;
using SibersCRM.Application.Models.Users;

namespace KyrgyzTest.Application.Models.Users.ChangePassword;

public class ChangeUserPasswordCommandHandler(
    IUserRepository userRepository,
    IUnitOfWork unitOfWork,
    IPasswordService passwordService,
    IHttpAccessorService httpContext) : ICommandHandler<ChangeUserPasswordCommand, Result<string>>
{
    public async Task<Result<string>> Handle(ChangeUserPasswordCommand request, CancellationToken cancellationToken)
    {
        var userId = httpContext.GetUserId();

        var user = await userRepository.GetByIdAsync(userId);
        if (user == null || passwordService.Verify(request.Dto.OldPassword, user.Password) == false)
        {
            return new Error(UserErrorCode.WrongPassword, $"Failed incorrect password");
        }

        user.Password = passwordService.Hash(request.Dto.NewPassword);
        
        userRepository.Update(user);
        await unitOfWork.SaveChangesAsync();

        return "Password changed";
    }
}