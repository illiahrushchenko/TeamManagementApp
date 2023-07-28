using Application.Common.Interfaces;
using MediatR;

namespace Application.Users.Commands.CreateUser;

public record CreateUserCommand(string Email, string Password) : IRequest<int>;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, int>
{
    private readonly IIdentityService _identityService;

    public CreateUserCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }
    
    public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        return await _identityService.CreateUserAsync(request.Email, request.Password);
    }
}