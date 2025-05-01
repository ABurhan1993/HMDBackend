using CrmBackend.Domain.Common;

namespace CrmBackend.Domain.Entities;

public class Measurement : AuditableEntity
{
    public int Id { get; set; }

    public int InquiryTaskId { get; set; }
    public InquiryTask InquiryTask { get; set; }

    public bool? IsMeasurementApproved { get; set; }
}
