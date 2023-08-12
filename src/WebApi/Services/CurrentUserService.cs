using System.Security.Claims;
using Application.Common.Interfaces;

namespace WebApi.Services;

public class CurrentUserService : ICurrentUserService
{
    private IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }
    
    public int UserId => 
        int.Parse(_httpContextAccessor.HttpContext?.User.FindFirstValue("userId")!);
    
    public string? UserEmail => _httpContextAccessor.HttpContext?.User.FindFirstValue("email");
}