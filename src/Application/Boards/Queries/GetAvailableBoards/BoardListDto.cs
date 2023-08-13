namespace Application.Boards.Queries.GetBoards;

public record BoardListDto(IEnumerable<BoardDto> OwnBoards, IEnumerable<BoardDto> OtherBoards);