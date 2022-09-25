using Sire.Data.Entities.Common;
using Sire.Data.Entities.Master;
using System.ComponentModel.DataAnnotations;

namespace Sire.Data.Dto.Master
{
    public class VesselDto : BaseDto
    {
        [Required(ErrorMessage = "Please Select Operator")]
        public int Operator_id { get; set; }

        [Required(ErrorMessage = "Please Select Fleet")]
        public int Fleet_id { get; set; }

        [Required(ErrorMessage = "Please  Enter Vessel Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please Vessel Enter Type")]
        public VesselTypes Vessel_Type { get; set; }
        public Sire.Data.Entities.Operator.Operator Operator { get; set; }
        public Fleet Fleet { get; set; }

        [Required(ErrorMessage = "Please Enter IMO")]
        public string IMO{ get; set; }
        public string Flag { get; set; }

    }

    public enum VesselTypes
    {
        Oil=1, LPG=2, LNG=3, Chemical=4

    }
}
