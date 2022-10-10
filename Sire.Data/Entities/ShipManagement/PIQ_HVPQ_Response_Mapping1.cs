using Sire.Data.Entities.Common;

namespace Sire.Data.Entities.ShipManagement
{
    public class PIQ_HVPQ_Response_Mapping1 : BaseEntity
    {
        public string PIQHVPQCode { get; set; }

        public int QuestionId { get; set; }

        public int VesselId { get; set; }
    }
}
