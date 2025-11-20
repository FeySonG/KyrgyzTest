using System.Runtime.InteropServices.JavaScript;
using KyrgyzTest.Core.Models.Users;
using KyrgyzTest.Application.Abstractions;
using KyrgyzTest.Application.Extensions.Result;
using KyrgyzTest.Application.Services;
using SibersCRM.Application.Models.Users;

namespace KyrgyzTest.Application.Models.Users.Auth.LogIn;

public class LogInCommandHandler(IUserRepository userRepository, 
    IHttpAccessorService httpContext
    ,IPasswordService passwordService) :  ICommandHandler<LogInCommand, Result<string>>
{
    public async Task<Result<string>> Handle(LogInCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetUserByLogInAsync(request.Dto.Login);

        if (user == null || passwordService.Verify(request.Dto.Password, user.Password) == false)
            return new Error(UserErrorCode.WrongPassword, $"Failed to login incorrect password");

        await httpContext.LoginHttpContext(user);
        return "Success";
    }
}