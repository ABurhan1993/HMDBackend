namespace CrmBackend.Application.Commands.UserCommands;

public class EditUserCommand
{
    public Guid Id { get; set; }

    public string FirstName { get; set; }
    public string LastName { get; set; }

    public string Email { get; set; }
    public string Phone { get; set; }

    public Guid RoleId { get; set; }
    public int BranchId { get; set; }

    public bool IsNotificationEnabled { get; set; }
}
