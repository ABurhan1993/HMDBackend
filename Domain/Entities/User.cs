using CrmBackend.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrmBackend.Domain.Entities;

public class User : AuditableEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string FullName { get; set; }

    [Required]
    [MaxLength(100)]
    public string FirstName { get; set; }

    [MaxLength(100)]
    public string? LastName { get; set; }

    [Required]
    [MaxLength(20)]
    public string Phone { get; set; }

    [MaxLength(255)]
    public string? UserImageUrl { get; set; }

    public bool IsNotificationEnabled { get; set; } = true;

    [Required]
    [MaxLength(150)]
    public string Email { get; set; }

    [Required]
    public string PasswordHash { get; set; }

    [Required]
    public Guid RoleId { get; set; }
    public Role Role { get; set; }

    [Required]
    public int BranchId { get; set; }
    public Branch Branch { get; set; }

    public ICollection<UserClaim> Claims { get; set; } = new List<UserClaim>();

}
