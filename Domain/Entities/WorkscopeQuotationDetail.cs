using CrmBackend.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrmBackend.Domain.Entities
{
    public class WorkscopeQuotationDetail : AuditableEntity
    {
        public int WorkscopeQuotationDetailId { get; set; }

        public int InquiryWorkscopeId { get; set; }
        [ForeignKey("InquiryWorkscopeId")]
        public InquiryWorkscope InquiryWorkscope { get; set; }

        // Quotation metadata
        public DateTime? QuotationAddedDate { get; set; }
        public Guid? QuotationAddedBy { get; set; }

        public DateTime? QuotationUpdatedDate { get; set; }
        public Guid? QuotationUpdatedBy { get; set; }

        public string WorkscopeQuotationPic { get; set; }

        public bool? IsUrlGenerated { get; set; }
        public DateTime? UrlGeneratedDate { get; set; }

        // Amounts
        public string WoodenWorkAmount { get; set; }
        public string CounterTopAmount { get; set; }
        public string AppliancesAmount { get; set; }
        public string LightningAmount { get; set; }
        public string AccessoriesAmount { get; set; }
        public string SpecialItemAmount { get; set; }

        public string Amount { get; set; }      // Subtotal
        public string TotalAmount { get; set; } // After discounts/tax/etc.
    }
}
