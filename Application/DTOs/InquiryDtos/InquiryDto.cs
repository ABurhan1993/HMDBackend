namespace CrmBackend.Application.DTOs.InquiryDTOs;

public class InquiryDto
{
    public int InquiryId { get; set; }
    public string InquiryCode { get; set; }
    public string InquiryName { get; set; }
    public string InquiryDescription { get; set; }

    public DateTime? InquiryStartDate { get; set; }
    public DateTime? InquiryDueDate { get; set; }
    public DateTime? InquiryEndDate { get; set; }

    public string ManagedByName { get; set; }
    public string QuotationAssignedToName { get; set; }
    public string EscalationRequestedByName { get; set; }

    public string StatusName { get; set; }

    public string BranchName { get; set; }
    public string CustomerName { get; set; }
    public string BuildingAddress { get; set; }

    public bool? IsEscalationRequested { get; set; }
    public bool? IsInquiryLocked { get; set; }
}
