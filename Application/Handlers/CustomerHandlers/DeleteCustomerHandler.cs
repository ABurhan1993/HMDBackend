// Application/Handlers/CustomerHandlers/DeleteCustomerHandler.cs
using CrmBackend.Domain.Services;

public class DeleteCustomerHandler
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IUserRepository _userRepository;

    public DeleteCustomerHandler(ICustomerRepository customerRepository, IUserRepository userRepository)
    {
        _customerRepository = customerRepository;
        _userRepository = userRepository;
    }

    public async Task Handle(DeleteCustomerCommand command)
    {
        var customer = await _customerRepository.GetByIdAsync(command.CustomerId);
        if (customer == null) throw new Exception("Customer not found");

        var user = await _userRepository.GetByIdAsync(command.DeletedBy);
        if (user == null) throw new Exception("User not found");

        customer.IsDeleted = true;
        customer.UpdatedBy = command.DeletedBy;
        customer.UpdatedDate = DateTime.UtcNow;

        await _customerRepository.UpdateAsync(customer);
    }
}
