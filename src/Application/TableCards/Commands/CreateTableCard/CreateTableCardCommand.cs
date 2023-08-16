using Application.Tables.Commands.CreateTable;
using MediatR;

namespace Application.TableCards.Commands.CreateTableCard;

public record CreateTableCardCommand() : IRequest<int>;

public class CreateTableCardCommandHandler : IRequestHandler<CreateTableCommand, int>
{
    public Task<int> Handle(CreateTableCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}