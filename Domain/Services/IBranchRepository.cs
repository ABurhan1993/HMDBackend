using CrmBackend.Domain.Entities;

namespace CrmBackend.Domain.Services
{
    public interface IBranchRepository
    {
        Task<int> AddAsync(Branch branch);
        Task UpdateAsync(Branch branch);
        Task<Branch?> GetByIdAsync(int id);
        Task<List<Branch>> GetAllAsync();
    }
}