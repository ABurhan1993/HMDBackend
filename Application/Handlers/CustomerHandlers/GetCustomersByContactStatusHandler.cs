using CrmBackend.Application.DTOs.CustomersDTOs;
using CrmBackend.Domain.Enums;
using CrmBackend.Domain.Services;
using Microsoft.EntityFrameworkCore;

namespace CrmBackend.Application.Handlers.CustomerHandlers;

public class GetCustomersByContactStatusHandler
{
    private readonly ICustomerRepository _customerRepository;

    public GetCustomersByContactStatusHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<IEnumerable<CustomerDto>> Handle(ContactStatus status)
    {
        var customers = await _customerRepository
            .Query()
            .Where(c => c.ContactStatus == status)
            .Include(c => c.Branch)
            .Include(c => c.CustomerAssignedToUser)
            .ToListAsync();

        return customers.Select(CustomerDto.FromEntity);
    }
}
