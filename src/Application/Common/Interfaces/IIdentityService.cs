using Application.Users.Queries.GetAuthToken;

namespace Application.Common.Interfaces;

public interface IIdentityService
{
    Task<int> CreateUserAsync(string email, string password);
    Task<string> GetAuthTokenAsync(string email, string password);
    
}