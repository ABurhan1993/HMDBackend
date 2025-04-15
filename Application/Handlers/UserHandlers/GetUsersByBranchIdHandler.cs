using CrmBackend.Application.DTOs.UserDTOs;
using CrmBackend.Domain.Services;

public class GetUsersByBranchIdHandler
{
    private readonly IUserRepository _userRepository;

    public GetUsersByBranchIdHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<List<UserDto>> Handle(int branchId)
    {
        var users = await _userRepository.GetUsersByBranchIdAsync(branchId);
        return users.Select(UserDto.FromEntity).ToList();
    }
}
