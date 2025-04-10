using CrmBackend.Application.Commands;
using CrmBackend.Application.Interfaces;
using CrmBackend.Domain.Entities;
using CrmBackend.Domain.Services;
using Microsoft.AspNetCore.Identity;

namespace CrmBackend.Application.Handlers;

public class LoginCommandHandler
{
    private readonly IUserRepository _userRepository;
    private readonly IAuthenticationService _authService;

    public LoginCommandHandler(IUserRepository userRepository, IAuthenticationService authService)
    {
        _userRepository = userRepository;
        _authService = authService;
    }

    public async Task<string> Handle(LoginCommand command)
    {
        var user = await _userRepository.FindByEmailAsync(command.Email);
        if (user == null)
            throw new UnauthorizedAccessException("Invalid credentials");

        var hasher = new PasswordHasher<User>();
        var result = hasher.VerifyHashedPassword(user, user.PasswordHash, command.Password);

        if (result == PasswordVerificationResult.Failed)
            throw new UnauthorizedAccessException("Invalid credentials");

        return _authService.GenerateToken(user);
    }
}
