using KyrgyzTest.Application.Contracts.Users;
using KyrgyzTest.Application.Models.Users.ChangeLogin;
using KyrgyzTest.Application.Models.Users.ChangePassword;
using KyrgyzTest.Application.Models.Users.ChangeRole;
using KyrgyzTest.Application.Models.Users.Delete;
using KyrgyzTest.Application.Models.Users.GetAll;
using KyrgyzTest.Application.Models.Users.GetById;
using KyrgyzTest.Application.Models.Users.Update;
using KyrgyzTest.Core.Models.Users;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SibersCRM.API.Extensions;
using SibersCRM.Application.Models.Users;

namespace KyrgyzTest.Api.Controllers;

[Authorize]
[ApiController]
[Route("api-users")]
public class UserController(ISender sender) : ControllerBase
{ 
    [Authorize(Roles = "Admin")]
    [HttpGet("get-all")]
    public async Task<IActionResult> GetAll()
    {
        var response = await sender.Send(new GetAllUserQuery());

        return response.Match(
            onSuccess: value => Ok(response.Value),
            onFailure: error => BadRequest(error.Message)
        );
    }

    [Authorize]
    [HttpGet("get-by-{id}")]
    public async Task<IActionResult> GetById(long id)
    {
        var response = await sender.Send(new GetByIdUserQuery(id));

        return response.Match(
            onSuccess: value => Ok(response.Value),
            onFailure: error => BadRequest(error.Message)
        );
    }

    [Authorize]
    [HttpPut("update-user-name")]
    public async Task<IActionResult> Update(UpdateUserDto userNickNameDto)
    {
        var response = await sender.Send(new UpdateUserCommand(userNickNameDto));

        return response.Match(
            onSuccess: value => Ok(response.Value),
            onFailure: error => BadRequest(error.Message)
        );
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("change-user-role")]
    public async Task<IActionResult> ChangeRole([FromBody]ChangeUserRoleDto args)
    {
        var response = await sender.Send(new ChangeUserRoleCommand(args));

        return response.Match(
            onSuccess: value => NoContent(),
            onFailure: error => BadRequest(error.Message)
        );
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("delete-user")]
    public async Task<IActionResult> Delete(long id)
    {
        var response = await sender.Send(new DeleteUserCommand(id));

        return response.Match(
            onSuccess: value => NoContent(),
            onFailure: error => BadRequest(error.Message)
        );
    }

    
    [Authorize]
    [HttpPut("change-user-password")]
    public async Task<IActionResult> ChangePassword(ChangeUserPasswordDto args)
    {
        var response = await sender.Send(new ChangeUserPasswordCommand(args));

        return response.Match(
            onSuccess: value => Ok(response.Value),
            onFailure: error => BadRequest(error.Message)
        );
    }

    [Authorize]
    [HttpPut("change-user-login")]
    public async Task<IActionResult> ChangeLogin([FromBody] ChangeLoginDto args)
    {
        var response = await sender.Send(new ChangeLoginCommand(args));
        
        return response.Match(
            onSuccess: value => NoContent(),
            onFailure: error => BadRequest(error.Message)
        );
    }
    
}