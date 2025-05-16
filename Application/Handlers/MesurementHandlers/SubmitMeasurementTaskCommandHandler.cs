

using CrmBackend.Application.Commands.MeasurementCommands;
using CrmBackend.Application.DTOs.NotificationDtos;
using CrmBackend.Application.Interfaces;
using CrmBackend.Application.Interfaces.Notifications;
using CrmBackend.Domain.Entities;
using CrmBackend.Domain.Enums;
using CrmBackend.Domain.Services;
using global::CrmBackend.Application.Commands.MeasurementCommands;
using global::CrmBackend.Application.Interfaces;
using global::CrmBackend.Domain.Entities;
using global::CrmBackend.Domain.Enums;
using global::CrmBackend.Domain.Services;
using global::CrmBackend.Infrastructure.Services;
using Microsoft.AspNetCore.Http;

namespace CrmBackend.Application.Handlers.MesurementHandlers;

public class SubmitMeasurementTaskCommandHandler
{
    private readonly IInquiryRepository _inquiryRepo;
    private readonly IInquiryTaskRepository _taskRepo;
    private readonly IMeasurementRepository _measurementRepo;
    private readonly IFileUploader _fileUploader;
    private readonly INotificationDispatcher _notificationDispatcher;

    public SubmitMeasurementTaskCommandHandler(
        IInquiryRepository inquiryRepo,
        IInquiryTaskRepository taskRepo,
        IMeasurementRepository measurementRepo,
        IFileUploader fileUploader,
        INotificationDispatcher notificationDispatcher)
    {
        _inquiryRepo = inquiryRepo;
        _taskRepo = taskRepo;
        _measurementRepo = measurementRepo;
        _fileUploader = fileUploader;
        _notificationDispatcher = notificationDispatcher;
    }

    public async Task<bool> Handle(SubmitMeasurementTaskCommand command, Guid userId)
    {
        var inquiry = await _inquiryRepo.GetByIdWithWorkscopesAndTasksAsync(command.InquiryId);
        if (inquiry == null )
            return false;

        // ✅ إنشاء المهمة
        var task = new InquiryTask
        {
            InquiryId = inquiry.InquiryId,
            TaskType = TaskType.Measurement,
            TaskName = "Measurement Submission",
            TaskComment = command.Comment,
            AssignedToUserId = userId,
            ApprovedOn = DateTime.UtcNow,
            CreatedBy = userId,
            CreatedDate = DateTime.UtcNow
        };

        // ✅ رفع الملفات وربطها
        foreach (var file in command.Files)
        {
            using var stream = file.OpenReadStream();
            var url = await _fileUploader.UploadFileAsync(
                stream,
                file.FileName,
                file.ContentType,
                $"inquiries/{inquiry.InquiryCode}/measurement");

            task.Files.Add(new TaskFile
            {
                FileName = file.FileName,
                FilePath = url,
                FileType = file.ContentType
            });
        }

        await _taskRepo.AddAsync(task);

        // ✅ إنشاء كيان القياس
        var measurement = new Measurement
        {
            InquiryTaskId = task.Id,
            CreatedBy = userId,
            CreatedDate = DateTime.UtcNow
        };
        await _measurementRepo.AddAsync(measurement);

        // ✅ تحديث حالة الاستفسار و الـ Workscopes
        inquiry.Status = InquiryStatus.MeasurementWaitingForApproval;
        inquiry.UpdatedBy = userId;
        inquiry.UpdatedDate = DateTime.UtcNow;

        foreach (var workscope in inquiry.InquiryWorkscopes)
        {
            workscope.InquiryStatus = InquiryStatus.MeasurementWaitingForApproval;
        }

        await _inquiryRepo.UpdateAsync(inquiry);

        // ✅ إشعار للمسؤول
        if (inquiry.ManagedBy.HasValue)
        {
            await _notificationDispatcher.DispatchAsync(new NotificationMessageDto
            {
                ReceiverUserId = inquiry.ManagedBy.Value,
                Title = "Measurement Submitted",
                Message = $"Measurement for inquiry {inquiry.InquiryCode} has been submitted and is waiting for approval.",
                Url = $"/inquiries/{inquiry.InquiryId}"
            });
        }

        return true;
    }

}

