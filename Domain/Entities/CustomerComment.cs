using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrmBackend.Domain.Entities;

public class CustomerComment
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int CustomerCommentId { get; set; }

    [Required]
    public string CustomerCommentDetail { get; set; }

    public int CustomerId { get; set; }
    public Guid CommentAddedBy { get; set; }

    public string? CommentAddedOn { get; set; }
    public string? CreatedDate { get; set; }
    public Guid? CreatedBy { get; set; }
    public string? UpdatedDate { get; set; }
    public Guid? UpdatedBy { get; set; }

    public bool IsActve { get; set; } = true;
    public bool IsDeleted { get; set; } = false;

    [ForeignKey(nameof(CommentAddedBy))]
    public virtual User CommentAddedByNavigation { get; set; }

    [ForeignKey(nameof(CustomerId))]
    public virtual Customer Customer { get; set; }
}
