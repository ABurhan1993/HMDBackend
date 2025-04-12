using Microsoft.AspNetCore.Mvc;
using CrmBackend.Application.Commands;
using CrmBackend.Application.Handlers;
using Microsoft.AspNetCore.Authorization;

namespace CrmBackend.Web.Controllers;

[AllowAnonymous]
[ApiController]
[Route("api/[controller]")]
public class AuthenticationController : ControllerBase
{
    private readonly LoginCommandHandler _handler;

    public AuthenticationController(LoginCommandHandler handler)
    {
        _handler = handler;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginCommand command)
    {
        try
        {
            var token = await _handler.Handle(command);
            return Ok(new { Token = token });
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new { Message = ex.Message });
        }
    }
}
