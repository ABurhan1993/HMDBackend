using CrmBackend.Domain.Entities;

namespace CrmBackend.Application.DTOs.UserDTOs;

public class UserDto
{
    public Guid Id { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string RoleName { get; set; }
    public string BranchName { get; set; }

    public static UserDto FromEntity(User u) => new()
    {
        Id = u.Id,
        FullName = u.FullName,
        Email = u.Email,
        Phone = u.Phone,
        RoleName = u.Role?.Name ?? "",
        BranchName = u.Branch?.Name ?? ""
    };
}
