using Application.Common.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Services;

public class IdentityService : IIdentityService
{
    private readonly UserManager<User> _userManager;

    public IdentityService(UserManager<User> userManager)
    {
        _userManager = userManager;
    }
    
    public async Task CreateUserAsync(string email, string password)
    {
        //Make username equal to email
        var result = await _userManager.CreateAsync(new User
        {
            UserName = email,
            Email = email
        }, password);
    }

    public Task<string> GetToken(string email, string password)
    {
        throw new NotImplementedException();
    }
}