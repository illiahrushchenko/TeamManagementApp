using Application.Boards.Queries.GetBoardDetails.Dto;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Boards.Queries.GetBoardDetails;

public record GetBoardDetailsQuery(int Id) : IRequest<BoardDetailsDto>;

public record GetBoardDetailsQueryHandler : IRequestHandler<GetBoardDetailsQuery, BoardDetailsDto>
{
    private readonly IApplicationDbContext _context;

    public GetBoardDetailsQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }


    public async Task<BoardDetailsDto> Handle(GetBoardDetailsQuery request, CancellationToken cancellationToken)
    {
        var board = await _context.Boards
                        .AsNoTracking()
                        .Include(x => x.Tables)
                            .ThenInclude(x => x.Cards)
                        .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken: cancellationToken) ??
                    throw new NotFoundException(nameof(Board), request.Id);

        var tableDtoList = new List<TableDetailsDto>();
        
        foreach (var table in board.Tables)
        {
            var tableCardDtoList = new List<TableCardDetailsDto>();
            foreach (var card in table.Cards)
            {
                tableCardDtoList.Add(new TableCardDetailsDto(card.Id, card.Title, card.Description));
            }
            tableDtoList.Add(new TableDetailsDto(table.Id, table.Title, tableCardDtoList));
        }

        return new BoardDetailsDto(board.Id, board.Title, tableDtoList);
    }
}