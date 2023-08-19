using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Tables.Commands.CreateTable;

public record CreateTableCommand(string Title, int BoardId) : IRequest<int>;

public class CreateTableCommandHandler : IRequestHandler<CreateTableCommand, int>
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IApplicationDbContext _context;
    private readonly IBoardMembersService _boardMembersService;

    public CreateTableCommandHandler(ICurrentUserService currentUserService, IApplicationDbContext context, IBoardMembersService boardMembersService)
    {
        _currentUserService = currentUserService;
        _context = context;
        _boardMembersService = boardMembersService;
    }

    public async Task<int> Handle(CreateTableCommand request, CancellationToken cancellationToken)
    {
        var board = await _context.Boards
                        .AsNoTracking()
                        .FirstOrDefaultAsync(x => x.Id == request.BoardId, cancellationToken: cancellationToken) ??
                    throw new NotFoundException(nameof(Board), request.BoardId);

        if (!await _boardMembersService.UserIsOwner(_currentUserService.UserId, board.Id))
        {
            throw new ForbiddenAccessException(nameof(Board), board.Id);
        }
        
        var table = new Table
        {
            Title = request.Title,
            BoardId = request.BoardId
        };

        await _context.Tables.AddAsync(table, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return table.Id;
    }
}