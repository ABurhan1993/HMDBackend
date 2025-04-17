using CrmBackend.Application.Commands;
using CrmBackend.Application.Interfaces;
using CrmBackend.Domain.Entities;
using CrmBackend.Domain.Services;
using Microsoft.AspNetCore.Identity;
using CrmBackend.Infrastructure.Services; // Ensure this is referenced for PermissionHelper

namespace CrmBackend.Application.Handlers;

public class LoginCommandHandler
{
    private readonly IUserRepository _userRepository;
    private readonly IAuthenticationService _authService;
    private readonly PermissionHelper _permissionHelper;

    public LoginCommandHandler(
        IUserRepository userRepository,
        IAuthenticationService authService,
        PermissionHelper permissionHelper)
    {
        _userRepository = userRepository;
        _authService = authService;
        _permissionHelper = permissionHelper;
    }

    public async Task<string> Handle(LoginCommand command)
    {
        var user = await _userRepository.FindByEmailAsync(command.Email.ToLower());
        if (user == null)
            throw new UnauthorizedAccessException("Invalid credentials");

        var hasher = new PasswordHasher<User>();
        var result = hasher.VerifyHashedPassword(user, user.PasswordHash, command.Password);

        if (result == PasswordVerificationResult.Failed)
            throw new UnauthorizedAccessException("Invalid credentials");

        var permissions = await _permissionHelper.GetAllPermissionsForUserAsync(user.Id);
        return _authService.GenerateToken(user, permissions);
    }
}
