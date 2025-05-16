using Amazon.S3.Transfer;
using Amazon.S3;
using Microsoft.Extensions.Configuration;

namespace CrmBackend.Infrastructure.Services;

public class S3FileUploader : IFileUploader
{
    private readonly IConfiguration _configuration;
    private readonly IAmazonS3 _s3Client;
    private readonly string _bucketName;

    public S3FileUploader(IConfiguration configuration)
    {
        _configuration = configuration;
        _bucketName = _configuration["AWS:BucketName"];

        _s3Client = new AmazonS3Client(
            _configuration["AWS:AccessKey"],
            _configuration["AWS:SecretKey"],
            Amazon.RegionEndpoint.GetBySystemName(_configuration["AWS:Region"]));
    }

    public async Task<string> UploadFileAsync(Stream fileStream, string fileName, string contentType, string path)
    {
        var key = string.IsNullOrWhiteSpace(path) ? fileName : $"{path.TrimEnd('/')}/{fileName}";

        var uploadRequest = new TransferUtilityUploadRequest
        {
            InputStream = fileStream,
            Key = key,
            BucketName = _bucketName,
            ContentType = contentType
        };

        var fileTransferUtility = new TransferUtility(_s3Client);
        await fileTransferUtility.UploadAsync(uploadRequest);

        return $"https://{_bucketName}.s3.{_configuration["AWS:Region"]}.amazonaws.com/{key}";
    }
}
