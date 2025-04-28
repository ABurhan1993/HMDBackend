using CrmBackend.Domain.Entities;

namespace CrmBackend.Domain.Services;

public interface IRoleClaimRepository
{
    Task AddAsync(RoleClaim roleClaim);
    Task<List<RoleClaim>> GetByRoleIdAsync(Guid roleId);
    Task DeleteByRoleIdAsync(Guid roleId);
}
