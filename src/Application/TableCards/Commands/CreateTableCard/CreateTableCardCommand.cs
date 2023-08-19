using Application.Tables.Commands.CreateTable;
using MediatR;

namespace Application.TableCards.Commands.CreateTableCard;

public record CreateTableCardCommand(string Title, string Description, int TableId) : IRequest<int>;

public class CreateTableCardCommandHandler : IRequestHandler<CreateTableCardCommand, int>
{
    public Task<int> Handle(CreateTableCardCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}