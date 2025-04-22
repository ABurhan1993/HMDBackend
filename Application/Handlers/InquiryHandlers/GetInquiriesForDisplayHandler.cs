using CrmBackend.Application.DTOs.InquiryDtos;
using CrmBackend.Application.Interfaces;

namespace CrmBackend.Application.Handlers.InquiryHandlers;

public class GetInquiriesForDisplayHandler
{
    private readonly IInquiryRepository _inquiryRepository;

    public GetInquiriesForDisplayHandler(IInquiryRepository inquiryRepository)
    {
        _inquiryRepository = inquiryRepository;
    }

    public async Task<List<InquiryListDto>> Handle(int branchId)
    {
        return await _inquiryRepository.GetAllForDisplayAsync(branchId);
    }
}
