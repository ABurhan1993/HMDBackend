using CrmBackend.Application.DTOs.UploadDtos;
using CrmBackend.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CrmBackend.Web.Controllers;

[ApiController]
[Route("api/files")]
public class FileUploadController : ControllerBase
{
    private readonly IFileUploader _fileUploader;

    public FileUploadController(IFileUploader fileUploader)
    {
        _fileUploader = fileUploader;
    }

    [HttpPost("upload")]
    [Authorize]
    public async Task<IActionResult> UploadMultipleInquiryFiles([FromForm] UploadInquiryFileRequest request)
    {
        if (request.Files == null || request.Files.Count == 0)
            return BadRequest("No files provided.");

        if (string.IsNullOrWhiteSpace(request.InquiryCode) || string.IsNullOrWhiteSpace(request.Step))
            return BadRequest("InquiryCode and Step are required.");

        var results = new List<object>();

        foreach (var file in request.Files)
        {
            //var fileName = $"{Guid.NewGuid()}_{file.FileName}";
            //var key = $"inquiries/{request.InquiryCode}/{request.Step}/{fileName}";

            //using var stream = file.OpenReadStream();
            ////var url = await _fileUploader.UploadFileAsync(stream, key, file.ContentType);

            //results.Add(new
            //{
            //    FileName = file.FileName,
            //    S3Key = key,
            //    Url = url
            //});
        }

        return Ok(results);
    }

}
