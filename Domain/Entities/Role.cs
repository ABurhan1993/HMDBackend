using CrmBackend.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrmBackend.Domain.Entities;

public class Role: AuditableEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [Required]
    public string Name { get; set; }

    public ICollection<User> Users { get; set; } = new List<User>();
    public ICollection<RoleClaim> RoleClaims { get; set; } = new List<RoleClaim>();
}
