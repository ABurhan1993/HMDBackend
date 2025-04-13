using CrmBackend.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrmBackend.Domain.Entities;

public class CustomerComment: AuditableEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int CustomerCommentId { get; set; }

    [Required]
    public string CustomerCommentDetail { get; set; }

    public int CustomerId { get; set; }
    public Guid CommentAddedBy { get; set; }

    public DateTime? CommentAddedOn { get; set; }

    [ForeignKey(nameof(CommentAddedBy))]
    public virtual User CommentAddedByNavigation { get; set; }

    [ForeignKey(nameof(CustomerId))]
    public virtual Customer Customer { get; set; }
}
