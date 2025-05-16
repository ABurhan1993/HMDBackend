namespace CrmBackend.Application.Commands.MeasurementCommands;

public class GetMeasurementApprovalsQuery
{
    public Guid CurrentUserId { get; set; }
    public int BranchId { get; set; }
}

