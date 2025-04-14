using CrmBackend.Domain.Entities;

namespace CrmBackend.Domain.Services;

public interface IRoleRepository
{
    Task<Guid> AddAsync(Role role);
    Task UpdateAsync(Role role);
    Task<Role?> GetByIdAsync(Guid id);
    Task<List<Role>> GetAllAsync();
}
