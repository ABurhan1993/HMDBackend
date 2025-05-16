using CrmBackend.Application.Commands.MeasurementCommands;
using CrmBackend.Application.DTOs.InquiryDtos;
using CrmBackend.Application.Interfaces;
using CrmBackend.Domain.Enums;

namespace CrmBackend.Application.Handlers.MesurementHandlers;

public class GetMeasurementApprovalsHandler
{
    private readonly IInquiryRepository _inquiryRepository;

    public GetMeasurementApprovalsHandler(IInquiryRepository inquiryRepository)
    {
        _inquiryRepository = inquiryRepository;
    }

    public async Task<List<InquiryListDto>> Handle(GetMeasurementApprovalsQuery query)
    {
        return await _inquiryRepository.GetMeasurementApprovalsAsync(query.BranchId, query.CurrentUserId);
    }
}

