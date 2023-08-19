using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Tables.Commands.DeleteTable;

public record DeleteTableCommand(int Id) : IRequest;

public class DeleteTableCommandHandler : IRequestHandler<DeleteTableCommand>
{

    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;
    private readonly IBoardMembersService _boardMembersService;

    public DeleteTableCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService, IBoardMembersService boardMembersService)
    {
        _context = context;
        _currentUserService = currentUserService;
        _boardMembersService = boardMembersService;
    }

    public async Task Handle(DeleteTableCommand request, CancellationToken cancellationToken)
    {
        var table = await _context.Tables
                        .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken: cancellationToken) ??
                    throw new NotFoundException(nameof(Table), request.Id);

        if (!await _boardMembersService.UserIsOwner(_currentUserService.UserId, table.BoardId))
        {
            throw new ForbiddenAccessException(nameof(Table), request.Id);
        }

        _context.Tables.Remove(table);
        await _context.SaveChangesAsync(cancellationToken);
    }
}