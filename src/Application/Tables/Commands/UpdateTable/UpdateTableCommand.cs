using MediatR;

namespace Application.Tables.Commands.UpdateTable;

public record UpdateTableCommand() : IRequest<int>;

public class UpdateTableCommandHandler : IRequestHandler<UpdateTableCommand, int>
{
    public Task<int> Handle(UpdateTableCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}