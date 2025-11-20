using KyrgyzTest.Core.Models.Users;
using KyrgyzTest.Application.Abstractions;
using KyrgyzTest.Application.Contracts.Users;
using KyrgyzTest.Application.Extensions.Result;
using KyrgyzTest.Application.Services;
using SibersCRM.Application.Models.Users;

namespace KyrgyzTest.Application.Models.Users.Delete;

public class DeleteUserCommandHandler(
    IUserRepository userRepository,
    IUnitOfWork unitOfWork) :  ICommandHandler<DeleteUserCommand, Result<UserDto>>
{
    public async Task<Result<UserDto>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(request.Id);
        if (user is null)
        {
            return new Error(UserErrorCode.UserNotFound, $"Operation failed user id: {request.Id} not found");
        }
        
        // var employee = await employeeRepository.GetByIdAsync(user.Id);
        // if (employee == null)
        // {
        //     return new Error(EmployeeErrorCode.EmployeeIsNotFound, $"Failed to make changes, employee id: {user.Id} not found");
        // }
        // employeeRepository.Remove(employee);
        
        
        userRepository.Remove(user);
        await unitOfWork.SaveChangesAsync();
        return Result.Ok(UserDto.Create(user));
    }
}