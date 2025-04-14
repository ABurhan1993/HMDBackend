namespace CrmBackend.Application.UserCommands;

public class CreateUserCommand
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Phone { get; set; }
    public string Password { get; set; } = null!;
    public Guid RoleId { get; set; }
    public int BranchId { get; set; }
    public Guid CreatedBy { get; set; }
}
