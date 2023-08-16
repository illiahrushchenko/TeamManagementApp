namespace Application.Boards.Queries.GetBoardDetails;

public record TableDetailsDto(int Id, string Title, IEnumerable<TableCardDto> TableCards);