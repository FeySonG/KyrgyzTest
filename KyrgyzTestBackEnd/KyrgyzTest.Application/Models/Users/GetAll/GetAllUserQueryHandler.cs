using KyrgyzTest.Core.Models.Users;
using KyrgyzTest.Application.Abstractions;
using KyrgyzTest.Application.Contracts.Users;
using KyrgyzTest.Application.Extensions.Result;

namespace KyrgyzTest.Application.Models.Users.GetAll;

public class GetAllUserQueryHandler(IUserRepository userRepository) : IQueryHandler<GetAllUserQuery, Result<List<UserDto>>>
{
    public async Task<Result<List<UserDto>>> Handle(GetAllUserQuery request, CancellationToken cancellationToken)
    {
        var listUsers = await userRepository.GetAllAsync();
        
        return Result.Ok(MapList(listUsers));
    }

    private List<UserDto> MapList(List<User> users)
    {
        var listDto = new List<UserDto>();
        foreach (var user in users)
        {
            listDto.Add(UserDto.Create(user));
        }
        return listDto;
    }
}