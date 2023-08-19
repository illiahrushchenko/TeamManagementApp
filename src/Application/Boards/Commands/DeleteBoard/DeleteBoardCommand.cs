using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Boards.Commands.DeleteBoard;

public record DeleteBoardCommand(int Id) : IRequest;

public class DeleteBoardCommandHandler : IRequestHandler<DeleteBoardCommand>
{
    
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;
    private readonly IBoardMembersService _boardMembersService;

    public DeleteBoardCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService, IBoardMembersService boardMembersService)
    {
        _context = context;
        _currentUserService = currentUserService;
        _boardMembersService = boardMembersService;
    }

    public async Task Handle(DeleteBoardCommand request, CancellationToken cancellationToken)
    {
        var board = await _context.Boards
                        .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken: cancellationToken) ??
                    throw new NotFoundException(nameof(Board), request.Id);

        if (!await _boardMembersService.UserIsOwner(_currentUserService.UserId, request.Id))
        {
            throw new ForbiddenAccessException(nameof(Board), request.Id);
        }

        _context.Boards.Remove(board);
        await _context.SaveChangesAsync(cancellationToken);
    }
}