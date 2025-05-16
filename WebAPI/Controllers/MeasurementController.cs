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
    private readonly GetMyMeasurementInquiriesHandler _getMyMeasurementHandler;
    private readonly SubmitMeasurementTaskCommandHandler _submitHandler;
    private readonly GetMeasurementApprovalsHandler _getApprovalsHandler;
    private readonly ApproveMeasurementHandler _approveMeasurementHandler;
    private readonly RejectMeasurementHandler _rejectMeasurementHandler;

    public MeasurementController(
        GetMeasurementAssignmentRequestsHandler assignmentHandler,
        ApproveMeasurementAssignmentCommandHandler approveHandler,
        RejectMeasurementAssignmentCommandHandler rejectHandler,
        GetMyMeasurementInquiriesHandler getMyMeasurementHandler,
        SubmitMeasurementTaskCommandHandler submitHandler,
        GetMeasurementApprovalsHandler getApprovalsHandler,
        ApproveMeasurementHandler approveMeasurementHandler,
        RejectMeasurementHandler rejectMeasurementHandler)
    {
        _assignmentHandler = assignmentHandler;
        _approveHandler = approveHandler;
        _rejectHandler = rejectHandler;
        _getMyMeasurementHandler = getMyMeasurementHandler;
        _submitHandler = submitHandler;
        _getApprovalsHandler = getApprovalsHandler;
        _approveMeasurementHandler = approveMeasurementHandler;
        _rejectMeasurementHandler = rejectMeasurementHandler;
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

    [Authorize(Policy = PermissionConstants.Measurements.View)]
    [HttpGet("my-measurements")]
    public async Task<IActionResult> GetMyMeasurements()
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var result = await _getMyMeasurementHandler.Handle(userId);
        return Ok(result);
    }

    [HttpPost("submit-task")]
    [Authorize(Policy = PermissionConstants.Measurements.Approve)]
    public async Task<IActionResult> SubmitMeasurementTask([FromForm] SubmitMeasurementTaskCommand command)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userId))
            return Unauthorized();

        var success = await _submitHandler.Handle(command, Guid.Parse(userId));

        if (!success)
            return BadRequest("Submission failed");

        return Ok("Measurement task submitted successfully");
    }

    [Authorize(Policy = PermissionConstants.Measurements.View)]
    [HttpGet("approvals")]
    public async Task<IActionResult> GetApprovals()
    {
        var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var branchId = int.Parse(User.FindFirst("BranchId")!.Value);

        var query = new GetMeasurementApprovalsQuery
        {
            CurrentUserId = userId,
            BranchId = branchId
        };

        var result = await _getApprovalsHandler.Handle(query);
        return Ok(result);
    }

    [Authorize(Policy = PermissionConstants.Measurements.Approve)]
    [HttpPost("approve")]
    public async Task<IActionResult> Approve([FromBody] ApproveMeasurementCommand command)
    {
        command.ApprovedBy = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        await _approveMeasurementHandler.Handle(command);
        return Ok(new { message = "Measurement approved and assigned to designer." });
    }

    [Authorize(Policy = PermissionConstants.Measurements.Approve)]
    [HttpPost("reject")]
    public async Task<IActionResult> Reject([FromBody] RejectMeasurementCommand command)
    {
        command.RejectedBy = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        await _rejectMeasurementHandler.Handle(command);
        return Ok(new { message = "Measurement rejected and related entities soft deleted." });
    }
}
