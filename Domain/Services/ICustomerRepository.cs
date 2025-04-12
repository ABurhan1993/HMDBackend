using CrmBackend.Domain.Entities;
using CrmBackend.Domain.Enums;

namespace CrmBackend.Domain.Services;

public interface ICustomerRepository
{
    Task<int> AddAsync(Customer customer);
    Task<Customer?> GetByIdAsync(int customerId);
    Task<IEnumerable<Customer>> GetAllAsync();
    Task UpdateAsync(Customer customer);
    Task DeleteAsync(int customerId);
    Task<IEnumerable<Customer>> GetByContactStatusAsync(ContactStatus status);
    Task<IEnumerable<Customer>> GetByWayOfContactAsync(WayOfContact way);
    Task<IEnumerable<Customer>> GetByAssignedToIdAsync(Guid userId);
    IQueryable<Customer> Query();
}
