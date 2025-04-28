using Microsoft.AspNetCore.Authorization;

public class PermissionOrRequirement : IAuthorizationRequirement
{
    public string[] Permissions { get; }

    public PermissionOrRequirement(params string[] permissions)
    {
        Permissions = permissions;
    }
}

public class PermissionOrHandler : AuthorizationHandler<PermissionOrRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionOrRequirement requirement)
    {
        if (context.User == null)
            return Task.CompletedTask;

        var userPermissions = context.User.FindAll("Permission").Select(c => c.Value).ToList();

        if (userPermissions.Any(p => requirement.Permissions.Contains(p)))
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}
