namespace Application.Common.Interfaces;

public interface IIdentityService
{
    Task<int> CreateUserAsync(string email, string password);
    Task<string> GetToken(string email, string password);
    
}