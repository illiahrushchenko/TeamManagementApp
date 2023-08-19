using Application.Common.Models;

namespace Application.Boards.Queries.GetBoardDetails.Dto;

public record BoardDetailsDto(int Id, string Title, IEnumerable<TableDetailsDto> Tables);