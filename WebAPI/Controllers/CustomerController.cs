using Microsoft.AspNetCore.Mvc;
using CrmBackend.Application.Commands.CustomerCommands;
using CrmBackend.Application.Handlers.CustomerHandlers;
using Microsoft.AspNetCore.Authorization;
using CrmBackend.Application.Handlers;
using CrmBackend.Domain.Enums;

namespace CrmBackend.Web.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class CustomerController : ControllerBase
{
    private readonly CreateCustomerCommandHandler _createHandler;
    private readonly UpdateCustomerAssignmentHandler _updateAssignHandler;
    private readonly AddCustomerCommentHandler _addCommentHandler;
    private readonly UpdateCustomerHandler _updateHandler;
    private readonly GetAllCustomersHandler _getAllHandler;
    private readonly GetCustomerByIdHandler _getByIdHandler;
    private readonly GetCustomersByContactStatusHandler _getByStatusHandler;
    private readonly GetCustomersByWayOfContactHandler _getByWayHandler;
    private readonly GetCustomersByAssignedToIdHandler _getByAssignHandler;

    public CustomerController(
        CreateCustomerCommandHandler createHandler,
        UpdateCustomerAssignmentHandler updateAssignHandler,
        AddCustomerCommentHandler addCommentHandler,
        UpdateCustomerHandler updateHandler,
        GetAllCustomersHandler getAllHandler,
        GetCustomerByIdHandler getByIdHandler,
        GetCustomersByContactStatusHandler getByStatusHandler,
        GetCustomersByWayOfContactHandler getByWayHandler,
        GetCustomersByAssignedToIdHandler getByAssignHandler)
    {
        _createHandler = createHandler;
        _updateAssignHandler = updateAssignHandler;
        _addCommentHandler = addCommentHandler;
        _updateHandler = updateHandler;
        _getAllHandler = getAllHandler;
        _getByIdHandler = getByIdHandler;
        _getByStatusHandler = getByStatusHandler;
        _getByWayHandler = getByWayHandler;
        _getByAssignHandler = getByAssignHandler;
    }

    [Authorize(Policy = "CanCreateOrUpdate")]
    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] CreateCustomerCommand command)
    {
        var id = await _createHandler.Handle(command);
        return Ok(new { CustomerId = id });
    }

    [Authorize(Policy = "CanCreateOrUpdate")]
    [HttpPut("update")]
    public async Task<IActionResult> Update([FromBody] UpdateCustomerCommand command)
    {
        await _updateHandler.Handle(command);
        return Ok(new { Message = "Customer updated successfully" });
    }

    [Authorize(Policy = "CanCreateOrUpdate")]
    [HttpPut("assign")]
    public async Task<IActionResult> Assign([FromBody] UpdateCustomerAssignmentCommand command)
    {
        await _updateAssignHandler.Handle(command);
        return Ok(new { Message = "Customer reassigned successfully" });
    }

    [Authorize(Policy = "CanComment")]
    [HttpPost("comment")]
    public async Task<IActionResult> AddComment([FromBody] AddCustomerCommentCommand command)
    {
        await _addCommentHandler.Handle(command);
        return Ok(new { Message = "Comment added successfully" });
    }

    [Authorize(Policy = "CanView")]
    [HttpGet("all")]
    public async Task<IActionResult> GetAll()
    {
        var result = await _getAllHandler.Handle();
        return Ok(result);
    }

    [Authorize(Policy = "CanView")]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _getByIdHandler.Handle(id);
        return Ok(result);
    }

    [Authorize(Policy = "CanView")]
    [HttpGet("by-status/{status}")]
    public async Task<IActionResult> GetByContactStatus(int status)
    {
        var result = await _getByStatusHandler.Handle((ContactStatus)status);
        return Ok(result);
    }

    [Authorize(Policy = "CanView")]
    [HttpGet("by-way/{way}")]
    public async Task<IActionResult> GetByWayOfContact(int way)
    {
        var result = await _getByWayHandler.Handle((WayOfContact)way);
        return Ok(result);
    }

    [Authorize(Policy = "CanView")]
    [HttpGet("by-assigned/{assignedToId}")]
    public async Task<IActionResult> GetByAssignedTo(Guid assignedToId)
    {
        var result = await _getByAssignHandler.Handle(assignedToId);
        return Ok(result);
    }
}
