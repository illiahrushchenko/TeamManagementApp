using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Boards.Commands.SendInvitation;

public record SendInvitationCommand(int BoardId, int UserId) : IRequest<int>;

public class SendInvitationCommandHandler : IRequestHandler<SendInvitationCommand, int>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public SendInvitationCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<int> Handle(SendInvitationCommand request, CancellationToken cancellationToken)
    {
        var member = await _context.Members
                         .AsNoTracking()
                         .FirstOrDefaultAsync(x => x.BoardId == request.BoardId &&
                                                   x.UserId == _currentUserService.UserId, cancellationToken)??
                     throw new ForbiddenAccessException("User isn't a member of the board");

        if (!member.IsAllowedToSendInvitations)
        {
            throw new ForbiddenAccessException("Member isn't allowed to send invitations");
        }

        var invitation = new Invitation
        {
            BoardId = request.BoardId,
            UserId = request.UserId
        };
        await _context.Invitations.AddAsync(invitation, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return invitation.Id;
    }
}