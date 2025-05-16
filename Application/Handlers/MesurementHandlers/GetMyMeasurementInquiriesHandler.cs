using CrmBackend.Application.DTOs.InquiryDtos;
using CrmBackend.Application.Interfaces;
using CrmBackend.Domain.Enums;

public class GetMyMeasurementInquiriesHandler
{
    private readonly IInquiryRepository _inquiryRepo;

    public GetMyMeasurementInquiriesHandler(IInquiryRepository inquiryRepo)
    {
        _inquiryRepo = inquiryRepo;
    }

    public async Task<List<InquiryListDto>> Handle(Guid userId)
    {
        var inquiries = await _inquiryRepo.GetMeasurementInquiriesInProgressAsync(userId);

        return inquiries.Select(i => new InquiryListDto
        {
            InquiryId = i.InquiryId,
            InquiryCode = i.InquiryCode,
            InquiryDescription = i.InquiryDescription,
            InquiryStartDate = i.InquiryStartDate,
            InquiryEndDate = i.InquiryEndDate,
            InquiryStatusName = i.Status.ToString(),
            ManagedByUserName = i.ManagedByUser?.FullName ?? "",

            IsMeasurementProvidedByCustomer = i.IsMeasurementProvidedByCustomer,
            IsDesignProvidedByCustomer = i.IsDesignProvidedByCustomer,

            // ✅ Customer Info
            CustomerName = i.Customer?.CustomerName ?? "",
            CustomerContact = i.Customer?.CustomerContact ?? "",
            CustomerEmail = i.Customer?.CustomerEmail ?? "",
            CustomerNotes = i.Customer?.CustomerNotes ?? "",
            CustomerNextMeetingDate = i.Customer?.CustomerNextMeetingDate,
            ContactStatus = i.Customer?.ContactStatus.ToString() ?? "",
            WayOfContact = i.Customer?.WayOfContact.ToString() ?? "",

            // ✅ Building Info
            BuildingAddress = i.Building?.BuildingAddress ?? "",
            BuildingMakaniMap = i.Building?.BuildingMakaniMap ?? "",
            BuildingTypeOfUnit = i.Building?.BuildingTypeOfUnit,
            BuildingCondition = i.Building?.BuildingCondition,
            BuildingFloor = i.Building?.BuildingFloor ?? "",
            BuildingReconstruction = i.Building?.BuildingReconstruction,
            IsOccupied = i.Building?.IsOccupied,

            // ✅ Workscope Info
            WorkscopeNames = i.InquiryWorkscopes.Select(w => w.InquiryWorkscopeDetailName).ToList(),
            WorkscopeDetails = i.InquiryWorkscopes.Select(w => new InquiryWorkscopeDisplayDto
            {
                WorkscopeName = w.WorkScope?.WorkScopeName ?? "",
                InquiryWorkscopeDetailName = w.InquiryWorkscopeDetailName,
                InquiryStatus = w.InquiryStatus.ToString(),
                MeasurementAssignedTo = w.MeasurementAssignedUser?.FullName ?? "",
                DesignAssignedTo = w.DesignAssignedUser?.FullName ?? "",
                MeasurementScheduleDate = w.MeasurementScheduleDate,
                DesignScheduleDate = w.DesignScheduleDate,
                MeasurementAddedOn = w.MeasurementAddedOn,
                DesignAddedOn = w.DesignAddedOn,
                IsDesignApproved = w.IsDesignApproved,
                IsDesignSentToCustomer = w.IsDesignSentToCustomer,
                FeedbackReaction = w.FeedbackReaction.ToString(),
                IsMeasurementReschedule = w.IsMeasurementReschedule,
                IsDesignReschedule = w.IsDesignReschedule
            }).ToList(),

            Comments = i.Comments.Select(c => new InquiryCommentDto
            {
                CommentName = c.CommentName,
                CommentDetail = c.CommentDetail,
                AddedBy = c.CommentAddedByUser?.FullName ?? "",
                AddedOn = c.CommentAddedOn,
                NextFollowup = c.CommentNextFollowup
            }).ToList()
        }).ToList();
    }
}
