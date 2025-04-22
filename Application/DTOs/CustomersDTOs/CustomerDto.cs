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
        public DateTime? CustomerNextMeetingDate { get; set; }

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

        public string? CustomerNotes { get; set; }
        public Guid? UserId { get; set; }
        public string? ManagedByName { get; set; }
        public Guid? CustomerAssignedBy { get; set; }
        public string? CustomerAssignedByName { get; set; }
        public DateTime? CustomerAssignedDate { get; set; }
        public bool? IsEscalationRequested { get; set; }
        public Guid? EscalationRequestedBy { get; set; }
        public string? EscalationRequestedByName { get; set; }
        public string? EscalationRequestedOn { get; set; }
        public Guid? EscalatedBy { get; set; }
        public string? EscalatedByUserName { get; set; }
        public string? EscalatedOn { get; set; }
        public DateTime? CreatedDate {  get; set; }

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
                BranchName = customer.Branch?.Name,
                CustomerNotes = customer.CustomerNotes,
                UserId = customer.UserId,
                ManagedByName = customer.User?.FullName,
                CustomerAssignedBy = customer.CustomerAssignedBy,
                CustomerAssignedByName = customer.CustomerAssignedByUser?.FullName,
                CustomerAssignedDate = customer.CustomerAssignedDate,
                IsEscalationRequested = customer.IsEscalationRequested,
                EscalationRequestedBy = customer.EscalationRequestedBy,
                EscalationRequestedByName = customer.EscalationRequestedByUser?.FullName,
                EscalationRequestedOn = customer.EscalationRequestedOn,
                EscalatedBy = customer.EscalatedBy,
                EscalatedByUserName = customer.EscalatedByUser?.FullName,
                EscalatedOn = customer.EscalatedOn,
                CreatedDate = customer.CreatedDate
            };
        }
    }
}
