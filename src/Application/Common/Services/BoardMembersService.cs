using Application.Common.Interfaces;

namespace Application.Common.Services;

public class BoardMembersService : IBoardMembersService
{
    private readonly IIdentityService _identityService;

    public BoardMembersService(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<bool> UserIsMember(int userId, int boardId)
    {
        var user = await _identityService.FindUserByIdAsync(userId);

        return user.OtherBoards
            .Any(x => x.Id == boardId);
    }

    public async Task<bool> UserIsOwner(int userId, int boardId)
    {
        var user = await _identityService.FindUserByIdAsync(userId);

        return user.OwnBoards
            .Any(x => x.Id == boardId);
    }
}