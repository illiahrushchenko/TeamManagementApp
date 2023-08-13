using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Boards.Commands.AddMember;

public record AddMemberCommand(int BoardId, string Email) : IRequest<int>;

public class AddMemberCommandHandler : IRequestHandler<AddMemberCommand, int>
{
    private readonly IIdentityService _identityService;
    private readonly IApplicationDbContext _context;

    public AddMemberCommandHandler(IIdentityService identityService, IApplicationDbContext context)
    {
        _identityService = identityService;
        _context = context;
    }
    
    public async Task<int> Handle(AddMemberCommand request, CancellationToken cancellationToken)
    {
        var board = await _context.Boards
                        .FirstOrDefaultAsync(x => x.Id == request.BoardId, cancellationToken: cancellationToken) ??
                    throw new NotFoundException(nameof(Board), request.BoardId);

        var user = await _identityService.FindUserByEmailAsync(request.Email) ??
                   throw new NotFoundException(nameof(User), request.Email);
        
        board.Members.Add(user);
        await _context.SaveChangesAsync(cancellationToken);

        return board.Id;
    }
}