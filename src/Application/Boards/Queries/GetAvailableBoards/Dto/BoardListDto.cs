namespace Application.Boards.Queries.GetAvailableBoards.Dto;

public record BoardListDto(IEnumerable<BoardDto> OwnBoards, IEnumerable<BoardDto> OtherBoards);