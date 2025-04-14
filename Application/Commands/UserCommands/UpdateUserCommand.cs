namespace CrmBackend.Application.UserCommands;

public class UpdateUserCommand
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public Guid RoleId { get; set; }
    public int BranchId { get; set; }
    public Guid UpdatedBy { get; set; }
}
