using Application.Boards.Commands.CreateBoard;
using Application.Boards.Commands.DeleteBoard;
using Application.TableCards.Commands.CreateTableCard;
using Application.Tables.Commands.CreateTable;
using Application.Tables.Commands.DeleteTable;
using Application.Tables.Commands.UpdateTable;
using Application.Tables.Queries.GetTable;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
[Authorize]
public class TableController : ControllerBase
{
    private readonly ISender _sender;

    public TableController(ISender sender)
    {
        _sender = sender;
    }
    
    [HttpPost()]
    public async Task<IActionResult> Create(CreateTableCommand command)
    {
        return Ok(await _sender.Send(command));
    }
    
    [HttpPut()]
    public async Task<IActionResult> Update(UpdateTableCommand command)
    {
        return Ok(await _sender.Send(command));
    }
    
    [HttpDelete()]
    public async Task<IActionResult> Delete(DeleteTableCommand command)
    {
        await _sender.Send(command);
        return Ok();
    }
    
    [HttpGet()]
    public async Task<IActionResult> Get(int id)
    {

        var command = new GetTableQuery(id);
        return Ok(await _sender.Send(command));
    }
}