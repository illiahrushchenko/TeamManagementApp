using Application.Common.Exceptions;
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
        
        if (!result.Succeeded)
        {
            var errors = result.Errors.Select(x => x.Description).ToArray();
            throw new UnauthorizedException(errors);
        }
    }

    public Task<string> GetToken(string email, string password)
    {
        throw new NotImplementedException();
    }
}