namespace CrmBackend.Application.CustomerCommands;

public class GetCustomersWithUpcomingMeetingsCommand
{
    public string Title { get; set; }         // مثال: 📞 Need to Contact – Ahmad
    public string Date { get; set; }          // التاريخ
    public string BackgroundColor { get; set; }
    public string TextColor { get; set; }

    public string CustomerName { get; set; }  // 👈 جديد: اسم الزبون
}
