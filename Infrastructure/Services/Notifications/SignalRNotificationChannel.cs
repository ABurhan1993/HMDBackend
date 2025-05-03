using CrmBackend.Application.DTOs.NotificationDtos;
using CrmBackend.Domain.Interfaces;
using CrmBackend.WebAPI.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace CrmBackend.Infrastructure.Services.Notifications;

public class SignalRNotificationChannel : INotificationChannel
{
    private readonly IHubContext<NotificationHub> _hubContext;

    public SignalRNotificationChannel(IHubContext<NotificationHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public async Task SendAsync(NotificationMessageDto message)
    {
        if (message.ReceiverUserId == Guid.Empty) return;

        await _hubContext.Clients
            .Group(message.ReceiverUserId.ToString())
            .SendAsync("ReceiveNotification", new
            {
                title = message.Title,
                message = message.Message,
                createdDate = DateTime.UtcNow.ToString("o")
            });
    }
}
