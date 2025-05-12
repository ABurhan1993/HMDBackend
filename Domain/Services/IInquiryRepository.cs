using CrmBackend.Application.DTOs.InquiryDtos;
using CrmBackend.Domain.Entities;

namespace CrmBackend.Application.Interfaces
{
    public interface IInquiryRepository
    {
        Task<Customer> AddCustomerFromInquiry(CreateInquiryRequest request, int branchId, Guid userId);
        Task<Building> AddBuildingAsync(CreateInquiryRequest request, Guid userId);
        Task<Inquiry> AddInquiryAsync(CreateInquiryRequest request, int customerId, int buildingId, int branchId, Guid createdBy);
        Task AddInquiryWorkscopesAsync(int inquiryId, CreateInquiryRequest request, Guid userId);

        Task<List<InquiryListDto>> GetAllForDisplayAsync(int branchId);
        Task<Customer?> GetCustomerByIdAsync(int customerId);
        Task UpdateCustomerAsyncIfNeeded(Customer customer, CreateInquiryRequest request, Guid userId);
        Task<List<InquiryListDto>> GetMeasurementAssignmentRequestsAsync(Guid userId);
        Task<Inquiry?> GetByIdAsync(int inquiryId);
        Task AddCommentAsync(Comment comment);
        Task<Inquiry?> GetByIdWithWorkscopesAsync(int inquiryId);

        Task UpdateAsync(Inquiry inquiry);

        Task SaveChangesAsync();
    }
}
