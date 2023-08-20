using MediatR;

namespace Application.TableCards.Commands.UpdateTableCard;

public record UpdateTableCardCommand(int TableCardId, string Title, string Description, int TableId) : IRequest<int>;

public class UpdateTableCardCommandHandler : IRequestHandler<UpdateTableCardCommand, int>
{
    public Task<int> Handle(UpdateTableCardCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}