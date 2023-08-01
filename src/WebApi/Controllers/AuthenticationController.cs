using Application.Users.Commands.CreateUser;
using Application.Users.Queries.GetAuthToken;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class AuthenticationController : ControllerBase
{
    private readonly ISender _sender;

    public AuthenticationController(ISender sender)
    {
        _sender = sender;
    }
    
    [HttpPost()]
    public async Task<IActionResult> Login(GetAuthTokenQuery query)
    {
        return Ok(await _sender.Send(query));
    }
    
    [HttpPost()]
    public async Task<IActionResult> Register(CreateUserCommand command)
    {
        return Ok(await _sender.Send(command));
    }

    [Authorize]
    [HttpGet()]
    public IActionResult Protected()
    {
        return Ok("Protected");
    }
}