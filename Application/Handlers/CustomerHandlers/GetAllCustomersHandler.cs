using CrmBackend.Application.DTOs.CustomersDTOs;
using CrmBackend.Domain.Services;
using Microsoft.EntityFrameworkCore;

namespace CrmBackend.Application.Handlers.CustomerHandlers;

public class GetAllCustomersHandler
{
    private readonly ICustomerRepository _customerRepository;

    public GetAllCustomersHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<IEnumerable<CustomerDto>> Handle(int branchId)
    {
        var customers = await _customerRepository
            .Query()
            .Include(c => c.Branch)
            .Include(c => c.CustomerAssignedToUser)
            .Where(c => c.BranchId == branchId && c.IsActive && !c.IsDeleted)
            .OrderByDescending(c => c.CreatedDate) // 👈 الترتيب هنا
            .ToListAsync();

        return customers.Select(CustomerDto.FromEntity);
    }


}
