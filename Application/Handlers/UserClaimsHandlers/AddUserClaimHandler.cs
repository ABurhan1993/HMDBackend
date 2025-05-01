using CrmBackend.Application.Commands.UserClaimCommands;
using CrmBackend.Domain.Entities;
using CrmBackend.Domain.Services;

namespace CrmBackend.Application.Handlers.UserClaimHandlers
{
    public class AddUserClaimHandler
    {
        private readonly IUserClaimRepository _repo;

        public AddUserClaimHandler(IUserClaimRepository repo)
        {
            _repo = repo;
        }

        public async Task HandleAsync(AddUserClaimCommand command)
        {
            var existingClaims = await _repo.GetClaimsByUserIdAsync(command.UserId);
            var alreadyExists = existingClaims.Any(c =>
                c.ClaimType == command.ClaimType && c.ClaimValue == command.ClaimValue);

            if (alreadyExists)
                throw new InvalidOperationException("Claim already exists for this user.");

            var claim = new UserClaim
            {
                UserId = command.UserId,
                ClaimType = command.ClaimType,
                ClaimValue = command.ClaimValue
            };

            await _repo.AddClaimAsync(claim);
        }
    }

    public class DeleteUserClaimHandler
    {
        private readonly IUserClaimRepository _repo;

        public DeleteUserClaimHandler(IUserClaimRepository repo)
        {
            _repo = repo;
        }

        public async Task HandleAsync(DeleteUserClaimCommand command)
        {
            await _repo.DeleteClaimAsync(command.Id);
            await _repo.SaveAsync();
        }
    }
}