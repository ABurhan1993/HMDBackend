namespace CrmBackend.Application.DTOs.CustomersDTOs;

public class CustomerCountByUserDto
{
    public Guid UserId { get; set; }
    public string? UserName { get; set; }
    public int Count { get; set; }
}


