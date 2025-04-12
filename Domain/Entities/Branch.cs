using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrmBackend.Domain.Entities;

public class Branch
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    public bool IsActive { get; set; } = true;

    public bool IsDeleted { get; set; } = false;

    public ICollection<User> Users { get; set; } = new List<User>();
    public ICollection<Customer> Customers { get; set; } = new List<Customer>();
}
