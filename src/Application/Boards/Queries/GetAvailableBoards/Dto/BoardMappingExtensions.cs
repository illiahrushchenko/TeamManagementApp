using Domain.Entities;

namespace Application.Boards.Queries.GetAvailableBoards.Dto;

public static class BoardMappingExtensions
{
    public static BoardDto Map(this Board board)
    {
        if (board.Title is null) throw new ArgumentNullException(nameof(board.Title));
        
        return new BoardDto(board.Id, board.Title);
    }
}