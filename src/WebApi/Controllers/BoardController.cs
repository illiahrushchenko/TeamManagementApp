using Application.Boards.Commands.AddMember;
using Application.Boards.Commands.CreateBoard;
using Application.Boards.Queries.GetBoards;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
[Authorize]
public class BoardController : ControllerBase
{
    private readonly ISender _sender;

    public BoardController(ISender sender)
    {
        _sender = sender;
    }
    
    [HttpPost()]
    public async Task<IActionResult> Create(CreateBoardCommand command)
    {
        return Ok(await _sender.Send(command));
    }
    
    [HttpPost()]
    public async Task<IActionResult> AddMember(AddMemberCommand command)
    {
        return Ok(await _sender.Send(command));
    }
    
    [HttpGet()]
    public async Task<IActionResult> GetAvailable()
    {

        var command = new GetAvailableBoardsQuery();
        return Ok(await _sender.Send(command));
    }
}