using CrmBackend.Application.Commands.CustomerCommands;
using CrmBackend.Domain.Entities;
using CrmBackend.Domain.Enums;
using CrmBackend.Domain.Services;

namespace CrmBackend.Application.Handlers.CustomerHandlers;

public class UpdateCustomerHandler
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IUserRepository _userRepository;

    public UpdateCustomerHandler(ICustomerRepository customerRepository, IUserRepository userRepository)
    {
        _customerRepository = customerRepository;
        _userRepository = userRepository;
    }

    public async Task Handle(UpdateCustomerCommand command)
    {
        var customer = await _customerRepository.GetByIdAsync(command.CustomerId);
        if (customer == null)
            throw new Exception("Customer not found");

        var user = await _userRepository.GetByIdAsync(command.UpdatedBy);
        if (user == null)
            throw new Exception("User not found");

        customer.CustomerName = command.CustomerName;
        customer.CustomerEmail = command.CustomerEmail;
        customer.CustomerContact = command.CustomerContact;
        customer.CustomerWhatsapp = command.CustomerWhatsapp;
        customer.CustomerAddress = command.CustomerAddress;
        customer.CustomerCity = command.CustomerCity;
        customer.CustomerCountry = command.CustomerCountry;
        customer.CustomerNationality = command.CustomerNationality;
        customer.CustomerNotes = command.CustomerNotes;
        customer.CustomerNextMeetingDate = command.CustomerNextMeetingDate;
        customer.ContactStatus = (ContactStatus)command.ContactStatus;
        customer.IsVisitedShowroom = command.IsVisitedShowroom;
        customer.CustomerTimeSpent = command.CustomerTimeSpent;
        customer.WayOfContact = (WayOfContact)command.WayOfContact;
        customer.CustomerAssignedTo = command.CustomerAssignedTo;
        customer.UpdatedBy = command.UpdatedBy;
        customer.UpdatedDate = DateTime.UtcNow;

        await _customerRepository.UpdateAsync(customer);
    }
}
