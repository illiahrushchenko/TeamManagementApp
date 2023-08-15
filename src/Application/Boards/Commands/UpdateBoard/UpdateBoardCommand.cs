using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Boards.Commands.UpdateBoard;

public record UpdateBoardCommand(int BoardId, string Title) : IRequest<int>;

public class UpdateBoardCommandHandler : IRequestHandler<UpdateBoardCommand, int>
{
    private readonly IApplicationDbContext _context;

    public UpdateBoardCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<int> Handle(UpdateBoardCommand request, CancellationToken cancellationToken)
    {
        var board = await _context.Boards
                        .FirstOrDefaultAsync(x => x.Id == request.BoardId, cancellationToken: cancellationToken) ??
                    throw new NotFoundException(nameof(Board), request.BoardId);

        board.Title = request.Title;

        await _context.SaveChangesAsync(cancellationToken);

        return board.Id;
    }
}