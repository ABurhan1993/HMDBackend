using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using CrmBackend.Application.Handlers.UserHandlers;
using CrmBackend.Application.UserCommands;
using CrmBackend.Application.Commands.UserCommands;
using CrmBackend.Domain.Constants;
using System.Security.Claims;

namespace CrmBackend.Web.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly GetAllUsersHandler _getAllUsersHandler;
    private readonly GetUsersByBranchIdHandler _getUsersByBranchIdHandler;
    private readonly ResetUserPasswordHandler _resetUserPasswordHandler;

    public UserController(
        GetAllUsersHandler getAllUsersHandler,
        GetUsersByBranchIdHandler getUsersByBranchIdHandler,
        ResetUserPasswordHandler resetUserPasswordHandler)
    {
        _getAllUsersHandler = getAllUsersHandler;
        _getUsersByBranchIdHandler = getUsersByBranchIdHandler;
        _resetUserPasswordHandler = resetUserPasswordHandler;
    }

    [Authorize(Policy = PermissionConstants.Users.View)] // 🔥 عرض جميع المستخدمين
    [HttpGet("all-users")]
    public async Task<IActionResult> GetAllUsers()
    {
        var result = await _getAllUsersHandler.Handle();
        return Ok(result);
    }

    [Authorize(Policy = PermissionConstants.Users.Create)] // 🔥 إنشاء مستخدم
    [HttpPost("create")]
    public async Task<IActionResult> CreateUser(
        [FromBody] CreateUserCommand command,
        [FromServices] CreateUserCommandHandler handler)
    {
        var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        command.CreatedBy = userId;

        var newUserId = await handler.Handle(command);
        return Ok(new { UserId = newUserId });
    }

    [Authorize(Policy = "UsersOrCustomersView")]// 🔥 عرض مستخدمي الفرع
    [HttpGet("by-branch")]
    public async Task<IActionResult> GetUsersInMyBranch()
    {
        var branchIdStr = User.FindFirst("BranchId")?.Value;
        if (string.IsNullOrEmpty(branchIdStr) || !int.TryParse(branchIdStr, out var branchId))
            return Unauthorized("Invalid branch ID in token.");

        var result = await _getUsersByBranchIdHandler.Handle(branchId);
        return Ok(result);
    }

    [Authorize(Policy = PermissionConstants.Users.Edit)] // 🔥 إعادة تعيين كلمة مرور مستخدم
    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword([FromBody] ResetUserPasswordCommand command)
    {
        await _resetUserPasswordHandler.Handle(command);
        return Ok();
    }
}
