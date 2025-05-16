using CrmBackend.Application.Commands.MeasurementCommands;
using CrmBackend.Application.Interfaces;
using CrmBackend.Domain.Enums;
using CrmBackend.Domain.Services;

namespace CrmBackend.Application.Handlers.MesurementHandlers;

public class RejectMeasurementHandler
{
    private readonly IInquiryRepository _inquiryRepository;
    private readonly IMeasurementRepository _measurementRepository;
    private readonly IInquiryTaskRepository _taskRepository;
    private readonly ITaskFileRepository _fileRepository;

    public RejectMeasurementHandler(
        IInquiryRepository inquiryRepository,
        IMeasurementRepository measurementRepository,
        IInquiryTaskRepository taskRepository,
        ITaskFileRepository fileRepository)
    {
        _inquiryRepository = inquiryRepository;
        _measurementRepository = measurementRepository;
        _taskRepository = taskRepository;
        _fileRepository = fileRepository;
    }
    public async Task Handle(RejectMeasurementCommand command)
    {
        var inquiry = await _inquiryRepository.GetByIdWithWorkscopesAndTasksAsync(command.InquiryId);
        if (inquiry == null)
            throw new Exception("Inquiry not found");

        // ✅ تحديث حالة الاستفسار
        inquiry.Status = InquiryStatus.MeasurementRejected;
        inquiry.UpdatedBy = command.RejectedBy;
        inquiry.UpdatedDate = DateTime.UtcNow;

        // ✅ تحديث حالة كل Workscope
        foreach (var workscope in inquiry.InquiryWorkscopes)
        {
            workscope.InquiryStatus = InquiryStatus.MeasurementRejected;
        }

        // ✅ حذف فقط المهام من نوع القياس
        var measurementTasks = inquiry.InquiryTasks
            .Where(t => t.TaskType == TaskType.Measurement && !t.IsDeleted)
            .ToList();

        foreach (var task in measurementTasks)
        {
            task.IsDeleted = true;
            await _taskRepository.UpdateAsync(task);

            var measurement = await _measurementRepository.GetByTaskIdAsync(task.Id);
            if (measurement != null)
            {
                measurement.IsDeleted = true;
                await _measurementRepository.UpdateAsync(measurement);
            }

            var files = await _fileRepository.GetByTaskIdAsync(task.Id);
            foreach (var file in files)
            {
                file.IsDeleted = true;
                await _fileRepository.UpdateAsync(file);
            }
        }

        await _inquiryRepository.UpdateAsync(inquiry); // احفظ كل التغييرات
    }

}



