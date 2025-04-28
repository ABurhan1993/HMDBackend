namespace CrmBackend.Application.Commands.RoleCommands
{
    public class UpdateRoleCommand
    {
        public Guid RoleId { get; set; }
        public string RoleName { get; set; } = string.Empty;
        public List<string> Claims { get; set; } = new();
    }
}
