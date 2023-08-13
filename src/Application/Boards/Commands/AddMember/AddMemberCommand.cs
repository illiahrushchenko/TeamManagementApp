using MediatR;

namespace Application.Boards.Commands.AddMember;

public record AddMemberCommand(int boardId, string email) : IRequest<int>;

public class AddMemberCommandHandler : IRequestHandler<AddMemberCommand, int>
{
    public async Task<int> Handle(AddMemberCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}