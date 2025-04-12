namespace CrmBackend.Application.Commands.CustomerCommands;

public class UpdateCustomerAssignmentCommand
{
    public int CustomerId { get; set; }
    public Guid AssignedTo { get; set; }
    public Guid UpdatedBy { get; set; }
    public Guid AssignedBy { get; set; }
}
