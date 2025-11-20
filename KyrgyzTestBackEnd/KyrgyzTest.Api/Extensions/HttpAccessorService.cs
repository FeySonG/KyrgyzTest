using System.Security.Claims;
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
        var identity = new ClaimsIdentity(claims, "Coockies");
        var principal = new ClaimsPrincipal(identity);

        return _httpContext.SignInAsync(principal);
    }

    //method to get id from http context
    public long GetUserId()
    {
        var userId = long.Parse(_httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        return userId;
    }
}