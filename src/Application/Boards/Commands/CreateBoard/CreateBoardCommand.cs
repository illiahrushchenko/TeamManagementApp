using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Boards.Commands.CreateBoard;

public record CreateBoardCommand(string Title) : IRequest<int>;

public class CreateBoardCommandHandler : IRequestHandler<CreateBoardCommand, int>
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IApplicationDbContext _context;
    

    public CreateBoardCommandHandler(ICurrentUserService currentUserService,
        IApplicationDbContext context)
    {
        _currentUserService = currentUserService;
        _context = context;
    }
    
    public async Task<int> Handle(CreateBoardCommand request, CancellationToken cancellationToken)
    {
        var board = new Board
        {
            Title = request.Title,
            OwnerId = _currentUserService.UserId
        };

        await _context.Boards.AddAsync(board, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return board.Id;
    }
}