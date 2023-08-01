using Application.Common.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Users.Queries.GetAuthToken;

public record GetAuthTokenQuery(string Email, string Password) : IRequest<AuthDto>;

public class GetAuthTokenQueryHandler : IRequestHandler<GetAuthTokenQuery, AuthDto>
{
    private readonly IIdentityService _identityService;

    public GetAuthTokenQueryHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<AuthDto> Handle(GetAuthTokenQuery request, CancellationToken cancellationToken)
    {
        var token = await _identityService.GetAuthTokenAsync(request.Email, request.Password);

        return new AuthDto(token);
    }
}