using CrmBackend.Domain.Services;

namespace CrmBackend.Application.Handlers.BranchHandlers
{
    public class GetAllBranchesHandler
    {
        private readonly IBranchRepository _repository;

        public GetAllBranchesHandler(IBranchRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<object>> Handle()
        {
            var branches = await _repository.GetAllAsync();
            return branches.Select(b => new { b.Id, b.Name });
        }
    }
}