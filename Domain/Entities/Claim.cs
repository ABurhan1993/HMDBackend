using System.ComponentModel.DataAnnotations;

namespace CrmBackend.Domain.Entities;

public class RoleClaim
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    public string Type { get; set; }

    [Required]
    public string Value { get; set; }

    public Guid RoleId { get; set; }
    public Role Role { get; set; }
}
