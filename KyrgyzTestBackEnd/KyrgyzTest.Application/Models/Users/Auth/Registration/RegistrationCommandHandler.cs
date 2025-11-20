using KyrgyzTest.Core.Models.Users;
using KyrgyzTest.Application.Abstractions;
using KyrgyzTest.Application.Contracts.Users;
using KyrgyzTest.Application.Extensions.Result;
using KyrgyzTest.Application.Services;
using SibersCRM.Application.Models.Users;

namespace KyrgyzTest.Application.Models.Users.Auth.Registration;

public class RegistrationCommandHandler(
    IUserRepository userRepository,
    IUnitOfWork unitOfWork,
    IPasswordService passwordService) 
    :  ICommandHandler<RegistrationCommand, Result<UserDto>>
{
    public async Task<Result<UserDto>> Handle(RegistrationCommand request, CancellationToken cancellationToken)
    {
        if (await userRepository.CheckUniqueLogInAsync(request.Args.Login))
        {
            return new Error(UserErrorCode.LoginIsNotUnique, $"User Login is not unique");
        }
        
        var user = CreateUser(request.Args);
        user.Password = passwordService.Hash(request.Args.Password);

        userRepository.Add(user);
        await unitOfWork.SaveChangesAsync();
        
        return Result.Ok(UserDto.Create(user));
    }

    private User CreateUser(RegistrationDto dto)
    {
        return new User
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            MiddleName = dto.MiddleName,
            Login = dto.Login,
            Password = dto.Password,
            Role = UserRole.Employee
        };
    }
}