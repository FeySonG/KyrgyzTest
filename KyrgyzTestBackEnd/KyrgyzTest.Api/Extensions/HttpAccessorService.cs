using System.Security.Claims;
using KyrgyzTest.Application.Contracts.Users;
using KyrgyzTest.Application.Services;
using KyrgyzTest.Core.Models.Users;
using Microsoft.AspNetCore.Authentication;

namespace KyrgyzTest.Api.Extensions;

public class HttpAccessorService : IHttpAccessorService
{
    public HttpAccessorService(IHttpContextAccessor accessor)
    {
        if (accessor.HttpContext is null)
        {
            throw new ArgumentException(nameof(accessor.HttpContext));
        }

        _httpContext = accessor.HttpContext;
    }

    private readonly HttpContext _httpContext;

    
    public Task LoginHttpContext(User user)
    {
        var claims = new Claim[]
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Email, user.Login),
            new(ClaimTypes.Role, user.Role.ToString()),
            new(ClaimTypes.Expiration, DateTime.UtcNow.AddHours(5).ToString()),
        };
        var identity = new ClaimsIdentity(claims, "Cookies");
        var principal = new ClaimsPrincipal(identity);

        return _httpContext.SignInAsync("Cookies",principal);
    }

    //method to get id from http context
    public long GetUserId()
    {
        var userId = long.Parse(_httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        return userId;
    }

    public CheckUserDto? GetUserRole()
    {
        var userRole = _httpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);
        var userLogin = _httpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email);
        
        if (userRole is null || userLogin is null) return null;
        
        var user = new CheckUserDto
        {
            LogIn = userLogin.Value,
            Role = userRole.Value,
        }; 
        
        return user;
    }
}