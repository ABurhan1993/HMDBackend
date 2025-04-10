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
        return await _context.Users.Include(u => u.Role)
                                   .FirstOrDefaultAsync(u => u.Email == email);
    }
}
