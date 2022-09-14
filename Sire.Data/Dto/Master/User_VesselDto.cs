using Sire.Data.Entities.Common;
using Sire.Data.Entities.Master;
using Sire.Data.Entities.UserMgt;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sire.Data.Dto.Master
{
    public class User_VesselDto : BaseDto
    {
        public int User_Id { get; set; }
        public int Vessel_Id { get; set; }
        public bool? is_own_vessel { get; set; }
        public User User { get; set; }
        public Vessel Vessel{ get; set; }
    }
}
