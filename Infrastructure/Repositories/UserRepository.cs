using CrmBackend.Infrastructure.Data;
using CrmBackend.Domain.Entities;
using CrmBackend.Domain.Services;
using Microsoft.EntityFrameworkCore;

namespace CrmBackend.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<User?> FindByEmailAsync(string email)
    {
        var loweredEmail = email.ToLower();
        return await _context.Users
            .Include(u => u.Role)
            .FirstOrDefaultAsync(u =>
                u.Email.ToLower() == loweredEmail &&
                u.IsActive &&
                !u.IsDeleted);
    }

    public async Task<User?> GetByIdAsync(Guid id)
    {
        return await _context.Users.FindAsync(id);
    }

    public async Task<List<User>> GetAllAsync()
    {
        return await _context.Users
            .Where(u => u.IsActive && !u.IsDeleted)
            .ToListAsync();
    }
    public async Task AddAsync(User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
    }
    public IQueryable<User> Query()
    {
        return _context.Users.AsQueryable();
    }
    public async Task<List<User>> GetUsersByBranchIdAsync(int branchId)
    {
        return await _context.Users
            .Where(u => u.BranchId == branchId && !u.IsDeleted && u.IsActive)
            .ToListAsync();
    }

}
