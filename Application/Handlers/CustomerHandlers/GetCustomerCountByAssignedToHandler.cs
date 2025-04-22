using CrmBackend.Application.DTOs.CustomersDTOs;
using CrmBackend.Domain.Services;

namespace CrmBackend.Application.Handlers.CustomerHandlers;

public class GetCustomerCountByAssignedToHandler
{
    private readonly ICustomerRepository _customerRepository;

    public GetCustomerCountByAssignedToHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<List<CustomerCountByUserDto>> HandleAsync(int branchId)
    {
        return await _customerRepository.GetCountGroupedByAssignedToAsync(branchId);
    }
}
