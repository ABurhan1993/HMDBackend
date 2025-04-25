using CrmBackend.Application.Commands.CustomerCommands;
using CrmBackend.Domain.Entities;
using CrmBackend.Domain.Enums;
using CrmBackend.Domain.Services;
using MediatR;

namespace CrmBackend.Application.Handlers.CustomerHandlers;

public class CreateCustomerCommandHandler
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IUserRepository _userRepository;
    private readonly ICustomerCommentRepository _customerCommentRepository;

    public CreateCustomerCommandHandler(
        ICustomerRepository customerRepository,
        IUserRepository userRepository,
        ICustomerCommentRepository customerCommentRepository)
    {
        _customerRepository = customerRepository;
        _userRepository = userRepository;
        _customerCommentRepository = customerCommentRepository;
    }

    public async Task<int> Handle(CreateCustomerCommand command)
    {
        var user = await _userRepository.GetByIdAsync(command.CreatedBy);
        if (user == null)
            throw new Exception("Invalid user");


        var customer = new Customer
        {
            CustomerName = command.CustomerName,
            CustomerEmail = command.CustomerEmail,
            CustomerContact = command.CustomerContact,
            CustomerWhatsapp = command.CustomerWhatsapp,
            CustomerAddress = command.CustomerAddress,
            CustomerCity = command.CustomerCity,
            CustomerCountry = command.CustomerCountry,
            CustomerNationality = command.CustomerNationality,
            CustomerNextMeetingDate = command.CustomerNextMeetingDate.HasValue
                ? DateTime.SpecifyKind(command.CustomerNextMeetingDate.Value, DateTimeKind.Utc)
                : null,
            ContactStatus = (ContactStatus)command.ContactStatus,
            IsVisitedShowroom = command.IsVisitedShowroom,
            CustomerTimeSpent = command.CustomerTimeSpent,
            WayOfContact = (WayOfContact)command.WayOfContact,
            BranchId = user.BranchId,
            UserId = user.Id,
            CustomerAssignedTo = command.CustomerAssignedTo,
            CustomerAssignedBy = command.CreatedBy,
            CustomerAssignedDate = DateTime.UtcNow,
            CreatedBy = command.CreatedBy,
            CreatedDate = DateTime.UtcNow,
            IsActive = true,
            IsDeleted = false
        };

        await _customerRepository.AddAsync(customer);

        if (!string.IsNullOrWhiteSpace(command.InitialComment))
        {
            var comment = new CustomerComment
            {
                CustomerId = customer.CustomerId,
                CustomerCommentDetail = command.InitialComment,
                CommentAddedBy = command.CreatedBy,
                CommentAddedOn = DateTime.UtcNow,
                CreatedDate = DateTime.UtcNow,
                CreatedBy = command.CreatedBy
            };

            await _customerCommentRepository.AddAsync(comment);
        }

        return customer.CustomerId;
    }
}
