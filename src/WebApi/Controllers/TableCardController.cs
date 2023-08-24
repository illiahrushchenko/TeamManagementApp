using Application.TableCards.Commands.CreateTableCard;
using Application.TableCards.Commands.DeleteTableCard;
using Application.TableCards.Commands.UpdateTableCard;
using Application.TableCards.Queries.GetTableCard;
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
public class TableCardController : ControllerBase
{
    private readonly ISender _sender;

    public TableCardController(ISender sender)
    {
        _sender = sender;
    }
    
    [HttpPost()]
    public async Task<IActionResult> Create(CreateTableCardCommand command)
    {
        return Ok(await _sender.Send(command));
    }
    
    [HttpPut()]
    public async Task<IActionResult> Update(UpdateTableCardCommand command)
    {
        return Ok(await _sender.Send(command));
    }
    
    [HttpDelete()]
    public async Task<IActionResult> Delete(DeleteTableCardCommand command)
    {
        await _sender.Send(command);
        return Ok();
    }
    
    [HttpGet()]
    public async Task<IActionResult> Get(int id)
    {

        var command = new GetTableCardQuery(id);
        return Ok(await _sender.Send(command));
    }
}