using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrmBackend.Domain.Entities;

public class RoleClaim
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [Required]
    public string Type { get; set; }

    [Required]
    public string Value { get; set; }

    public Guid RoleId { get; set; }
    public Role Role { get; set; }
}
