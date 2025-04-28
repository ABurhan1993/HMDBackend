namespace CrmBackend.Application.Commands.UserCommands;

public class ResetUserPasswordCommand
{
    public Guid UserId { get; set; }
    public string NewPassword { get; set; } = string.Empty;
}
