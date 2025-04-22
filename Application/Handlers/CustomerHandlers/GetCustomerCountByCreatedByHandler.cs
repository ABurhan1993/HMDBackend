using CrmBackend.Application.DTOs.CustomersDTOs;
using CrmBackend.Domain.Services;

namespace CrmBackend.Application.Handlers.CustomerHandlers;

public class GetCustomerCountByCreatedByHandler
{
    private readonly ICustomerRepository _customerRepository;

    public GetCustomerCountByCreatedByHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<List<CustomerCountByUserDto>> HandleAsync(int branchId)
    {
        return await _customerRepository.GetCountGroupedByCreatedByAsync(branchId);
    }
}
