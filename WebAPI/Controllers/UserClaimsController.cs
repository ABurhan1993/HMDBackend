using CrmBackend.Application.Commands.UserClaimCommands;
using CrmBackend.Application.Handlers.UserClaimHandlers;
using CrmBackend.Domain.Constants;
using CrmBackend.Domain.Entities;
using CrmBackend.Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class UserClaimController : ControllerBase
{
    private readonly AddUserClaimHandler _addHandler;
    private readonly DeleteUserClaimHandler _deleteHandler;
    private readonly IUserClaimRepository _repo; // نبقيه فقط للعرض

    public UserClaimController(
    IUserClaimRepository repo,
    AddUserClaimHandler addHandler,
    DeleteUserClaimHandler deleteHandler)
    {
        _repo = repo;
        _addHandler = addHandler;
        _deleteHandler = deleteHandler;
    }

    [Authorize(Policy = PermissionConstants.UserClaims.View)]
    [HttpGet("by-user/{userId}")]
    public async Task<IActionResult> GetClaims(Guid userId)
    {
        var claims = await _repo.GetClaimsByUserIdAsync(userId);
        return Ok(claims);
    }

    [Authorize(Policy = PermissionConstants.UserClaims.Create)]
    [HttpPost("add")]
    public async Task<IActionResult> AddClaim([FromBody] AddUserClaimCommand command)
    {
        await _addHandler.HandleAsync(command);
        return Ok();
    }


    [Authorize(Policy = PermissionConstants.UserClaims.Delete)]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteClaim(int id)
    {
        var command = new DeleteUserClaimCommand { Id = id };
        await _deleteHandler.HandleAsync(command);
        return Ok();
    }


    [Authorize(Policy = PermissionConstants.UserClaims.View)]
    [HttpGet("all-permissions")]
    public IActionResult GetAllPermissions()
    {
        var result = new Dictionary<string, string[]>
    {
        { "Customers", PermissionConstants.Customers.All },
        { "Users", PermissionConstants.Users.All },
        { "Branches", PermissionConstants.Branches.All },
        { "Roles", PermissionConstants.Roles.All },
        { "CustomerComments", PermissionConstants.CustomerComments.All },
        { "Inquiries", PermissionConstants.Inquiries.All },
        { "UserClaims", PermissionConstants.UserClaims.All }
    };

        return Ok(result);
    }

    [Authorize(Policy = PermissionConstants.UserClaims.View)]
    [HttpGet("all-permissions-flat")]
    public IActionResult GetAllPermissionsFlat()
    {
        return Ok(PermissionConstants.All);
    }
}

