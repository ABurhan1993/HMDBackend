using CrmBackend.Application.Commands.CustomerCommands;
using CrmBackend.Domain.Entities;
using CrmBackend.Domain.Enums;
using CrmBackend.Domain.Services;
using MediatR;

namespace CrmBackend.Application.Handlers.CustomerHandlers;

public class UpdateCustomerHandler
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IUserRepository _userRepository;
    private readonly ICustomerCommentRepository _customerCommentRepository;

    public UpdateCustomerHandler(
        ICustomerRepository customerRepository,
        IUserRepository userRepository,
        ICustomerCommentRepository customerCommentRepository)
    {
        _customerRepository = customerRepository;
        _userRepository = userRepository;
        _customerCommentRepository = customerCommentRepository;
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
        customer.CustomerNextMeetingDate = command.CustomerNextMeetingDate.HasValue
            ? DateTime.SpecifyKind(command.CustomerNextMeetingDate.Value, DateTimeKind.Utc) : null;
        customer.ContactStatus = (ContactStatus)command.ContactStatus;
        customer.IsVisitedShowroom = command.IsVisitedShowroom;
        customer.CustomerTimeSpent = command.CustomerTimeSpent;
        customer.WayOfContact = (WayOfContact)command.WayOfContact;
        customer.CustomerAssignedTo = command.CustomerAssignedTo;
        customer.UpdatedBy = command.UpdatedBy;
        customer.UpdatedDate = DateTime.UtcNow;

        if (command.CustomerAssignedTo != null || customer.CustomerAssignedTo != command.CustomerAssignedTo)
        {
            customer.CustomerAssignedBy = command.UpdatedBy;
            customer.CustomerAssignedDate = DateTime.UtcNow;
        }

        await _customerRepository.UpdateAsync(customer);

        if (!string.IsNullOrWhiteSpace(command.CommentDetail))
        {
            var comment = new CustomerComment
            {
                CustomerId = customer.CustomerId,
                CustomerCommentDetail = command.CommentDetail,
                CommentAddedBy = command.UpdatedBy,
                CommentAddedOn = DateTime.UtcNow,
                CreatedDate = DateTime.UtcNow,
                CreatedBy = command.UpdatedBy
            };

            await _customerCommentRepository.AddAsync(comment);
        }
    }
}
