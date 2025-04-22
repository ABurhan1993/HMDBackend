namespace CrmBackend.Application.DTOs.InquiryDTOs;

public class WorkscopeQuotationDetailDto
{
    public string WoodenWorkAmount { get; set; }
    public string CounterTopAmount { get; set; }
    public string AppliancesAmount { get; set; }
    public string LightningAmount { get; set; }
    public string AccessoriesAmount { get; set; }
    public string SpecialItemAmount { get; set; }

    public string Subtotal { get; set; }
    public string TotalAmount { get; set; }

    public string QuotationPicUrl { get; set; }
    public string AddedByName { get; set; }
    public DateTime? AddedDate { get; set; }
}
