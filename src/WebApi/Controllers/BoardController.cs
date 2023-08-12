using Application.Boards.Commands.CreateBoard;
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
}