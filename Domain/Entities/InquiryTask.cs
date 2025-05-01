using CrmBackend.Domain.Common;
using CrmBackend.Domain.Enums;
using System;
using System.Collections.Generic;

namespace CrmBackend.Domain.Entities;

public class InquiryTask : AuditableEntity
{
    public int Id { get; set; }

    // ✅ الإلزامي: ربط مع الاستفسار
    public int InquiryId { get; set; }
    public Inquiry Inquiry { get; set; }

    // ✅ اختياري: ربط مع نوع العمل إذا احتجته
    public int? InquiryWorkscopeId { get; set; }
    public InquiryWorkscope InquiryWorkscope { get; set; }

    public TaskType TaskType { get; set; } // Measurement, Design, etc.

    public string TaskName { get; set; }
    public string TaskDescription { get; set; }
    public string TaskComment { get; set; }

    public DateTime? ScheduledDate { get; set; }

    public Guid? AssignedToUserId { get; set; }
    public User AssignedToUser { get; set; }

    public bool IsRescheduled { get; set; }

    public Guid? ApprovedByUserId { get; set; }
    public User ApprovedByUser { get; set; }

    public DateTime? ApprovedOn { get; set; }

    public ICollection<TaskFile> Files { get; set; } = new List<TaskFile>();
}
