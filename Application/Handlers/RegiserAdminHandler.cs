using CrmBackend.Domain.Entities;
using CrmBackend.Infrastructure.Data;
using CrmBackend.Application.Commands;
using CrmBackend.Domain.Entities;
using CrmBackend.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;

namespace CrmBackend.Application.Handlers;

public class RegisterAdminCommandHandler
{
    private readonly ApplicationDbContext _context;

    public RegisterAdminCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(RegisterAdminCommand command)
    {
        var hasher = new PasswordHasher<User>();

        var user = new User
        {
            FullName = command.FullName,
            Email = command.Email,
            RoleId = command.RoleId,
            BranchId = command.BranchId
        };

        user.PasswordHash = hasher.HashPassword(user, command.Password);

        _context.Users.Add(user);
        await _context.SaveChangesAsync();
    }
}
