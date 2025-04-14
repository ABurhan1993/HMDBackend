using CrmBackend.Application.Commands.RoleCommands;
using CrmBackend.Domain.Entities;
using CrmBackend.Domain.Services;

namespace CrmBackend.Application.Handlers.RoleHandlers
{
    public class CreateRoleCommandHandler
    {
        private readonly IRoleRepository _repository;

        public CreateRoleCommandHandler(IRoleRepository repository)
        {
            _repository = repository;
        }

        public async Task<Guid> Handle(CreateRoleCommand command)
        {
            var role = new Role { Name = command.Name };
            return await _repository.AddAsync(role);
        }
    }
}