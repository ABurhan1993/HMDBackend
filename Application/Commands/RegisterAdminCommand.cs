namespace CrmBackend.Application.Commands;

public class RegisterAdminCommand
{
    public string FullName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public Guid RoleId { get; set; }
    public int BranchId { get; set; }
}
