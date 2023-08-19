using Application.Boards.Queries.GetBoardDetails.Dto;
using MediatR;

namespace Application.Boards.Queries.GetBoardDetails;

public record GetBoardDetailsQuery(int Id) : IRequest<BoardDetailsDto>;

public record GetBoardDetailsQueryHandler : IRequestHandler<GetBoardDetailsQuery, BoardDetailsDto>
{
    public Task<BoardDetailsDto> Handle(GetBoardDetailsQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}