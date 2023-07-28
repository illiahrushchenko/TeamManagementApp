using MediatR;

namespace Application.Users.Commands.CreateUser;

public record CreateUserCommand(string Email, string Password) : IRequest<int>;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, int>
{
    public Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}