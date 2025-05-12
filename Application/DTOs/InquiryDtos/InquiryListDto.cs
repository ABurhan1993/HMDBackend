using CrmBackend.Domain.Enums;

namespace CrmBackend.Application.DTOs.InquiryDtos;

public class InquiryListDto
{
    public int InquiryId { get; set; }
    public string InquiryCode { get; set; }
    public string InquiryDescription { get; set; }
    public DateTime? InquiryStartDate { get; set; }
    public DateTime? InquiryEndDate { get; set; }

    public bool? IsMeasurementProvidedByCustomer { get; set; }
    public bool? IsDesignProvidedByCustomer { get; set; }

    public string InquiryStatusName { get; set; }
    public string ManagedByUserName { get; set; }

    // Customer
    public string CustomerName { get; set; }
    public string CustomerContact { get; set; }
    public string CustomerEmail { get; set; }
    public string CustomerNotes { get; set; }
    public DateTime? CustomerNextMeetingDate { get; set; }
    public string ContactStatus { get; set; }
    public string WayOfContact { get; set; }

    // Building
    public string BuildingAddress { get; set; }
    public string BuildingMakaniMap { get; set; }
    public BuildingTypeOfUnit? BuildingTypeOfUnit { get; set; }
    public BuildingCondition? BuildingCondition { get; set; }
    public string BuildingFloor { get; set; }
    public bool? BuildingReconstruction { get; set; }
    public bool? IsOccupied { get; set; }

    // Workscope Summary
    public List<string> WorkscopeNames { get; set; } = new();

    // Workscope Details
    public List<InquiryWorkscopeDisplayDto> WorkscopeDetails { get; set; } = new();
    public List<InquiryCommentDto> Comments { get; set; } = new();

}

public class InquiryWorkscopeDisplayDto
{
    public string WorkscopeName { get; set; }
    public string InquiryWorkscopeDetailName { get; set; }
    public string InquiryStatus { get; set; }

    public string MeasurementAssignedTo { get; set; }
    public string DesignAssignedTo { get; set; }

    public DateTime? MeasurementScheduleDate { get; set; }
    public DateTime? DesignScheduleDate { get; set; }
    public DateTime? MeasurementAddedOn { get; set; }
    public DateTime? DesignAddedOn { get; set; }

    public bool? IsDesignApproved { get; set; }
    public bool? IsDesignSentToCustomer { get; set; }

    public string FeedbackReaction { get; set; }

    public bool? IsMeasurementReschedule { get; set; }
    public bool? IsDesignReschedule { get; set; }
}
