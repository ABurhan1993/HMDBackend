using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using CrmBackend.Application.Handlers.UserHandlers;
using CrmBackend.Application.UserCommands;
using System.Security.Claims;
using CrmBackend.Application.Commands.UserCommands;

namespace CrmBackend.Web.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly GetAllUsersHandler _getAllUsersHandler;
    private readonly GetUsersByBranchIdHandler _getUsersByBranchIdHandler;

    public UserController(GetAllUsersHandler getAllUsersHandler, GetUsersByBranchIdHandler getUsersByBranchIdHandler)
    {
        _getAllUsersHandler = getAllUsersHandler;
        _getUsersByBranchIdHandler = getUsersByBranchIdHandler;
    }

    [HttpGet("all-users")]
    public async Task<IActionResult> GetAllUsers()
    {
        var branchId = int.Parse(User.FindFirst("BranchId")!.Value);
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

    [HttpGet("by-branch")]
    public async Task<IActionResult> GetUsersInMyBranch()
    {
        var branchIdStr = User.FindFirst("BranchId")?.Value;

        if (string.IsNullOrEmpty(branchIdStr) || !int.TryParse(branchIdStr, out var branchId))
            return Unauthorized("Invalid branch ID in token.");

        var result = await _getUsersByBranchIdHandler.Handle(branchId);
        return Ok(result);
    }



}
