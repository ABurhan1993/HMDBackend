using CrmBackend.Application.Commands.BranchCommands;
using CrmBackend.Domain.Services;

namespace CrmBackend.Application.Handlers.BranchHandlers
{
    public class UpdateBranchCommandHandler
    {
        private readonly IBranchRepository _repository;

        public UpdateBranchCommandHandler(IBranchRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(UpdateBranchCommand command)
        {
            var branch = await _repository.GetByIdAsync(command.BranchId);
            if (branch == null) throw new Exception("Branch not found");

            branch.Name = command.Name;
            await _repository.UpdateAsync(branch);
        }
    }
}