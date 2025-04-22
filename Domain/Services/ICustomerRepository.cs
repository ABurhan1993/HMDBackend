using CrmBackend.Application.DTOs.CustomersDTOs;
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
    Task SoftDeleteAsync(int customerId, Guid deletedBy);
    Task<IEnumerable<Customer>> GetUpcomingMeetingsAsync();
    Task<Customer?> FindByPhoneAsync(string phone);
    Task<List<CustomerCountByUserDto>> GetCountGroupedByCreatedByAsync(int branchId);
    Task<List<CustomerCountByUserDto>> GetCountGroupedByAssignedToAsync(int branchId);


}
