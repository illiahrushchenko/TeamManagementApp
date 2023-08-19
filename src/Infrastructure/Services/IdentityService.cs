using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Services;

public class IdentityService : IIdentityService
{
    private readonly UserManager<User> _userManager;
    private readonly IConfiguration _configuration;

    public IdentityService(UserManager<User> userManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _configuration = configuration;
    }
    
    public async Task<int> CreateUserAsync(string email, string password)
    {
        var user = new User
        {
            UserName = email,
            Email = email
        };
        
        //Make username equal to email
        var result = await _userManager.CreateAsync(user, password);
        
        if (!result.Succeeded)
        {
            var errors = result.Errors.Select(x => x.Description).ToArray();
            throw new UnauthorizedException(errors);
        }
        
        return user.Id;
    }

    public async Task<User> FindUserByEmailAsync(string email)
    {
        return await _userManager.FindByEmailAsync(email);
    }

    public async Task<User> FindUserByIdAsync(int id)
    {
        return await _userManager.FindByIdAsync(id.ToString());
    }

    public async Task<string> GetAuthTokenAsync(string email, string password)
    {
        var user = await _userManager.FindByEmailAsync(email);

        if(user is null)
        {
            throw new UnauthorizedException("User not found");
        }
        if(!await _userManager.CheckPasswordAsync(user, password))
        {
            throw new UnauthorizedException($"Invalid password");
        }

        return GenerateJwt(email, user.Id);
    }

    private string GenerateJwt(string email, int userId)
    {
        var claims = new List<Claim>
        {
            new Claim("email", email),
            new Claim("userId", userId.ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"]!));

        var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            _configuration["Jwt:Iss"],
            _configuration["Jwt:Aud"],
            claims,
            notBefore: DateTime.Now,
            expires: DateTime.Now.AddDays(14),
            signingCredentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}