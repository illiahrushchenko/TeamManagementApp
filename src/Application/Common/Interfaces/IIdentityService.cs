using Application.Users.Queries.GetAuthToken;
using Domain.Entities;

namespace Application.Common.Interfaces;

public interface IIdentityService
{
    Task<int> CreateUserAsync(string email, string password);
    Task<User> FindUserByEmailAsync(string email);
    Task<string> GetAuthTokenAsync(string email, string password);
    
}