using Application.Common.Models;
using MediatR;

namespace Application.TableCards.Queries.GetTableCard;

public record GetTableCardQuery() : IRequest<TableCardDetailsDto>;

public class GetTableCardQueryHandler : IRequestHandler<GetTableCardQuery, TableCardDetailsDto>
{
    public Task<TableCardDetailsDto> Handle(GetTableCardQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}