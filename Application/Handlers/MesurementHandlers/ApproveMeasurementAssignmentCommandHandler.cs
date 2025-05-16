using CrmBackend.Application.Commands.MeasurementCommands;
using CrmBackend.Application.DTOs.NotificationDtos;
using CrmBackend.Application.Interfaces;
using CrmBackend.Application.Interfaces.Notifications;
using CrmBackend.Domain.Enums;

public class ApproveMeasurementAssignmentCommandHandler
{
    private readonly IInquiryRepository _inquiryRepo;
    private readonly INotificationDispatcher _notificationDispatcher;

    public ApproveMeasurementAssignmentCommandHandler(
        IInquiryRepository inquiryRepo,
        INotificationDispatcher notificationDispatcher)
    {
        _inquiryRepo = inquiryRepo;
        _notificationDispatcher = notificationDispatcher;
    }

    public async Task<bool> Handle(ApproveMeasurementAssignmentCommand command, Guid userId)
    {
        // ✅ جلب الاستفسار مع الـ Workscopes
        var inquiry = await _inquiryRepo.GetByIdWithWorkscopesAndTasksAsync(command.InquiryId);
        if (inquiry == null || !inquiry.InquiryWorkscopes.Any())
            return false;

        // ✅ العثور على الـ Workscope المخصص للمستخدم الحالي
        var workscope = inquiry.InquiryWorkscopes
            .FirstOrDefault(ws => ws.MeasurementAssignedTo == userId);

        if (workscope == null)
            return false;

        // ✅ تحديث حالات الاستفسار والـ Workscope
        inquiry.Status = InquiryStatus.MeasurementInProgress;
        workscope.InquiryStatus = InquiryStatus.MeasurementInProgress;

        // ✅ إشعار للمسؤول
        if (inquiry.ManagedBy.HasValue)
        {
            await _notificationDispatcher.DispatchAsync(new NotificationMessageDto
            {
                ReceiverUserId = inquiry.ManagedBy.Value,
                Title = "Measurement Accepted",
                Message = $"Measurement assignment for inquiry {inquiry.InquiryCode} has been accepted.",
                Url = $"/inquiries/{inquiry.InquiryId}"
            });
        }

        await _inquiryRepo.UpdateAsync(inquiry); // حفظ كل شيء دفعة وحدة
        return true;
    }
}
