using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.TableCards.Commands.DeleteTableCard;

public record DeleteTableCardCommand(int Id) : IRequest;

public class DeleteTableCardCommandHandler : IRequestHandler<DeleteTableCardCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;
    private readonly IBoardMembersService _boardMembersService;

    public DeleteTableCardCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService, IBoardMembersService boardMembersService)
    {
        _context = context;
        _currentUserService = currentUserService;
        _boardMembersService = boardMembersService;
    }

    public async Task Handle(DeleteTableCardCommand request, CancellationToken cancellationToken)
    {
        var tableCard = await _context.TableCards
                            .Include(x => x.Table)
                            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken: cancellationToken) ??
                        throw new NotFoundException(nameof(TableCard), request.Id);

        if (!await _boardMembersService.UserIsOwner(_currentUserService.UserId, tableCard.Table.BoardId) && 
            !await _boardMembersService.UserIsMember(_currentUserService.UserId, tableCard.Table.BoardId))
        {
            throw new ForbiddenAccessException(nameof(Table), request.Id);
        }

        _context.TableCards.Remove(tableCard);
        await _context.SaveChangesAsync(cancellationToken);
    }
}