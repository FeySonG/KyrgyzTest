using KyrgyzTest.Application.Abstractions;
using KyrgyzTest.Application.Contracts.Users;
using KyrgyzTest.Application.Extensions.Result;
using KyrgyzTest.Application.Services;
using KyrgyzTest.Core.Models.Users;
using MediatR;
using SibersCRM.Application.Models.Users;

namespace KyrgyzTest.Application.Models.Users.ChangeLogin;

public class ChangeLoginCommandHandler(
                        IUserRepository userRepository,
                        IUnitOfWork unitOfWork) :  ICommandHandler<ChangeLoginCommand, Result<Unit>>
{
    public async Task<Result<Unit>> Handle(ChangeLoginCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(request.Args.Id);
        if(user is null) 
        {
            return new Error(UserErrorCode.UserNotFound, $"Operation failed user id: {request.Args.Id} not found");
        }

        if (await userRepository.CheckUniqueLogInAsync(request.Args.Login))
        {
            return new Error(UserErrorCode.LoginIsNotUnique, $"User Login is not unique");
        }
        
        user.Login = request.Args.Login;
        
        userRepository.Update(user);
        await unitOfWork.SaveChangesAsync();
        
        return Result.Ok(Unit.Value);
    }
}