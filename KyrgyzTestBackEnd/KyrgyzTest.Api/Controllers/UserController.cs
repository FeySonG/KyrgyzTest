using KyrgyzTest.Application.Contracts.Users;
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
[Route("api/users")]
public class UserController(ISender sender) : ControllerBase
{ 
    [Authorize(Roles = "Manager")]
    [HttpGet("get-all")]
    public async Task<IActionResult> GetAll()
    {
        var response = await sender.Send(new GetAllUserQuery());

        return response.Match(
            onSuccess: value => Ok(response.Value),
            onFailure: error => BadRequest()
        );
    }

    [Authorize]
    [HttpGet("get-by-{id}")]
    public async Task<IActionResult> GetById(long id)
    {
        var response = await sender.Send(new GetByIdUserQuery(id));

        return response.Match(
            onSuccess: value => Ok(response.Value),
            onFailure: error =>
            {
                if (error.Code == UserErrorCode.UserNotFound)
                    return BadRequest(error.Message);

                else return BadRequest();
            }
        );
    }

    [Authorize]
    [HttpPut("update-user")]
    public async Task<IActionResult> Update(UpdateUserDto userNickNameDto)
    {
        var response = await sender.Send(new UpdateUserCommand(userNickNameDto));

        return response.Match(
            onSuccess: value => Ok(response.Value),
            onFailure: error =>
            {
                if (error.Code == UserErrorCode.UserNotFound)
                    return NotFound(error.Message);

                if (error.Code == UserErrorCode.LoginIsNotUnique)
                    return BadRequest(error.Message);

                return BadRequest();
            }
        );
    }

    //[Authorize(Roles = "Admin")]
    [HttpPut("change-role/{id}")]
    public async Task<IActionResult> ChangeRole(long id, UserRole role)
    {
        var response = await sender.Send(new ChangeUserRoleCommand(role, id));

        return response.Match(
            onSuccess: value => Ok(response.Value),
            onFailure: error =>
            {
                if (error.Code == UserErrorCode.UserNotFound)
                    return BadRequest(error.Message);

                return BadRequest();
            }
        );
    }

    [Authorize(Roles = "Manager")]
    [HttpDelete("delete-by{id}")]
    public async Task<IActionResult> Delete(long id)
    {
        var response = await sender.Send(new DeleteUserCommand(id));

        return response.Match(
            onSuccess: value => NoContent(),
            onFailure: error =>
            {
                if (error.Code == UserErrorCode.UserNotFound)
                    return BadRequest(error.Message);

                return BadRequest();
            }
        );
    }


    [Authorize]
    [HttpPut("change-password/{id}")]
    public async Task<IActionResult> ChangePassword(ChangeUserPasswordDto args)
    {
        var response = await sender.Send(new ChangeUserPasswordCommand(args));

        return response.Match(
            onSuccess: value => Ok(response.Value),
            onFailure: error => BadRequest()
        );
    }
}