namespace Application.Common.Interfaces;

public interface IBoardMembersService
{
    Task<bool> UserIsMember(int userId, int boardId);
    Task<bool> UserIsOwner(int userId, int boardId);
}