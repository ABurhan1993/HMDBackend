using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrmBackend.Domain.Entities;

public class CustomerBranch
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int CustomerBranchId { get; set; }

    public int CustomerId { get; set; }
    public int BranchId { get; set; }

    public Guid? CreatedBy { get; set; }
    public string? CreatedDate { get; set; }
    public Guid? UpdatedBy { get; set; }
    public string? UpdatedDate { get; set; }

    [ForeignKey(nameof(BranchId))]
    public virtual Branch Branch { get; set; }

    [ForeignKey(nameof(CustomerId))]
    public virtual Customer Customer { get; set; }
}
