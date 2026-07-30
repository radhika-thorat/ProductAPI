using Microsoft.AspNetCore.Http;

namespace Infrastructure.Services;

public class UserService : IUserService
{
    private readonly IHttpContextAccessor _contextAccessor;

    public UserService(IHttpContextAccessor contextAccessor)
    {
        _contextAccessor = contextAccessor;
    }

    public string GetCurrentUser()
    {
        return _contextAccessor
            .HttpContext?
            .User?
            .Identity?
            .Name ?? "System";
    }
}