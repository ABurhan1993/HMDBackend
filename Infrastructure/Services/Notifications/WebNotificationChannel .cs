using CrmBackend.Application.DTOs.NotificationDtos;
using CrmBackend.Domain.Entities;
using CrmBackend.Domain.Interfaces;
using CrmBackend.Infrastructure.Data;
using System;

namespace CrmBackend.Infrastructure.Services.Notifications;

public class WebNotificationChannel : INotificationChannel
{
    private readonly ApplicationDbContext _context;

    public WebNotificationChannel(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task SendAsync(NotificationMessageDto message)
    {
        var entity = new Notification
        {
            ReceiverUserId = message.ReceiverUserId,
            Title = message.Title,
            Message = message.Message,
            LinkUrl = message.Url,
            CreatedDate = DateTime.UtcNow
        };

        _context.Notifications.Add(entity);
        await _context.SaveChangesAsync();
    }
}
