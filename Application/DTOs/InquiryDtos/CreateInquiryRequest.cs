using CrmBackend.Domain.Enums;

namespace CrmBackend.Application.DTOs.InquiryDtos;

public class CreateInquiryRequest
{
    // ===== Customer =====
    public int? CustomerId { get; set; }
    public string CustomerName { get; set; }
    public string CustomerEmail { get; set; }
    public string CustomerContact { get; set; }
    public string CustomerWhatsapp { get; set; }

    public string CustomerAddress { get; set; }
    public string CustomerCity { get; set; }
    public string CustomerCountry { get; set; }
    public string CustomerNationality { get; set; }

    public string? CustomerNotes { get; set; }
    public DateTime? CustomerNextMeetingDate { get; set; }

    public int ContactStatus { get; set; }
    public int WayOfContact { get; set; }

    public bool? IsVisitedShowroom { get; set; }
    public int? CustomerTimeSpent { get; set; }

    public Guid? CustomerAssignedTo { get; set; }

    // BranchId, CreatedByUserId will be taken from token

    // ===== Inquiry =====
    public DateTime? InquiryDueDate { get; set; } // optional
    public string? InquiryName { get; set; }       // optional
    public string? InquiryDescription { get; set; }

    public bool? IsDesignProvidedByCustomer { get; set; }
    public bool? IsMeasurementProvidedByCustomer { get; set; }

    public Guid? MeasurementAssignedTo { get; set; }
    public DateTime? MeasurementScheduleDate { get; set; }

    // ===== Building =====
    public string BuildingAddress { get; set; }
    public BuildingTypeOfUnit BuildingTypeOfUnit { get; set; }
    public BuildingCondition BuildingCondition { get; set; }
    public string BuildingFloor { get; set; }

    public bool? IsOccupied { get; set; }
    public bool? IsUnderConstruction { get; set; }
    public string BuildingMakaniMap { get; set; }
    public string BuildingLongitude { get; set; }
    public string BuildingLatitude { get; set; }

    // ===== Workscopes =====
    public List<WorkscopeRequestDto> Workscopes { get; set; } = new();
}

public class WorkscopeRequestDto
{
    public int WorkscopeId { get; set; }
    public string InquiryWorkscopeDetailName { get; set; }

}
