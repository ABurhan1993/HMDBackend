using CrmBackend.Application.DTOs.UsersDtos;
using CrmBackend.Domain.Services;

namespace CrmBackend.Application.Handlers.UserHandlers;

public class GetAllUsersHandler
{
    private readonly IUserRepository _userRepository;

    public GetAllUsersHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<IEnumerable<UserSelectDto>> Handle()
    {
        var users = await _userRepository.GetAllAsync();
        return users
            .Where(u => u.IsActive && !u.IsDeleted)
            .Select(u => new UserSelectDto
            {
                Value = u.Id,
                Label = u.FullName
            });
    }
}

