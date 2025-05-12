namespace CrmBackend.Infrastructure.Services;

public interface IFileUploader
{
    Task<string> UploadFileAsync(Stream fileStream, string fileName, string contentType);
}
