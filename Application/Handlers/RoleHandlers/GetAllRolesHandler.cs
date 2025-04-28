using CrmBackend.Application.DTOs.RoleDtos;
using CrmBackend.Domain.Services;
using CrmBackend.Infrastructure.Repositories;

namespace CrmBackend.Application.Handlers.RoleHandlers
{
    public class GetAllRolesHandler
    {
        private readonly IRoleRepository _roleRepository;

        public GetAllRolesHandler(IRoleRepository repository)
        {
            _roleRepository = repository;
        }

        public async Task<List<RoleDto>> Handle()
        {
            var roles = await _roleRepository.GetAllAsync();

            var roleDtos = roles.Select(r => new RoleDto
            {
                Id = r.Id,
                Name = r.Name,
                Claims = r.RoleClaims.Select(c => c.Value).ToList()
            }).ToList();

            return roleDtos;
        }

    }
}