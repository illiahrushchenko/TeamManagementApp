namespace Application.Common.Models;

public record TableDetailsDto(int Id, string Title, IEnumerable<TableCardDetailsDto> TableCards);