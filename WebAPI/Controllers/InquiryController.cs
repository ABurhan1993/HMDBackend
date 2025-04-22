using CrmBackend.Application.Commands.InquiryCommands;
using CrmBackend.Application.DTOs.InquiryDtos;
using CrmBackend.Application.Handlers.InquiryHandlers;
using CrmBackend.Domain.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CrmBackend.WebAPI.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class InquiryController : ControllerBase
{
    private readonly AddInquiryCommandHandler _addInquiryHandler;
    private readonly GetInquiriesForDisplayHandler _getInquiriesHandler;

    public InquiryController(AddInquiryCommandHandler addInquiryHandler,
        GetInquiriesForDisplayHandler getInquiriesHandler)
    {
        _addInquiryHandler = addInquiryHandler;
        _getInquiriesHandler = getInquiriesHandler;
    }

    [Authorize(Policy = PermissionConstants.Inquiries.Create)]
    [HttpPost("create")]
    public async Task<IActionResult> AddInquiry([FromBody] CreateInquiryRequest request)
    {
        var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var branchId = int.Parse(User.FindFirst("BranchId")!.Value);

        var inquiryId = await _addInquiryHandler.Handle(new AddInquiryCommand(request), userId, branchId);

        return Ok(new { inquiryId });
    }

    [Authorize(Policy = PermissionConstants.Inquiries.View)]
    [HttpGet("all")]
    public async Task<IActionResult> GetAll()
    {
        var branchId = int.Parse(User.FindFirst("BranchId")!.Value);
        var result = await _getInquiriesHandler.Handle(branchId);
        return Ok(result);
    }


}
