using CrmBackend.Application.Commands.UserCommands;
using CrmBackend.Domain.Entities;
using CrmBackend.Domain.Services;
using CrmBackend.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;

namespace CrmBackend.Application.Handlers.UserHandlers;

public class ResetUserPasswordHandler
{
    private readonly ApplicationDbContext _context;
    private readonly IPasswordHasher<User> _passwordHasher;

    public ResetUserPasswordHandler(ApplicationDbContext context, IPasswordHasher<User> passwordHasher)
    {
        _context = context;
        _passwordHasher = passwordHasher;
    }

    public async Task Handle(ResetUserPasswordCommand command)
    {
        var user = await _context.Users.FindAsync(command.UserId);
        if (user == null)
            throw new Exception("User not found.");

        user.PasswordHash = _passwordHasher.HashPassword(user, command.NewPassword);
        await _context.SaveChangesAsync();
    }
}
