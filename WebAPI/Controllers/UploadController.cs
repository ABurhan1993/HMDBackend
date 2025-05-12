using CrmBackend.Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CrmBackend.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UploadController : ControllerBase
    {
        private readonly IFileUploader _uploader;

        public UploadController(IFileUploader uploader)
        {
            _uploader = uploader;
        }

        [HttpPost("image")]
        public async Task<IActionResult> UploadImage([FromForm] UploadFileRequest file)
        {
            if (file == null || file.File.Length == 0)
                return BadRequest("File is empty.");

            using var stream = file.File.OpenReadStream();
            var url = await _uploader.UploadFileAsync(stream, file.File.FileName, file.File.ContentType);

            return Ok(new { Url = url });
        }
    }

    public class UploadFileRequest
    {
        public IFormFile File { get; set; }
    }

}
