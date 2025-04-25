namespace CrmBackend.Application.Commands.CustomerCommands;

public class UpdateCustomerCommand
{
    public int CustomerId { get; set; }
    public string? CustomerName { get; set; }
    public string? CustomerEmail { get; set; }
    public string? CustomerContact { get; set; }
    public string? CustomerWhatsapp { get; set; }
    public string? CustomerAddress { get; set; }
    public string? CustomerCity { get; set; }
    public string? CustomerCountry { get; set; }
    public string? CustomerNationality { get; set; }
    public string? CustomerNationalId { get; set; }
    public DateTime? CustomerNextMeetingDate { get; set; }
    public int? ContactStatus { get; set; }
    public bool? IsVisitedShowroom { get; set; }
    public int? CustomerTimeSpent { get; set; }
    public int? WayOfContact { get; set; }
    public Guid? CustomerAssignedTo { get; set; }
    public Guid UpdatedBy { get; set; } // من التوكن لاحقًا
    public string? CommentDetail { get; set; } // التعليق الجديد
}
