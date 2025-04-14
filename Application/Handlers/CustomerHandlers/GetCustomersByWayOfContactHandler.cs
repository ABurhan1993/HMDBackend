using CrmBackend.Application.DTOs.CustomersDTOs;
using CrmBackend.Domain.Enums;
using CrmBackend.Domain.Services;
using Microsoft.EntityFrameworkCore;

namespace CrmBackend.Application.Handlers.CustomerHandlers;

public class GetCustomersByWayOfContactHandler
{
    private readonly ICustomerRepository _customerRepository;

    public GetCustomersByWayOfContactHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<IEnumerable<CustomerDto>> Handle(WayOfContact way, int branchId)
    {
        var customers = await _customerRepository
            .Query()
            .Where(c => c.WayOfContact == way && c.BranchId == branchId && !c.IsDeleted && c.IsActive)
            .Include(c => c.Branch)
            .Include(c => c.CustomerAssignedToUser)
            .ToListAsync();

        return customers.Select(CustomerDto.FromEntity);
    }

}
