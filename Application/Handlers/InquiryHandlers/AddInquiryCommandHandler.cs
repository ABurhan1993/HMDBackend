using CrmBackend.Application.Commands.InquiryCommands;
using CrmBackend.Application.DTOs.NotificationDtos;
using CrmBackend.Application.Interfaces;
using CrmBackend.Application.Interfaces.Notifications;
using CrmBackend.Domain.Services;

namespace CrmBackend.Application.Handlers.InquiryHandlers
{
    public class AddInquiryCommandHandler
    {
        private readonly IInquiryRepository _inquiryRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly INotificationDispatcher _notificationDispatcher;

        public AddInquiryCommandHandler(
    IInquiryRepository inquiryRepository,
    IHttpContextAccessor httpContextAccessor,
    INotificationDispatcher notificationDispatcher // ✅ جديد
)
        {
            _inquiryRepository = inquiryRepository;
            _httpContextAccessor = httpContextAccessor;
            _notificationDispatcher = notificationDispatcher;
        }

        public async Task<int> Handle(AddInquiryCommand command, Guid userId, int branchId)
        {
            var request = command.Request;

            // ✅ استرجاع الكوستمر مرة واحدة فقط
            var customer = request.CustomerId.HasValue && request.CustomerId.Value > 0
                ? await _inquiryRepository.GetCustomerByIdAsync(request.CustomerId.Value)
                : await _inquiryRepository.AddCustomerFromInquiry(request, branchId, userId);

            // ✅ تحديث الكوستمر فقط إذا تغيرت بياناته
            await _inquiryRepository.UpdateCustomerAsyncIfNeeded(customer, request, userId);

            // ➕ أضف المبنى
            var building = await _inquiryRepository.AddBuildingAsync(request, userId);

            // ➕ أضف الاستفسار
            var inquiry = await _inquiryRepository.AddInquiryAsync(
                request,
                customer.CustomerId,
                building.BuildingId,
                branchId,
                userId
            );

            // ➕ أضف الـ Workscopes
            await _inquiryRepository.AddInquiryWorkscopesAsync(inquiry.InquiryId, request, userId);



            // ✅ حفظ
            await _inquiryRepository.SaveChangesAsync();

            // ✅ إشعار للمستخدم المكلّف بالقياس
            if (request.MeasurementAssignedTo.HasValue)
            {
                await _notificationDispatcher.DispatchAsync(new NotificationMessageDto
                {
                    ReceiverUserId = request.MeasurementAssignedTo.Value,
                    Title = "Measurement Assignment",
                    Message = $"You have been assigned to take measurements for customer {customer.CustomerName}.",
                    Url = $"/inquiries/{inquiry.InquiryId}"
                });
            }

            if (request.CustomerAssignedTo.HasValue)
            {
                await _notificationDispatcher.DispatchAsync(new NotificationMessageDto
                {
                    ReceiverUserId = request.CustomerAssignedTo.Value,
                    Title = "New Customer Assigned",
                    Message = $"Customer {customer.CustomerName} has been assigned to you.",
                    Url = $"/customers/{customer.CustomerId}"
                });
            }


            return inquiry.InquiryId;
        }
    }
}
