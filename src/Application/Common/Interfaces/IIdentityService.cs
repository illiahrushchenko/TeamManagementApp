namespace Application.Common.Interfaces;

public interface IIdentityService
{
    Task CreateUserAsync(string email, string password);
    Task<string> GetToken(string email, string password);
    
}