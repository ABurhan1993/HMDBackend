using CrmBackend.Domain.Common;
using CrmBackend.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrmBackend.Domain.Entities
{
    public class Inquiry : AuditableEntity
    {
        public int InquiryId { get; set; }

        public string? InquiryCode { get; set; }
        public string? InquiryName { get; set; }
        public string? InquiryDescription { get; set; }

        public DateTime? InquiryStartDate { get; set; }
        public DateTime? InquiryDueDate { get; set; }
        public DateTime? InquiryEndDate { get; set; }

        public bool? IsMeasurementProvidedByCustomer { get; set; }
        public bool? IsDesignProvidedByCustomer { get; set; }

        public Guid? QuotationAssignTo { get; set; }
        [ForeignKey("QuotationAssignTo")]
        public User QuotationAssignedUser { get; set; }

        public DateTime? QuotationScheduleDate { get; set; }
        public DateTime? QuotationAddedOn { get; set; }

        public int? CustomerId { get; set; }
        public Customer Customer { get; set; }

        public int? BranchId { get; set; }
        public Branch Branch { get; set; }

        public int? BuildingId { get; set; }
        public Building Building { get; set; }

        public bool? IsEscalationRequested { get; set; }
        public DateTime? EscalationRequestedDate { get; set; }

        public Guid? EscalationRequestedBy { get; set; }
        [ForeignKey("EscalationRequestedBy")]
        public User EscalationRequestedByUser { get; set; }

        public InquiryStatus? Status { get; set; }

        public Guid? ManagedBy { get; set; }
        [ForeignKey("ManagedBy")]
        public User ManagedByUser { get; set; }

        public int? TempContractAssignedTo { get; set; }
        public bool? IsQuotationReschedule { get; set; }
        public bool? IsInquiryLocked { get; set; }
        public bool? IsExistingInquiry { get; set; }

        public int? ExistingInquiryId { get; set; }
        public Inquiry? ExistingInquiry { get; set; }
        public virtual ICollection<InquiryWorkscope> InquiryWorkscopes { get; set; } = new List<InquiryWorkscope>();
        public ICollection<InquiryTask> InquiryTasks { get; set; } = new List<InquiryTask>();
        public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    }
}
