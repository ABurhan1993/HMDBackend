namespace CrmBackend.Application.Commands.CustomerCommands;

public class AddCustomerCommentCommand
{
    public int CustomerId { get; set; }
    public string CommentDetail { get; set; } = string.Empty;
    public Guid CommentAddedBy { get; set; }

    // خيار اختياري لتحديث الحالة
    public int? ContactStatus { get; set; }
}
