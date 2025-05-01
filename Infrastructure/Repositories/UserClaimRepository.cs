using CrmBackend.Domain.Entities;
using CrmBackend.Domain.Services;
using CrmBackend.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CrmBackend.Infrastructure.Repositories;

public class UserClaimRepository : IUserClaimRepository
{
    private readonly ApplicationDbContext _context;

    public UserClaimRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<UserClaim>> GetClaimsByUserIdAsync(Guid userId)
    {
        return await _context.UserClaims
            .Where(c => c.UserId == userId)
            .ToListAsync();
    }

    public async Task AddClaimAsync(UserClaim claim)
    {
        _context.UserClaims.Add(claim);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteClaimAsync(int id)
    {
        var claim = await _context.UserClaims.FindAsync(id);
        if (claim != null)
        {
            _context.UserClaims.Remove(claim);
        }
    }

    public async Task SaveAsync() => await _context.SaveChangesAsync();
}
