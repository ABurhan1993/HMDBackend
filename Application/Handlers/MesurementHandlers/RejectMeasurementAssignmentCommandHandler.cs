using CrmBackend.Application.Commands.MeasurementCommands;
using CrmBackend.Application.DTOs.NotificationDtos;
using CrmBackend.Application.Interfaces;
using CrmBackend.Application.Interfaces.Notifications;
using CrmBackend.Domain.Entities;
using CrmBackend.Domain.Enums;
using CrmBackend.Domain.Services;

namespace CrmBackend.Application.Handlers.MesurementHandlers;

public class RejectMeasurementAssignmentCommandHandler
{
    private readonly IInquiryTaskRepository _taskRepo;
    private readonly IInquiryRepository _inquiryRepo;
    private readonly INotificationDispatcher _notificationDispatcher;

    public RejectMeasurementAssignmentCommandHandler(
        IInquiryTaskRepository taskRepo,
        IInquiryRepository inquiryRepo,
        INotificationDispatcher notificationDispatcher)
    {
        _taskRepo = taskRepo;
        _inquiryRepo = inquiryRepo;
        _notificationDispatcher = notificationDispatcher;
    }
    public async Task<bool> Handle(RejectMeasurementAssignmentCommand command, Guid userId)
    {
        var inquiry = await _inquiryRepo.GetByIdWithWorkscopesAndTasksAsync(command.InquiryId);
        if (inquiry == null || !inquiry.InquiryWorkscopes.Any())
            return false;

        // ✅ تحديد الـ Workscope المرتبط بالمستخدم
        var workscope = inquiry.InquiryWorkscopes
            .FirstOrDefault(ws => ws.MeasurementAssignedTo == userId);

        if (workscope == null)
            return false;

        // ✅ تحديث الحالتين
        inquiry.Status = InquiryStatus.MeasurementAssigneeRejected;
        workscope.InquiryStatus = InquiryStatus.MeasurementAssigneeRejected;

        // ✅ إضافة تعليق
        var comment = new Comment
        {
            InquiryId = inquiry.InquiryId,
            InquiryWorkscopeId = workscope.InquiryWorkscopeId,
            CommentName = "Measurement Assignment Rejected",
            CommentDetail = command.RejectionReason,
            InquiryStatus = InquiryStatus.MeasurementAssigneeRejected,
            CommentAddedBy = userId,
            CommentAddedOn = DateTime.UtcNow
        };

        await _inquiryRepo.AddCommentAsync(comment);
        await _inquiryRepo.UpdateAsync(inquiry);

        // ✅ إشعار لمدير الاستفسار
        if (inquiry.ManagedBy.HasValue)
        {
            await _notificationDispatcher.DispatchAsync(new NotificationMessageDto
            {
                ReceiverUserId = inquiry.ManagedBy.Value,
                Title = "Measurement Assignment Rejected",
                Message = $"Measurement assignment for inquiry {inquiry.InquiryCode} was rejected.",
                Url = $"/inquiries/{inquiry.InquiryId}"
            });
        }

        return true;
    }

}

