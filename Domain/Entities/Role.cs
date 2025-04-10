using System.ComponentModel.DataAnnotations;

namespace CrmBackend.Domain.Entities;

public class Role
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    public string Name { get; set; }

    public ICollection<User> Users { get; set; } = new List<User>();
    public ICollection<RoleClaim> RoleClaims { get; set; } = new List<RoleClaim>();
}
