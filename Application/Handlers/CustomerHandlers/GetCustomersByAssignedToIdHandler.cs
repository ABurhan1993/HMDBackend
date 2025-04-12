using CrmBackend.Application.DTOs.CustomersDTOs;
using CrmBackend.Domain.Services;
using Microsoft.EntityFrameworkCore;

namespace CrmBackend.Application.Handlers.CustomerHandlers;

public class GetCustomersByAssignedToIdHandler
{
    private readonly ICustomerRepository _customerRepository;

    public GetCustomersByAssignedToIdHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<IEnumerable<CustomerDto>> Handle(Guid userId)
    {
        var customers = await _customerRepository
            .Query()
            .Where(c => c.CustomerAssignedTo == userId)
            .Include(c => c.Branch)
            .Include(c => c.CustomerAssignedToUser)
            .ToListAsync();

        return customers.Select(CustomerDto.FromEntity);
    }
}
