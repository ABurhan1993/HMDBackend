using CrmBackend.Domain.Common;

namespace CrmBackend.Domain.Entities;

public class Design : AuditableEntity
{
    public int Id { get; set; }

    public int InquiryTaskId { get; set; }
    public InquiryTask InquiryTask { get; set; }

    public string DesignCustomerReviewDate { get; set; }
    public string TotalHoursConsumed { get; set; }

    public bool? IsTimerStarted { get; set; }
    public string TimerStartedOn { get; set; }
    public string TimerEndsOn { get; set; }

    public string SoftwareUsed { get; set; }
}
