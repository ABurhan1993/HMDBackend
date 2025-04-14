using CrmBackend.Domain.Entities;

namespace CrmBackend.Domain.Services;

public interface IUserRepository
{
    Task<User?> FindByEmailAsync(string email);
    Task<User?> GetByIdAsync(Guid id);
    Task<List<User>> GetAllAsync();
    Task AddAsync(User user);
    IQueryable<User> Query();


}
