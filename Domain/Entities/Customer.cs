using CrmBackend.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrmBackend.Domain.Entities;

public class Customer
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int CustomerId { get; set; }

    [Required]
    public string CustomerName { get; set; }

    public string? CustomerEmail { get; set; }
    public string? CustomerContact { get; set; }
    public string? CustomerWhatsapp { get; set; }
    public string? CustomerAddress { get; set; }
    public string? CustomerCity { get; set; }
    public string? CustomerCountry { get; set; }
    public string? CustomerNationality { get; set; }
    public string? CustomerNationalId { get; set; }
    public string? CustomerNotes { get; set; }
    public string? CustomerNextMeetingDate { get; set; }

    public ContactStatus ContactStatus { get; set; }
    public bool? IsVisitedShowroom { get; set; }
    public int? CustomerTimeSpent { get; set; }
    public WayOfContact WayOfContact { get; set; }

    [ForeignKey("Branch")]
    public int? BranchId { get; set; }
    public virtual Branch Branch { get; set; }

    [ForeignKey("User")]
    public Guid? UserId { get; set; }
    public virtual User User { get; set; }

    [ForeignKey("CustomerAssignedToUser")]
    public Guid? CustomerAssignedTo { get; set; }
    public virtual User CustomerAssignedToUser { get; set; }

    [ForeignKey("CustomerAssignedByUser")]
    public Guid? CustomerAssignedBy { get; set; }
    public virtual User CustomerAssignedByUser { get; set; }

    public string? CustomerAssignedDate { get; set; }

    public bool? IsEscalationRequested { get; set; }

    [ForeignKey("EscalationRequestedByUser")]
    public Guid? EscalationRequestedBy { get; set; }
    public virtual User EscalationRequestedByUser { get; set; }

    public string? EscalationRequestedOn { get; set; }

    [ForeignKey("EscalatedByUser")]
    public Guid? EscalatedBy { get; set; }
    public virtual User EscalatedByUser { get; set; }

    public string? EscalatedOn { get; set; }

    public bool? IsReturnedNotRespondingCustomer { get; set; }
    public string? IsReturnedNotRespondingCustomerDate { get; set; }

    [ForeignKey("IsReturnedByUser")]
    public Guid? IsReturnedBy { get; set; }
    public virtual User IsReturnedByUser { get; set; }

    [ForeignKey("CreatedByUser")]
    public Guid CreatedBy { get; set; }
    public virtual User CreatedByUser { get; set; }

    public string? CreatedDate { get; set; }

    [ForeignKey("UpdatedByUser")]
    public Guid? UpdatedBy { get; set; }
    public virtual User UpdatedByUser { get; set; }

    public string? UpdatedDate { get; set; }

    public bool IsActive { get; set; } = true;
    public bool IsDeleted { get; set; } = false;

    public virtual ICollection<CustomerBranch> CustomerBranches { get; set; } = new List<CustomerBranch>();
    public virtual ICollection<CustomerComment> CustomerComments { get; set; } = new List<CustomerComment>();
    //public virtual ICollection<Inquiry> Inquiries { get; set; } = new List<Inquiry>();
}
