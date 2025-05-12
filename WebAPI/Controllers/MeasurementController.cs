using CrmBackend.Application.Commands.MeasurementCommands;
using CrmBackend.Application.Handlers.MesurementHandlers;
using CrmBackend.Domain.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CrmBackend.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MeasurementController : ControllerBase
{
    private readonly GetMeasurementAssignmentRequestsHandler _assignmentHandler;
    private readonly ApproveMeasurementAssignmentCommandHandler _approveHandler;
    private readonly RejectMeasurementAssignmentCommandHandler _rejectHandler;

    public MeasurementController(GetMeasurementAssignmentRequestsHandler assignmentHandler,ApproveMeasurementAssignmentCommandHandler approveHandler, RejectMeasurementAssignmentCommandHandler rejectHandler)
    {
        _assignmentHandler = assignmentHandler;
        _approveHandler = approveHandler;
        _rejectHandler = rejectHandler;
    }

    [HttpGet("assignment-requests")]
    [Authorize(Policy = PermissionConstants.Measurements.View)]
    public async Task<IActionResult> GetAssignmentRequests()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null)
            return Unauthorized();

        var result = await _assignmentHandler.Handle(new GetMeasurementAssignmentRequestsQuery
        {
            UserId = Guid.Parse(userId)
        });

        return Ok(result);
    }

    [Authorize(Policy = PermissionConstants.Measurements.Approve)]
    [HttpPost("assignment/approve")]
    public async Task<IActionResult> ApproveAssignee([FromBody] ApproveMeasurementAssignmentCommand command)
    {
        var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var result = await _approveHandler.Handle(command, userId);

        return result ? Ok() : BadRequest("Approval failed");
    }

    [Authorize(Policy = PermissionConstants.Measurements.Approve)]
    [HttpPost("assignment/reject")]
    public async Task<IActionResult> RejectAssginee([FromBody] RejectMeasurementAssignmentCommand command)
    {
        var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var result = await _rejectHandler.Handle(command, userId);

        return result ? Ok() : BadRequest("Rejection failed");
    }

}
