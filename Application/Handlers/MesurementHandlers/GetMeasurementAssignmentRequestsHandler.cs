using CrmBackend.Application.Commands.MeasurementCommands;
using CrmBackend.Application.DTOs.InquiryDtos;
using CrmBackend.Application.Interfaces;
using CrmBackend.Domain.Enums;

namespace CrmBackend.Application.Handlers.MesurementHandlers;

public class GetMeasurementAssignmentRequestsHandler
{
    private readonly IInquiryRepository _repository;

    public GetMeasurementAssignmentRequestsHandler(IInquiryRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<InquiryListDto>> Handle(GetMeasurementAssignmentRequestsQuery query)
    {
        return await _repository.GetMeasurementAssignmentRequestsAsync(query.UserId);
        
    }
}