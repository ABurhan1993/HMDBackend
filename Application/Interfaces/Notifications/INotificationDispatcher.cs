using CrmBackend.Application.DTOs.NotificationDtos;

namespace CrmBackend.Application.Interfaces.Notifications;

public interface INotificationDispatcher
{
    Task DispatchAsync(NotificationMessageDto message);
}
