using CrmBackend.Application.Commands.RoleCommands;
using CrmBackend.Domain.Entities;
using CrmBackend.Domain.Services;

namespace CrmBackend.Application.Handlers.RoleHandlers
{
    public class UpdateRoleCommandHandler
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IRoleClaimRepository _roleClaimRepository;

        public UpdateRoleCommandHandler(
            IRoleRepository roleRepository,
            IRoleClaimRepository roleClaimRepository)
        {
            _roleRepository = roleRepository;
            _roleClaimRepository = roleClaimRepository;
        }

        public async Task Handle(UpdateRoleCommand command)
        {
            var role = await _roleRepository.GetByIdAsync(command.RoleId);
            if (role == null)
                throw new Exception("Role not found.");

            // تحديث اسم الدور
            role.Name = command.RoleName;
            await _roleRepository.UpdateAsync(role);

            // نحذف كل الصلاحيات القديمة
            await _roleClaimRepository.DeleteByRoleIdAsync(role.Id);

            // ونضيف الصلاحيات الجديدة
            foreach (var claim in command.Claims)
            {
                var roleClaim = new RoleClaim
                {
                    RoleId = role.Id,
                    Type = "permission",
                    Value = claim
                };

                await _roleClaimRepository.AddAsync(roleClaim);
            }
        }
    }
}
