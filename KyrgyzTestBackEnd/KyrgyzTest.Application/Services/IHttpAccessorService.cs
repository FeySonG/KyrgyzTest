using KyrgyzTest.Core.Models.Users;

namespace KyrgyzTest.Application.Services;

//contract for using http context in application layer
public interface IHttpAccessorService
{
    public Task LoginHttpContext(User user);
    public long GetUserId();
}