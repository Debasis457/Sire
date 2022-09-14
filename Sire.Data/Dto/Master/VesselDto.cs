using Sire.Data.Entities.Common;
using Sire.Data.Entities.Master;

namespace Sire.Data.Dto.Master
{
    public class VesselDto : BaseDto
    {
        public int Operator_id { get; set; }
        public int Fleet_id { get; set; }
        public string Name { get; set; }
        public VesselTypes Vessel_Type { get; set; }
        public Sire.Data.Entities.Operator.Operator Operator { get; set; }
        public Fleet Fleet { get; set; }
        
        
        public string IMO{ get; set; }
    }

    public enum VesselTypes
    {
        Oil=1, LPG=2, LNG=3, Chemical=4

    }
}
