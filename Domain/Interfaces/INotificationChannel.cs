using CrmBackend.Application.DTOs.NotificationDtos;

namespace CrmBackend.Domain.Interfaces;

public interface INotificationChannel
{
    Task SendAsync(NotificationMessageDto message);
}
