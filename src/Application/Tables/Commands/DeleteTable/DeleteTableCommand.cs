using MediatR;

namespace Application.Tables.Commands.RemoveTable;

public record DeleteTableCommand : IRequest;

public class DeleteTableCommandHandler : IRequestHandler<DeleteTableCommand>
{
    public Task Handle(DeleteTableCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}