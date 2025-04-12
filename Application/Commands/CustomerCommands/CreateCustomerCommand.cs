namespace CrmBackend.Application.Commands.CustomerCommands
{
    public class CreateCustomerCommand
    {
        public string CustomerName { get; set; }
        public string? CustomerEmail { get; set; }
        public string? CustomerContact { get; set; }
        public string? CustomerWhatsapp { get; set; }
        public string? CustomerAddress { get; set; }
        public string? CustomerCity { get; set; }
        public string? CustomerCountry { get; set; }
        public string? CustomerNationality { get; set; }
        public string? CustomerNationalId { get; set; }
        public string? CustomerNotes { get; set; }
        public string? CustomerNextMeetingDate { get; set; }
        public int ContactStatus { get; set; }
        public bool? IsVisitedShowroom { get; set; }
        public int? CustomerTimeSpent { get; set; }
        public int WayOfContact { get; set; }
        public Guid? CustomerAssignedTo { get; set; }
        public Guid CreatedBy { get; set; } // نجيبه من التوكن لاحقًا

        // إضافات جديدة
        public string? InitialComment { get; set; }
    }


}