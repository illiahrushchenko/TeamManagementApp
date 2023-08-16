using MediatR;

namespace Application.TableCards.Commands.UpdateTableCard;

public record UpdateTableCardCommand() : IRequest<int>;

public class UpdateTableCardCommandHandler : IRequestHandler<UpdateTableCardCommand, int>
{
    public Task<int> Handle(UpdateTableCardCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}