using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.TableCards.Commands.CreateTableCard;

public record CreateTableCardCommand(string Title, string Description, int TableId) : IRequest<int>;

public class CreateTableCardCommandHandler : IRequestHandler<CreateTableCardCommand, int>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;
    private readonly IBoardMembersService _boardMembersService;

    public CreateTableCardCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService, IBoardMembersService boardMembersService)
    {
        _context = context;
        _currentUserService = currentUserService;
        _boardMembersService = boardMembersService;
    }

    public async Task<int> Handle(CreateTableCardCommand request, CancellationToken cancellationToken)
    {
        var table = await _context.Tables
                        .AsNoTracking()
                        .FirstOrDefaultAsync(x => x.Id == request.TableId, cancellationToken: cancellationToken) ??
                    throw new NotFoundException(nameof(Table), request.TableId);

        if (!await _boardMembersService.UserIsOwner(_currentUserService.UserId, table.Id) &&
            !await _boardMembersService.UserIsMember(_currentUserService.UserId, table.Id))
        {
            throw new ForbiddenAccessException(nameof(Table), table.Id);
        }
        
        var tableCard = new TableCard
        {
            Title = request.Title,
            Description = request.Description,
            TableId = request.TableId,
            AddedById = _currentUserService.UserId
        };

        await _context.TableCards.AddAsync(tableCard, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return tableCard.Id;
    }
}