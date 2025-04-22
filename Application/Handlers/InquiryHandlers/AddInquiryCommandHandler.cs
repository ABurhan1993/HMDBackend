using CrmBackend.Application.Commands.InquiryCommands;
using CrmBackend.Application.Interfaces;
using CrmBackend.Domain.Services;

namespace CrmBackend.Application.Handlers.InquiryHandlers
{
    public class AddInquiryCommandHandler
    {
        private readonly IInquiryRepository _inquiryRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AddInquiryCommandHandler(IInquiryRepository inquiryRepository, IHttpContextAccessor httpContextAccessor)
        {
            _inquiryRepository = inquiryRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<int> Handle(AddInquiryCommand command, Guid userId, int branchId)
        {
            var request = command.Request;

            if (!PhoneValidator.IsValidUAEPhone(request.CustomerContact))
                throw new Exception("Phone number must start with '971' and be 12 digits.");

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

            return inquiry.InquiryId;
        }
    }
}
