using CrmBackend.Domain.Entities;

namespace CrmBackend.Domain.Services;

public interface INotificationRepository
{
    Task<List<Notification>> GetUserNotificationsAsync(Guid userId);
    Task MarkAsReadAsync(int notificationId);
}
