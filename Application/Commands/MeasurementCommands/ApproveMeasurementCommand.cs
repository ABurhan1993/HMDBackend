namespace CrmBackend.Application.Commands.MeasurementCommands;

public class ApproveMeasurementCommand
{
    public int InquiryId { get; set; }
    public Guid DesignerUserId { get; set; }
    public Guid ApprovedBy { get; set; }
}

