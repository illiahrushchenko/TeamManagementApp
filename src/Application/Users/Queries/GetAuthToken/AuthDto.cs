using Application.Common.Models;

namespace Application.Users.Queries.GetAuthToken;

public class AuthDto : Result
{
    public string Token { get; init; }
    
    public AuthDto(string token, bool succeeded, IEnumerable<string> errors) : base(succeeded, errors)
    {
        Token = token;
    }
}