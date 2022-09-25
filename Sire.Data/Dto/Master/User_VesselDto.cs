using Sire.Data.Entities.Common;
using Sire.Data.Entities.Master;
using Sire.Data.Entities.UserMgt;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sire.Data.Dto.Master
{
    public class User_VesselDto : BaseDto
    {
        [Required(ErrorMessage = "Please Select User")]
        public int User_Id { get; set; }

        [Required(ErrorMessage = "Please Select Vessel")]
        public int Vessel_Id { get; set; }
       
        public bool? is_own_vessel { get; set; }
        public User User { get; set; }
        public Vessel Vessel{ get; set; }
        public Fleet Fleet { get; set; }
    }
}
