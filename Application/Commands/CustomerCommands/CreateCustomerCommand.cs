using System.Text.Json.Serialization;

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
    public string? CustomerNotes { get; set; }
    public DateTime? CustomerNextMeetingDate { get; set; }
    public bool? IsVisitedShowroom { get; set; }
    public int? CustomerTimeSpent { get; set; }
    public int WayOfContact { get; set; }
    public int ContactStatus { get; set; }
    public Guid? CustomerAssignedTo { get; set; }

    [JsonIgnore] // تجاهل القيمة من JSON
    public Guid CreatedBy { get; set; } // هاد بنعبيه من التوكن
    public string? InitialComment { get; set; }
}
