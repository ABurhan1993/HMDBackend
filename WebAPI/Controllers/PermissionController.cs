using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CrmBackend.Domain.Constants;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class PermissionController : ControllerBase
{
    [Authorize(Policy = PermissionConstants.Roles.View)] // 🔥 نربطه بصلاحية عرض الأدوار لأن permissions مرتبطة بالأدوار
    [HttpGet("all")]
    public IActionResult GetAllPermissions()
    {
        return Ok(PermissionConstants.All);
    }
}
