using CrmBackend.Application.DTOs.CustomersDTOs;
using CrmBackend.Domain.Services;
using Microsoft.EntityFrameworkCore;

namespace CrmBackend.Application.Handlers.CustomerHandlers;

public class GetCustomerByIdHandler
{
    private readonly ICustomerRepository _customerRepository;

    public GetCustomerByIdHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<CustomerDto?> Handle(int id)
    {
        var customer = await _customerRepository
            .Query()
            .Include(c => c.Branch)
            .Include(c => c.CustomerAssignedToUser)
            .FirstOrDefaultAsync(c => c.CustomerId == id);

        return customer is null ? null : CustomerDto.FromEntity(customer);
    }
}
