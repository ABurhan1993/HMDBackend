using CrmBackend.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CrmBackend.Infrastructure.Services;

public class PermissionHelper
{
    private readonly ApplicationDbContext _context;

    public PermissionHelper(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<string>> GetAllPermissionsForUserAsync(Guid userId)
    {
        var user = await _context.Users
            .Include(u => u.Role)
                .ThenInclude(r => r.RoleClaims)
            .Include(u => u.Claims)
            .FirstOrDefaultAsync(u => u.Id == userId); // ✅ هون لازم تكون async

        if (user == null)
            return new List<string>();

        var rolePermissions = user.Role?.RoleClaims
    .Where(rc => rc.Type.Equals("Permission", StringComparison.OrdinalIgnoreCase))
    .Select(rc => rc.Value) ?? Enumerable.Empty<string>();

        var userPermissions = user.Claims
            .Where(uc => uc.ClaimType.Equals("Permission", StringComparison.OrdinalIgnoreCase))
            .Select(uc => uc.ClaimValue);


        return rolePermissions
            .Concat(userPermissions)
            .Distinct()
            .ToList();
    }
}
