namespace Application.Boards.Queries.GetBoardDetails;

public record BoardDetailsDto(int Id, string Title, IEnumerable<TableDetailsDto> Tables);