using CrmBackend.Domain.Entities;
using CrmBackend.Domain.Services;
using CrmBackend.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CrmBackend.Infrastructure.Repositories;

public class RoleClaimRepository : IRoleClaimRepository
{
    private readonly ApplicationDbContext _context;

    public RoleClaimRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(RoleClaim roleClaim)
    {
        _context.RoleClaims.Add(roleClaim);
        await _context.SaveChangesAsync();
    }

    public async Task<List<RoleClaim>> GetByRoleIdAsync(Guid roleId)
    {
        return await _context.RoleClaims
            .Where(rc => rc.RoleId == roleId)
            .ToListAsync();
    }

    public async Task DeleteByRoleIdAsync(Guid roleId)
    {
        var claims = await _context.RoleClaims
            .Where(rc => rc.RoleId == roleId)
            .ToListAsync();

        _context.RoleClaims.RemoveRange(claims);
        await _context.SaveChangesAsync();
    }
}
