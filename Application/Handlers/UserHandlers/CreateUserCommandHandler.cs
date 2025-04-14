using CrmBackend.Application.UserCommands;
using CrmBackend.Domain.Entities;
using CrmBackend.Domain.Services;
using Microsoft.AspNetCore.Identity;

namespace CrmBackend.Application.Handlers.UserHandlers;

public class CreateUserCommandHandler
{
    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly IPasswordHasher<User> _passwordHasher;

    public CreateUserCommandHandler(
        IUserRepository userRepository,
        IRoleRepository roleRepository,
        IPasswordHasher<User> passwordHasher)
    {
        _userRepository = userRepository;
        _roleRepository = roleRepository;
        _passwordHasher = passwordHasher;
    }
    public async Task<Guid> Handle(CreateUserCommand command)
    {
        var role = await _roleRepository.GetByIdAsync(command.RoleId);
        if (role == null) throw new Exception("Role not found");

        var fullName = $"{command.FirstName} {command.LastName}".Trim();

        var user = new User
        {
            FirstName = command.FirstName,
            LastName = command.LastName,
            FullName = fullName,
            Email = command.Email,
            Phone = command.Phone,
            BranchId = command.BranchId,
            RoleId = command.RoleId,
            CreatedBy = command.CreatedBy,
            CreatedDate = DateTime.UtcNow,
            IsActive = true,
            IsDeleted = false
        };

        user.PasswordHash = _passwordHasher.HashPassword(user, command.Password);

        await _userRepository.AddAsync(user);
        return user.Id;
    }

}
