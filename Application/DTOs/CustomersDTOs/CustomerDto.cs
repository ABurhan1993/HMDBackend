using CrmBackend.Domain.Entities;
using CrmBackend.Domain.Enums;

namespace CrmBackend.Application.DTOs.CustomersDTOs
{
    public class CustomerDto
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string? CustomerEmail { get; set; }
        public string? CustomerContact { get; set; }
        public string? CustomerWhatsapp { get; set; }
        public string? CustomerAddress { get; set; }
        public string? CustomerCity { get; set; }
        public string? CustomerCountry { get; set; }
        public string? CustomerNationality { get; set; }
        public string? CustomerNextMeetingDate { get; set; }

        public ContactStatus ContactStatus { get; set; }
        public string ContactStatusName => ContactStatus.ToString();

        public WayOfContact WayOfContact { get; set; }
        public string WayOfContactName => WayOfContact.ToString();

        public Guid? CustomerAssignedTo { get; set; }
        public string? CustomerAssignedToName { get; set; }

        public bool? IsVisitedShowroom { get; set; }
        public int? CustomerTimeSpent { get; set; }

        public int? BranchId { get; set; }
        public string? BranchName { get; set; }

        public static CustomerDto FromEntity(Customer customer)
        {
            return new CustomerDto
            {
                CustomerId = customer.CustomerId,
                CustomerName = customer.CustomerName,
                CustomerEmail = customer.CustomerEmail,
                CustomerContact = customer.CustomerContact,
                CustomerWhatsapp = customer.CustomerWhatsapp,
                CustomerAddress = customer.CustomerAddress,
                CustomerCity = customer.CustomerCity,
                CustomerCountry = customer.CustomerCountry,
                CustomerNationality = customer.CustomerNationality,
                CustomerNextMeetingDate = customer.CustomerNextMeetingDate,
                ContactStatus = customer.ContactStatus,
                WayOfContact = customer.WayOfContact,
                CustomerAssignedTo = customer.CustomerAssignedTo,
                CustomerAssignedToName = customer.CustomerAssignedToUser?.FullName,
                IsVisitedShowroom = customer.IsVisitedShowroom,
                CustomerTimeSpent = customer.CustomerTimeSpent,
                BranchId = customer.BranchId,
                BranchName = customer.Branch?.Name
            };
        }
    }
}
