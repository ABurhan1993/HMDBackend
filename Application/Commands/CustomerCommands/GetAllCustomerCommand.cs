namespace CrmBackend.Application.Commands.CustomerCommands;

public class GetAllCustomersCommand { }

public class GetCustomersByBranchCommand
{
    public int BranchId { get; set; }
}