using Application.Common.Models;
using MediatR;

namespace Application.Tables.Queries.GetTable;

public record GetTableQuery() : IRequest<TableDetailsDto>;

public class GetTableQueryHandler : IRequestHandler<GetTableQuery, TableDetailsDto>
{
    public Task<TableDetailsDto> Handle(GetTableQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}