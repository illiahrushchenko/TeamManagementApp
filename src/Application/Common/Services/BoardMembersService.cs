using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Services;

public class BoardMembersService : IBoardMembersService
{
    private readonly IApplicationDbContext _context;

    public BoardMembersService(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> UserIsMember(int userId, int boardId)
    {
        var user = await _context.Users
                       .Include(x => x.OtherBoards)
                       .FirstOrDefaultAsync(x => x.Id == userId) ??
                   throw new NotFoundException(nameof(User), userId);

        return user.OtherBoards
            .Any(x => x.Id == boardId);
    }

    public async Task<bool> UserIsOwner(int userId, int boardId)
    {
        var user = await _context.Users
                       .Include(x => x.OwnBoards)
                       .FirstOrDefaultAsync(x => x.Id == userId) ??
                   throw new NotFoundException(nameof(User), userId);

        return user.OwnBoards
            .Any(x => x.Id == boardId);
    }
}