using MediatR;

namespace Application.TableCards.Commands.DeleteTableCard;

public record DeleteTableCardCommand() : IRequest;

public class DeleteTableCardCommandHandler : IRequestHandler<DeleteTableCardCommand>
{
    public Task Handle(DeleteTableCardCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}