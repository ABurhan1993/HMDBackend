namespace CrmBackend.Application.Commands.UserClaimCommands;
    public class AddUserClaimCommand
    {
        public Guid UserId { get; set; }
        public string ClaimType { get; set; } = string.Empty;
        public string ClaimValue { get; set; } = string.Empty;
    }

    public class DeleteUserClaimCommand
    {
        public int Id { get; set; }
    }
