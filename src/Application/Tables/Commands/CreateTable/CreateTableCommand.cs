using MediatR;

namespace Application.Tables.Commands.CreateTable;

public record CreateTableCommand : IRequest<int>;

public class CreateTableCommandHandler : IRequestHandler<CreateTableCommand, int>
{
    public Task<int> Handle(CreateTableCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}