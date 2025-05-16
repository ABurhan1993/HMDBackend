using CrmBackend.Application.Commands.MeasurementCommands;
using CrmBackend.Application.Interfaces;
using CrmBackend.Domain.Enums;

namespace CrmBackend.Application.Handlers.MesurementHandlers;

public class ApproveMeasurementHandler
{
    private readonly IInquiryRepository _inquiryRepository;

    public ApproveMeasurementHandler(IInquiryRepository inquiryRepository)
    {
        _inquiryRepository = inquiryRepository;
    }

    public async Task Handle(ApproveMeasurementCommand command)
    {
        var inquiry = await _inquiryRepository.GetByIdWithWorkscopesAndTasksAsync(command.InquiryId);
        if (inquiry == null)
            throw new Exception("Inquiry not found");

        inquiry.Status = InquiryStatus.DesignAssigneePending;
        inquiry.UpdatedBy = command.ApprovedBy;
        inquiry.UpdatedDate = DateTime.UtcNow;

        // تحديث كل الأعمال المرتبطة
        foreach (var workscope in inquiry.InquiryWorkscopes)
        {
            workscope.DesignAssignedTo = command.DesignerUserId;
            workscope.InquiryStatus = InquiryStatus.DesignAssigneePending;
        }

        // تحديث المهام المرتبطة (فقط Tasks من نوع Measurement)
        foreach (var task in inquiry.InquiryTasks.Where(t => t.TaskType == TaskType.Measurement))
        {
            task.ApprovedByUserId = command.ApprovedBy;
            task.ApprovedOn = DateTime.UtcNow;
        }

        await _inquiryRepository.UpdateAsync(inquiry); // هذا بيكفي يحفظ كل شي دفعة وحدة
    }


}

