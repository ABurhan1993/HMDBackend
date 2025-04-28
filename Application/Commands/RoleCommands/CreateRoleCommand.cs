namespace CrmBackend.Application.Commands.RoleCommands;

public class CreateRoleCommand
{
    public string RoleName { get; set; } = string.Empty;
    public List<string> Claims { get; set; } = new();
}
