using CrmBackend.Application.Commands.CustomerCommands;
using CrmBackend.Domain.Entities;
using CrmBackend.Domain.Services;

namespace CrmBackend.Application.Handlers.CustomerHandlers;

public class GetCustomerByPhoneHandler
{
    private readonly ICustomerRepository _customerRepository;

    public GetCustomerByPhoneHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<Customer?> Handle(GetCustomerByPhoneCommand command)
    {
        return await _customerRepository.FindByPhoneAsync(command.Phone);
    }
}
