using CrmBackend.Application.Commands.RoleCommands;
using CrmBackend.Domain.Services;

namespace CrmBackend.Application.Handlers.RoleHandlers
{
    public class UpdateRoleCommandHandler
    {
        private readonly IRoleRepository _repository;

        public UpdateRoleCommandHandler(IRoleRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(UpdateRoleCommand command)
        {
            var role = await _repository.GetByIdAsync(command.RoleId);
            if (role == null) throw new Exception("Role not found");

            role.Name = command.Name;
            await _repository.UpdateAsync(role);
        }
    }
}