using CrmBackend.Domain.Entities;

namespace CrmBackend.Domain.Services;

public interface IUserRepository
{
    Task<User?> FindByEmailAsync(string email);
}
