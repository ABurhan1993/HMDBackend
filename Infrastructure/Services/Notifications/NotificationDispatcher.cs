using CrmBackend.Application.DTOs.NotificationDtos;
using CrmBackend.Application.Interfaces.Notifications;
using CrmBackend.Domain.Interfaces;

namespace CrmBackend.Infrastructure.Services.Notifications;

// ملف: Infrastructure/Notifications/NotificationDispatcher.cs
public class NotificationDispatcher : INotificationDispatcher
{
    private readonly IEnumerable<INotificationChannel> _channels;

    public NotificationDispatcher(IEnumerable<INotificationChannel> channels)
    {
        _channels = channels;
    }

    public async Task DispatchAsync(NotificationMessageDto message)
    {
        foreach (var channel in _channels)
        {
            var typeName = channel.GetType().Name;

            if (message.Channels == null || !message.Channels.Any() || message.Channels.Contains(typeName))
            {
                await channel.SendAsync(message);
            }
        }
    }

}