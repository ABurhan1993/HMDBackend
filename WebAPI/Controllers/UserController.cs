using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using CrmBackend.Application.Handlers.UserHandlers;

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
}
