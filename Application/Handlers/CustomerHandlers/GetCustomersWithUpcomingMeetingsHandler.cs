using CrmBackend.Application.CustomerCommands;
using CrmBackend.Domain.Enums;
using CrmBackend.Domain.Services;
using Microsoft.EntityFrameworkCore;

namespace CrmBackend.Application.CustomerHandlers
{
    public class GetCustomersWithUpcomingMeetingsHandler
    {
        private readonly ICustomerRepository _repository;

        public GetCustomersWithUpcomingMeetingsHandler(ICustomerRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<GetCustomersWithUpcomingMeetingsCommand>> HandleAsync(int branchId)
        {
            var customers = await _repository
                .Query()
                .Include(c => c.CustomerAssignedToUser)
                .Where(c => c.CustomerNextMeetingDate != null && !c.IsDeleted && c.BranchId == branchId)
                .ToListAsync();

            return customers.Select(c =>
            {
                var (emoji, bgColor, textColor) = c.ContactStatus switch
                {
                    ContactStatus.NeedToContact => ("📞 Need to Contact", "#fde2e4", "#b91c1c"),
                    ContactStatus.NeedToFollowUp => ("🟡 Need to Follow Up", "#fffacc", "#92400e"),
                    ContactStatus.NotResponding => ("❌ Not Responding", "#e0e7ff", "#1e3a8a"),
                    _ => ("✅ Contacted", "#c7f0db", "#065f46")
                };

                return new GetCustomersWithUpcomingMeetingsCommand
                {
                    Title = $"{emoji} – {c.CustomerName} ({c.CustomerAssignedToUser?.FullName})",
                    Date = c.CustomerNextMeetingDate?.ToString("yyyy-MM-dd"),
                    BackgroundColor = bgColor,
                    TextColor = textColor,
                    CustomerName = c.CustomerName
                };
            }).ToList();
        }
    }
}
