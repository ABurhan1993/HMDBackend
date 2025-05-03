using CrmBackend.Domain.Common;

namespace CrmBackend.Domain.Entities;

public class Notification : AuditableEntity
{
    public int Id { get; set; }
    public Guid ReceiverUserId { get; set; } // المستلم
    public string Title { get; set; }
    public string Message { get; set; }
    public string? LinkUrl { get; set; }
    public bool IsRead { get; set; } = false;
    public DateTime? ReadOn { get; set; }
}

