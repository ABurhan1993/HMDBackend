using Microsoft.AspNetCore.Mvc;
using CrmBackend.Application.Commands.CustomerCommands;
using CrmBackend.Application.Handlers.CustomerHandlers;
using Microsoft.AspNetCore.Authorization;
using CrmBackend.Domain.Enums;
using CrmBackend.Domain.Constants;
using CrmBackend.Application.Handlers;
using System.Security.Claims;
using CrmBackend.Application.CustomerHandlers;
using CrmBackend.Application.DTOs.CustomersDTOs;
using CrmBackend.Domain.Services;

namespace CrmBackend.Web.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class CustomerController : ControllerBase
{
    private readonly CreateCustomerCommandHandler _createHandler;
    private readonly UpdateCustomerAssignmentHandler _updateAssignHandler;
    private readonly AddCustomerCommentHandler _addCommentHandler;
    private readonly UpdateCustomerHandler _updateCustomerHandler;
    private readonly GetAllCustomersHandler _getAllHandler;
    private readonly GetCustomerByIdHandler _getByIdHandler;
    private readonly GetCustomersByContactStatusHandler _getByStatusHandler;
    private readonly GetCustomersByWayOfContactHandler _getByWayHandler;
    private readonly GetCustomersByAssignedToIdHandler _getByAssignHandler;
    private readonly DeleteCustomerHandler _deleteCustomerHandler;
    private readonly ICustomerCommentRepository _customerCommentRepository;
    public CustomerController(
        CreateCustomerCommandHandler createHandler,
        UpdateCustomerAssignmentHandler updateAssignHandler,
        AddCustomerCommentHandler addCommentHandler,
        UpdateCustomerHandler updateCustomerHandler,
        GetAllCustomersHandler getAllHandler,
        GetCustomerByIdHandler getByIdHandler,
        GetCustomersByContactStatusHandler getByStatusHandler,
        GetCustomersByWayOfContactHandler getByWayHandler,
        GetCustomersByAssignedToIdHandler getByAssignHandler,
        DeleteCustomerHandler deleteCustomerHandler,
        ICustomerCommentRepository customerCommentRepository)
    {
        _createHandler = createHandler;
        _updateAssignHandler = updateAssignHandler;
        _addCommentHandler = addCommentHandler;
        _updateCustomerHandler = updateCustomerHandler;
        _getAllHandler = getAllHandler;
        _getByIdHandler = getByIdHandler;
        _getByStatusHandler = getByStatusHandler;
        _getByWayHandler = getByWayHandler;
        _getByAssignHandler = getByAssignHandler;
        _deleteCustomerHandler = deleteCustomerHandler;
        _customerCommentRepository = customerCommentRepository;
    }

    [Authorize(Policy = PermissionConstants.Customers.Create)]
    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] CreateCustomerCommand command)
    {
        var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        command.CreatedBy = userId;
        var id = await _createHandler.Handle(command);
        return Ok(new { CustomerId = id });
    }

    [Authorize(Policy = PermissionConstants.Customers.Edit)]
    [HttpPut("update")]
    public async Task<IActionResult> Update([FromBody] UpdateCustomerCommand command)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId)) return Unauthorized();

        command.UpdatedBy = Guid.Parse(userId);

        await _updateCustomerHandler.Handle(command);
        return Ok();
    }


    [Authorize(Policy = PermissionConstants.Customers.Edit)]
    [HttpPut("assign")]
    public async Task<IActionResult> Assign([FromBody] UpdateCustomerAssignmentCommand command)
    {
        await _updateAssignHandler.Handle(command);
        return Ok(new { Message = "Customer reassigned successfully" });
    }

    [Authorize(Policy = PermissionConstants.CustomerComments.Create)]
    [HttpPost("add-comment")]
    public async Task<IActionResult> AddComment([FromBody] AddCustomerCommentCommand command)
    {
        var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        command.CommentAddedBy = userId;
        await _addCommentHandler.Handle(command);
        return Ok(new { Message = "Comment added successfully" });
    }

    [Authorize(Policy = PermissionConstants.Customers.View)]
    [HttpGet("all")]
    public async Task<IActionResult> GetAll()
    {
        var branchId = int.Parse(User.FindFirst("BranchId")!.Value);
        var result = await _getAllHandler.Handle(branchId);
        return Ok(result);
    }

    [Authorize(Policy = PermissionConstants.Customers.View)]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _getByIdHandler.Handle(id);
        return Ok(result);
    }

    [Authorize(Policy = PermissionConstants.Customers.View)]
    [HttpGet("by-status/{status}")]
    public async Task<IActionResult> GetByContactStatus(int status)
    {
        var branchId = int.Parse(User.FindFirst("BranchId")!.Value);
        var result = await _getByStatusHandler.Handle((ContactStatus)status, branchId);
        return Ok(result);
    }


    [Authorize(Policy = PermissionConstants.Customers.View)]
    [HttpGet("by-way/{way}")]
    public async Task<IActionResult> GetByWayOfContact(int way)
    {
        var branchId = int.Parse(User.FindFirst("BranchId")!.Value);
        var result = await _getByWayHandler.Handle((WayOfContact)way, branchId);
        return Ok(result);
    }


    [Authorize(Policy = PermissionConstants.Customers.View)]
    [HttpGet("by-assigned/{assignedToId}")]
    public async Task<IActionResult> GetByAssignedTo(Guid assignedToId)
    {
        var branchId = int.Parse(User.FindFirst("BranchId")!.Value);
        var result = await _getByAssignHandler.Handle(assignedToId, branchId);
        return Ok(result);
    }


    [HttpDelete("{id}")]
    [Authorize(Policy = PermissionConstants.Customers.Delete)]
    public async Task<IActionResult> Delete([FromQuery]int id)
    {
        var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);// استخرج من التوكن
        var command = new DeleteCustomerCommand
        {
            CustomerId = id,
            DeletedBy = userId
        };

        await _deleteCustomerHandler.Handle(command);
        return Ok();
    }

    [HttpGet("calendar-events")]
    [Authorize(Policy = PermissionConstants.Customers.View)]
    public async Task<IActionResult> GetCalendarEvents([FromServices] GetCustomersWithUpcomingMeetingsHandler handler)
    {
        var branchId = int.Parse(User.FindFirst("BranchId")!.Value);
        var result = await handler.HandleAsync(branchId);
        return Ok(result);
    }


    [HttpGet("comments/{customerId}")]
    public async Task<IActionResult> GetCustomerComments(int customerId)
    {
        var comments = await _customerCommentRepository.GetByCustomerIdAsync(customerId);

        var result = comments
            .OrderByDescending(c => c.CreatedDate)
            .Select(c => new CustomerCommentDto
            {
                Comment = c.CustomerCommentDetail,
                CreatedDate = (DateTime)c.CreatedDate,
                AddedBy = c.CommentAddedByNavigation.FullName,
            })
            .ToList();

        return Ok(result);
    }


}
