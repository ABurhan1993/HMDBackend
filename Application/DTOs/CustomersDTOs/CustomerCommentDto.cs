namespace CrmBackend.Application.DTOs.CustomersDTOs;

public class CustomerCommentDto
{
    public string Comment { get; set; }
    public DateTime? CreatedDate { get; set; }
    public string AddedBy { get; set; }
}

