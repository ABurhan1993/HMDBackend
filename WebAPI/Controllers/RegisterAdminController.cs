using CrmBackend.Application.Commands;
using CrmBackend.Application.Handlers;
using Microsoft.AspNetCore.Mvc;

namespace MhdCRM.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RegisterAdminController : ControllerBase
{
    private readonly RegisterAdminCommandHandler _handler;

    public RegisterAdminController(RegisterAdminCommandHandler handler)
    {
        _handler = handler;
    }

    [HttpPost]
    public async Task<IActionResult> Register([FromBody] RegisterAdminCommand command)
    {
        await _handler.Handle(command);
        return Ok("Admin user created successfully.");
    }
}
