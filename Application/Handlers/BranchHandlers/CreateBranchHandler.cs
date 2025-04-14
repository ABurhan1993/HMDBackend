using CrmBackend.Application.Commands.BranchCommands;
using CrmBackend.Domain.Entities;
using CrmBackend.Domain.Services;

namespace CrmBackend.Application.Handlers.BranchHandlers
{
    public class CreateBranchCommandHandler
    {
        private readonly IBranchRepository _repository;

        public CreateBranchCommandHandler(IBranchRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> Handle(CreateBranchCommand command)
        {
            var branch = new Branch { Name = command.Name };
            return await _repository.AddAsync(branch);
        }
    }
}