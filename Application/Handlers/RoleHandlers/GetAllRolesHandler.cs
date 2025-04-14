using CrmBackend.Domain.Services;

namespace CrmBackend.Application.Handlers.RoleHandlers
{
    public class GetAllRolesHandler
    {
        private readonly IRoleRepository _repository;

        public GetAllRolesHandler(IRoleRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<object>> Handle()
        {
            var roles = await _repository.GetAllAsync();
            return roles.Select(r => new { r.Id, r.Name });
        }
    }
}