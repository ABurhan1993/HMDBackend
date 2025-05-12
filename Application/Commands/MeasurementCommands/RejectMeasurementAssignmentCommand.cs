namespace CrmBackend.Application.Commands.MeasurementCommands;

public class RejectMeasurementAssignmentCommand
{
    public int InquiryId { get; set; }
    public string RejectionReason { get; set; }
}
