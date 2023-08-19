using MediatR;

namespace Application.Boards.Commands.DeleteBoard;

public record DeleteBoardCommand(int Id) : IRequest;

public class DeleteBoardCommandHandler : IRequestHandler<DeleteBoardCommand>
{
    public Task Handle(DeleteBoardCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}