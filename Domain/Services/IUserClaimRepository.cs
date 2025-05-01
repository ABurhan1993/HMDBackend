using CrmBackend.Domain.Entities;

namespace CrmBackend.Domain.Services;

public interface IUserClaimRepository
{
    Task<List<UserClaim>> GetClaimsByUserIdAsync(Guid userId);
    Task AddClaimAsync(UserClaim claim);
    Task DeleteClaimAsync(int id);
    Task SaveAsync();
}
