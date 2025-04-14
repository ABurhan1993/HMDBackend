using CrmBackend.Domain.Services;
using Microsoft.EntityFrameworkCore;
using CrmBackend.Application.DTOs.UserDTOs;

namespace CrmBackend.Application.Handlers.UserHandlers;

public class GetAllUsersHandler
{
    private readonly IUserRepository _userRepository;

    public GetAllUsersHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<IEnumerable<UserDto>> Handle(int branchId)
    {
        var users = await _userRepository.Query()
            .Include(u => u.Role)
            .Include(u => u.Branch)
            .Where(u => u.BranchId == branchId && u.IsActive && !u.IsDeleted)
            .ToListAsync();

        return users.Select(UserDto.FromEntity);
    }
}
