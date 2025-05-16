namespace CrmBackend.Application.DTOs.UploadDtos;

public class UploadInquiryFileRequest
{
    public List<IFormFile> Files { get; set; } = new();
    public string InquiryCode { get; set; } = string.Empty;
    public string Step { get; set; } = string.Empty;
}
