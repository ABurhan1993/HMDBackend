namespace CrmBackend.Application.DTOs.InquiryDtos;

public class InquiryCommentDto
{
    public string CommentName { get; set; }
    public string CommentDetail { get; set; }
    public string AddedBy { get; set; }
    public DateTime? AddedOn { get; set; }
    public DateTime? NextFollowup { get; set; }
}
