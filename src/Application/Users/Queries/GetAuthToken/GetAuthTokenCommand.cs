using Application.Common.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Users.Queries.GetAuthToken;

public record GetAuthTokenCommand(string Email, string Password) : IRequest<AuthDto>;

public class GetAuthTokenCommandHandler : IRequestHandler<GetAuthTokenCommand, AuthDto>
{
    private readonly IIdentityService _identityService;

    public GetAuthTokenCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public Task<AuthDto> Handle(GetAuthTokenCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}