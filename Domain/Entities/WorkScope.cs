using CrmBackend.Domain.Common;
using CrmBackend.Domain.Entities;

public class WorkScope : AuditableEntity
{
    public int WorkScopeId { get; set; }

    public string WorkScopeName { get; set; }
    public string? WorkScopeDescription { get; set; }

    public int? QuestionaireType { get; set; }

    public ICollection<InquiryWorkscope> InquiryWorkscopes { get; set; } = new List<InquiryWorkscope>();
}
