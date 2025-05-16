namespace CrmBackend.Application.Commands.MeasurementCommands;

public class RejectMeasurementCommand
{
    public int InquiryId { get; set; }
    public Guid RejectedBy { get; set; }
}

