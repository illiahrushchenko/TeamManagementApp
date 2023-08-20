using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Tables.Queries.GetTable;

public record GetTableQuery(int Id) : IRequest<TableDetailsDto>;

public class GetTableQueryHandler : IRequestHandler<GetTableQuery, TableDetailsDto>
{
    private readonly IApplicationDbContext _context;

    public GetTableQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<TableDetailsDto> Handle(GetTableQuery request, CancellationToken cancellationToken)
    {
        var table = await _context.Tables
                        .AsNoTracking()
                        .Include(x => x.Cards)
                        .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken: cancellationToken) ??
                    throw new NotFoundException(nameof(Table), request.Id);

        var cardDtoList = new List<TableCardDetailsDto>();

        foreach (var card in table.Cards)
        {
            cardDtoList.Add(new TableCardDetailsDto(card.Id, card.Title, card.Description));
        }

        return new TableDetailsDto(table.Id, table.Title, cardDtoList);
    }
}