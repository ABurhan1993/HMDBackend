using CrmBackend.Domain.Entities;

namespace CrmBackend.Application.Interfaces;

public interface IAuthenticationService
{
    string GenerateToken(User user);
}
