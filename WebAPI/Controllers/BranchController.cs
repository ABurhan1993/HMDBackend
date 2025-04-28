using CrmBackend.Application.Commands.BranchCommands;
using CrmBackend.Application.Handlers.BranchHandlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CrmBackend.Domain.Constants; // ✅ لازم استيراد الكونستانت تبع البرميشن

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class BranchController : ControllerBase
{
    private readonly CreateBranchCommandHandler _createHandler;
    private readonly UpdateBranchCommandHandler _updateHandler;
    private readonly GetAllBranchesHandler _getAllHandler;

    public BranchController(
        CreateBranchCommandHandler createHandler,
        UpdateBranchCommandHandler updateHandler,
        GetAllBranchesHandler getAllHandler)
    {
        _createHandler = createHandler;
        _updateHandler = updateHandler;
        _getAllHandler = getAllHandler;
    }

    [Authorize(Policy = PermissionConstants.Branches.Create)] // 🔥 صلاحية إنشاء فرع
    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] CreateBranchCommand command)
    {
        var id = await _createHandler.Handle(command);
        return Ok(new { BranchId = id });
    }

    [Authorize(Policy = PermissionConstants.Branches.Edit)] // 🔥 صلاحية تعديل فرع
    [HttpPut("update")]
    public async Task<IActionResult> Update([FromBody] UpdateBranchCommand command)
    {
        await _updateHandler.Handle(command);
        return Ok();
    }

    [Authorize(Policy = PermissionConstants.Branches.View)] // 🔥 صلاحية عرض الفروع
    [HttpGet("all")]
    public async Task<IActionResult> GetAll()
    {
        var result = await _getAllHandler.Handle();
        return Ok(result);
    }
}
