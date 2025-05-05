using CrmBackend.Application.Commands.CustomerCommands;
using CrmBackend.Application.DTOs.NotificationDtos;
using CrmBackend.Application.Interfaces.Notifications;
using CrmBackend.Domain.Entities;
using CrmBackend.Domain.Enums;
using CrmBackend.Domain.Services;
using CrmBackend.Infrastructure.Repositories;
using CrmBackend.Infrastructure.Services.Notifications;
using MediatR;

namespace CrmBackend.Application.Handlers.CustomerHandlers;

public async Task<int> Handle(CreateCustomerCommand command)
{
    var user = await _userRepository.GetByIdAsync(command.CreatedBy);
    if (user == null)
        throw new Exception("Invalid user");

    // ✅ تحقق إذا العميل موجود بنفس رقم الهاتف
    var existingCustomer = await _customerRepository.GetByContactAsync(command.CustomerContact);
    if (existingCustomer != null)
    {
        // يمكنك بدلًا من هذا إرجاع الـ ID مباشرة إذا أردت
        throw new Exception("A customer with the same contact already exists.");
    }

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

    if (command.CustomerAssignedTo.HasValue)
    {
        await _notificationDispatcher.DispatchAsync(new NotificationMessageDto
        {
            ReceiverUserId = command.CustomerAssignedTo.Value,
            Title = "New Customer Assigned",
            Message = $"Customer {customer.CustomerName} has been assigned to you.",
            Url = $"/customers/{customer.CustomerId}"
        });
    }

    return customer.CustomerId;
}
