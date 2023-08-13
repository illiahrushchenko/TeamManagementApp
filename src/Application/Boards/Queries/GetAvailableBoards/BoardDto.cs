using MediatR;

namespace Application.Boards.Queries.GetBoards;

public record BoardDto(int Id, string Title);