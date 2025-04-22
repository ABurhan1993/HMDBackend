using CrmBackend.Domain.Common;
using CrmBackend.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrmBackend.Domain.Entities
{
    public class InquiryWorkscope : AuditableEntity
    {
        public int InquiryWorkscopeId { get; set; }

        public int InquiryId { get; set; }
        [ForeignKey("InquiryId")]
        public Inquiry Inquiry { get; set; }

        public int WorkScopeId { get; set; }
        [ForeignKey("WorkScopeId")]
        public WorkScope WorkScope { get; set; }

        public string InquiryWorkscopeDetailName { get; set; }

        // Drawing & Design
        public bool? IsMeasurementDrawing { get; set; }
        public Guid? MeasurementAssignedTo { get; set; }

        [ForeignKey("MeasurementAssignedTo")]
        public User MeasurementAssignedUser { get; set; }

        public Guid? DesignAssignedTo { get; set; }

        [ForeignKey("DesignAssignedTo")]
        public User DesignAssignedUser { get; set; }

        public InquiryStatus? InquiryStatus { get; set; }

        public DateTime? MeasurementScheduleDate { get; set; }
        public DateTime? MeasurementAddedOn { get; set; }

        public DateTime? DesignScheduleDate { get; set; }
        public DateTime? DesignAddedOn { get; set; }

        public bool? IsDesignApproved { get; set; }
        public bool? IsDesignSentToCustomer { get; set; }

        public FeedbackReaction? FeedbackReaction { get; set; }

        public bool? IsMeasurementReschedule { get; set; }
        public bool? IsDesignReschedule { get; set; }

        // Navigation to Quotation Details
        public WorkscopeQuotationDetail QuotationDetail { get; set; }

        // Navigation to Comments (اختياري في حال بدك تجيبهم مع الاستعلام)
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}
