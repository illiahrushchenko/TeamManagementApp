using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.TableCards.Queries.GetTableCard;

public record GetTableCardQuery(int Id) : IRequest<TableCardDetailsDto>;

public class GetTableCardQueryHandler : IRequestHandler<GetTableCardQuery, TableCardDetailsDto>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;
    private readonly IBoardMembersService _boardMembersService;

    public GetTableCardQueryHandler(IApplicationDbContext context, ICurrentUserService currentUserService, IBoardMembersService boardMembersService)
    {
        _context = context;
        _currentUserService = currentUserService;
        _boardMembersService = boardMembersService;
    }

    public async Task<TableCardDetailsDto> Handle(GetTableCardQuery request, CancellationToken cancellationToken)
    {
        var tableCard = await _context.TableCards
                            .AsNoTracking()
                            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken: cancellationToken) ??
                        throw new NotFoundException(nameof(TableCard), request.Id);

        return new TableCardDetailsDto(tableCard.Id, tableCard.Title, tableCard.Description);
    }
}