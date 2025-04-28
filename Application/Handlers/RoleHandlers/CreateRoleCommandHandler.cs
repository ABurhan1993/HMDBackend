using CrmBackend.Application.Commands.RoleCommands;
using CrmBackend.Domain.Entities;
using CrmBackend.Domain.Services;

namespace CrmBackend.Application.Handlers.RoleHandlers
{
    public class CreateRoleCommandHandler
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IRoleClaimRepository _roleClaimRepository;

        public CreateRoleCommandHandler(
            IRoleRepository roleRepository,
            IRoleClaimRepository roleClaimRepository)
        {
            _roleRepository = roleRepository;
            _roleClaimRepository = roleClaimRepository;
        }

        public async Task<Guid> Handle(CreateRoleCommand command)
        {
            var newRole = new Role
            {
                Name = command.RoleName
            };

            await _roleRepository.AddAsync(newRole);

            foreach (var claim in command.Claims)
            {
                var roleClaim = new RoleClaim
                {
                    RoleId = newRole.Id,
                    Type = "permission",
                    Value = claim
                };

                await _roleClaimRepository.AddAsync(roleClaim);
            }

            return newRole.Id;
        }
    }
}
