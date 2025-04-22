using CrmBackend.Domain.Common;
using CrmBackend.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrmBackend.Domain.Entities
{
    public class Comment : AuditableEntity
    {
        public int CommentId { get; set; }

        public string CommentName { get; set; }
        public string CommentDetail { get; set; }

        public int? InquiryId { get; set; }
        [ForeignKey("InquiryId")]
        public Inquiry Inquiry { get; set; }
        public int? InquiryWorkscopeId { get; set; }
        [ForeignKey("InquiryWorkscopeId")]
        public InquiryWorkscope InquiryWorkscope { get; set; }

        public InquiryStatus? InquiryStatus { get; set; }

        public Guid? CommentAddedBy { get; set; }
        [ForeignKey("CommentAddedBy")]
        public User CommentAddedByUser { get; set; }

        public DateTime? CommentAddedOn { get; set; }
        public DateTime? CommentNextFollowup { get; set; }

        public bool? IsFollowedUpRequired { get; set; }
    }
}
