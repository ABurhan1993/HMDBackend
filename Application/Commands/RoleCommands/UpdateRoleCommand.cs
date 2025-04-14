namespace CrmBackend.Application.Commands.RoleCommands
{
    public class UpdateRoleCommand
    {
        public Guid RoleId { get; set; }
        public string Name { get; set; }
    }
}