using CrmBackend.Application.Commands.MeasurementCommands;
using CrmBackend.Application.DTOs.NotificationDtos;
using CrmBackend.Application.Interfaces;
using CrmBackend.Application.Interfaces.Notifications;
using CrmBackend.Domain.Entities;
using CrmBackend.Domain.Enums;
using CrmBackend.Domain.Services;

public class ApproveMeasurementAssignmentCommandHandler
{
    private readonly IInquiryRepository _inquiryRepo;
    private readonly IInquiryTaskRepository _taskRepo;
    private readonly IMeasurementRepository _measurementRepo;
    private readonly INotificationDispatcher _notificationDispatcher;

    public ApproveMeasurementAssignmentCommandHandler(
        IInquiryRepository inquiryRepo,
        IInquiryTaskRepository taskRepo,
        IMeasurementRepository measurementRepo,
        INotificationDispatcher notificationDispatcher)
    {
        _inquiryRepo = inquiryRepo;
        _taskRepo = taskRepo;
        _measurementRepo = measurementRepo;
        _notificationDispatcher = notificationDispatcher;
    }

    public async Task<bool> Handle(ApproveMeasurementAssignmentCommand command, Guid userId)
    {
        // ✅ جلب الاستفسار مع الـ Workscopes
        var inquiry = await _inquiryRepo.GetByIdWithWorkscopesAsync(command.InquiryId);
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

        // ✅ إنشاء مهمة القياس InquiryTask
        var task = new InquiryTask
        {
            InquiryId = inquiry.InquiryId,
            InquiryWorkscopeId = workscope.InquiryWorkscopeId,
            TaskType = TaskType.Measurement,
            TaskName = $"Measurement for {workscope.InquiryWorkscopeDetailName}",
            ScheduledDate = workscope.MeasurementScheduleDate,
            AssignedToUserId = userId,
            ApprovedByUserId = userId,
            ApprovedOn = DateTime.UtcNow,
            CreatedBy = userId,
            CreatedDate = DateTime.UtcNow
        };
        await _taskRepo.AddAsync(task);

        // ✅ إنشاء كيان القياس
        var measurement = new Measurement
        {
            InquiryTask = task,
            CreatedBy = userId,
            CreatedDate = DateTime.UtcNow
        };
        await _measurementRepo.AddAsync(measurement);

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
