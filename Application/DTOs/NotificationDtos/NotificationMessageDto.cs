namespace CrmBackend.Application.DTOs.NotificationDtos;

// ملف: Application/DTOs/NotificationDTOs/NotificationMessageDto.cs
public class NotificationMessageDto
{
    public Guid ReceiverUserId { get; set; }
    public string Title { get; set; }
    public string Message { get; set; }
    public string? Url { get; set; }
    public List<string>? Channels { get; set; }
}
