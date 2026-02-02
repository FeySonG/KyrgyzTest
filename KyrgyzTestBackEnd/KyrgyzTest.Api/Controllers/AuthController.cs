using System.Security.Claims;
using KyrgyzTest.Application.Contracts.Users;
using KyrgyzTest.Application.Models.Users.Auth.CheckUser;
using KyrgyzTest.Application.Models.Users.Auth.LogIn;
using KyrgyzTest.Application.Models.Users.Auth.Registration;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using SibersCRM.API.Extensions;

namespace KyrgyzTest.Api.Controllers;

[Route("api-auth")]
public class AuthController(ISender sender) : ControllerBase
{
    [HttpPost("registration")]
    public async Task<IActionResult> Registration([FromBody] RegistrationDto dto)
    {
        var result = await sender.Send(new RegistrationCommand(dto));

        return result.Match(
            onSuccess: value => Ok(),
            onFailure: error => BadRequest(error.Message));
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LogInDto loginDto)
    {
        var result = await sender.Send(new LogInCommand(loginDto));

        return result.Match(
            onSuccess: value => Ok("Authorize!"),
            onFailure: error => Unauthorized(error.Message));
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync("Cookies");
        return Ok();
    }

    [HttpGet("me")]
    public async Task<IActionResult> Me()
    {
        if (!(User.Identity?.IsAuthenticated ?? false))
            return Unauthorized();
        
        var result = await sender.Send(new CheckUserQuery());
        
        return result.Match(
            onSuccess: value => Ok(result.Value),
            onFailure: error => Unauthorized(error.Message));

    }
}