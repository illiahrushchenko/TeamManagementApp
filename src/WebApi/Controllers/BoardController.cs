using Application.Boards.Commands.AddMember;
using Application.Boards.Commands.CreateBoard;
using Application.Boards.Commands.DeleteBoard;
using Application.Boards.Commands.UpdateBoard;
using Application.Boards.Queries.GetAvailableBoards;
using Application.Boards.Queries.GetBoardDetails;
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
    
    [HttpDelete()]
    public async Task<IActionResult> Delete(DeleteBoardCommand command)
    {
        await _sender.Send(command);
        return Ok();
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
    
    [HttpGet()]
    public async Task<IActionResult> Get(int id)
    {

        var command = new GetBoardDetailsQuery(id);
        return Ok(await _sender.Send(command));
    }

    [HttpPut()]
    public async Task<IActionResult> Update(UpdateBoardCommand command)
    {
        return Ok(await _sender.Send(command));
    }
}