using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Boards.Queries.GetBoards;

public record GetAvailableBoardsQuery : IRequest<BoardListDto>;

public class GetAvailableBoardsQueryHandler : IRequestHandler<GetAvailableBoardsQuery, BoardListDto>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;
    
    public GetAvailableBoardsQueryHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }
    
    public async Task<BoardListDto> Handle(GetAvailableBoardsQuery request, CancellationToken cancellationToken)
    {
        var user = await _context.Users
                       .Include(x => x.OwnBoards)
                       .Include(x => x.OtherBoards)
                       .FirstOrDefaultAsync(x => x.Id == _currentUserService.UserId,
                           cancellationToken: cancellationToken) ??
                   throw new NotFoundException(nameof(User), _currentUserService.UserId);

        var ownBoards = user.OwnBoards.Select(x => x.Map());
        var otherBoards = user.OtherBoards.Select(x => x.Map());

        return new BoardListDto(ownBoards, otherBoards);
    }
}

