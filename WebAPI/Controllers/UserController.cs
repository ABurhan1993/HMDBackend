using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using CrmBackend.Application.Handlers.UserHandlers;
using CrmBackend.Application.UserCommands;
using System.Security.Claims;

namespace CrmBackend.Web.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly GetAllUsersHandler _getAllUsersHandler;

    public UserController(GetAllUsersHandler getAllUsersHandler)
    {
        _getAllUsersHandler = getAllUsersHandler;
    }

    [HttpGet("all-users")]
    public async Task<IActionResult> GetAllUsers()
    {
        var result = await _getAllUsersHandler.Handle();
        return Ok(result);
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserCommand command, [FromServices] CreateUserCommandHandler handler)
    {
        var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        command.CreatedBy = userId;

        var newUserId = await handler.Handle(command);
        return Ok(new { UserId = newUserId });
    }
}
