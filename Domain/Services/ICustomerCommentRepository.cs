using CrmBackend.Domain.Entities;

namespace CrmBackend.Domain.Services
{
    public interface ICustomerCommentRepository
    {
        Task AddAsync(CustomerComment comment);
        Task<List<CustomerComment>> GetByCustomerIdAsync(int customerId);
    }
}
