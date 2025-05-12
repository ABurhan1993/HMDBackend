using CrmBackend.Domain.Entities;

namespace CrmBackend.Domain.Services;

public interface IInquiryTaskRepository
{
    Task AddAsync(InquiryTask task);
    Task<InquiryTask?> GetMeasurementTaskByInquiryAndUserAsync(int inquiryId, Guid userId);
    Task UpdateAsync(InquiryTask task);
}
