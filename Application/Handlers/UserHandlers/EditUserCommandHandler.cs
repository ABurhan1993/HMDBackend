using CrmBackend.Application.Commands.UserCommands;
using CrmBackend.Domain.Services;
using CrmBackend.Infrastructure.Data;

namespace CrmBackend.Application.Handlers.UserHandlers;

public class EditUserCommandHandler
{
    private readonly IUserRepository _userRepository;

    public EditUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task Handle(EditUserCommand command)
    {
        var user = await _userRepository.GetByIdAsync(command.Id);
        if (user == null)
            throw new Exception("User not found.");

        user.FirstName = command.FirstName;
        user.LastName = command.LastName;
        user.FullName = $"{command.FirstName} {command.LastName}".Trim(); // ← الإضافة هنا
        user.Email = command.Email;
        user.Phone = command.Phone;
        user.RoleId = command.RoleId;
        user.BranchId = command.BranchId;
        user.IsNotificationEnabled = command.IsNotificationEnabled;
        user.UpdatedDate = DateTime.UtcNow;

        await _userRepository.SaveAsync();
    }

}
