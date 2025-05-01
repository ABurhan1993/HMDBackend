using CrmBackend.Domain.Common;
using System;

namespace CrmBackend.Domain.Entities;

public class TaskFile : AuditableEntity
{
    public int Id { get; set; }

    public int InquiryTaskId { get; set; }
    public InquiryTask InquiryTask { get; set; }

    public string FileName { get; set; }
    public string FilePath { get; set; }

    public string FileType { get; set; }
}
