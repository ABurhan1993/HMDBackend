using CrmBackend.Application.Commands.CustomerCommands;
using CrmBackend.Domain.Services;

namespace CrmBackend.Application.Handlers.CustomerHandlers
{
    public class UpdateCustomerAssignmentHandler
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IUserRepository _userRepository;

        public UpdateCustomerAssignmentHandler(ICustomerRepository customerRepository, IUserRepository userRepository)
        {
            _customerRepository = customerRepository;
            _userRepository = userRepository;
        }

        public async Task Handle(UpdateCustomerAssignmentCommand command)
        {
            var customer = await _customerRepository.GetByIdAsync(command.CustomerId);
            if (customer == null)
                throw new Exception("Customer not found");

            var user = await _userRepository.GetByIdAsync(command.AssignedTo);
            if (user == null)
                throw new Exception("Assigned user not found");

            customer.CustomerAssignedTo = command.AssignedTo;
            customer.CustomerAssignedBy = command.AssignedBy;
            customer.CustomerAssignedDate = DateTime.UtcNow.ToString("s");
            customer.UpdatedBy = command.AssignedBy;
            customer.UpdatedDate = DateTime.UtcNow.ToString("s");

            await _customerRepository.UpdateAsync(customer);
        }
    }
}
