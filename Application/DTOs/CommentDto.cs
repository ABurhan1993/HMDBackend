namespace CrmBackend.Application.DTOs;

public class CommentDto
{
    public int CommentId { get; set; }
    public string CommentDetail { get; set; }
    public string AddedByName { get; set; }
    public DateTime? AddedOn { get; set; }

    public int? InquiryId { get; set; }
    public int? InquiryWorkscopeId { get; set; }
}
