using CrmBackend.Domain.Common;
using CrmBackend.Domain.Enums;

namespace CrmBackend.Domain.Entities
{
    public class Building : AuditableEntity
    {
        public int BuildingId { get; set; }
        public string BuildingAddress { get; set; }
        public BuildingTypeOfUnit BuildingTypeOfUnit { get; set; }
        public BuildingCondition BuildingCondition { get; set; }
        public string BuildingFloor { get; set; }
        public bool? BuildingReconstruction { get; set; }
        public bool? IsOccupied { get; set; }
        public string BuildingMakaniMap { get; set; }
        public string BuildingLatitude { get; set; }
        public string BuildingLongitude { get; set; }

        // الربط مع الاستفسارات
        public virtual ICollection<Inquiry> Inquiries { get; set; } = new List<Inquiry>();
    }
}
