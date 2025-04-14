using CrmBackend.Application.Commands.RoleCommands;
using CrmBackend.Application.Handlers.RoleHandlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class RoleController : ControllerBase
{
    private readonly CreateRoleCommandHandler _createHandler;
    private readonly UpdateRoleCommandHandler _updateHandler;
    private readonly GetAllRolesHandler _getAllHandler;

    public RoleController(
        CreateRoleCommandHandler createHandler,
        UpdateRoleCommandHandler updateHandler,
        GetAllRolesHandler getAllHandler)
    {
        _createHandler = createHandler;
        _updateHandler = updateHandler;
        _getAllHandler = getAllHandler;
    }

    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] CreateRoleCommand command)
    {
        var id = await _createHandler.Handle(command);
        return Ok(new { RoleId = id });
    }

    [HttpPut("update")]
    public async Task<IActionResult> Update([FromBody] UpdateRoleCommand command)
    {
        await _updateHandler.Handle(command);
        return Ok();
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAll()
    {
        var result = await _getAllHandler.Handle();
        return Ok(result);
    }
}
