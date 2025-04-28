namespace CrmBackend.Application.DTOs.RoleDtos;

public class RoleDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public List<string> Claims { get; set; } = new();
}
