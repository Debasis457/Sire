using Sire.Data.Entities.Common;
using Sire.Data.Entities.UserMgt;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sire.Data.Dto.Master
{
    public class FleetDto : BaseDto
    {

        [Required(ErrorMessage = "Please Enter Fleet Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please Select  Fleet Head")]
        public int Fleet_Head_Id { get; set; }

        public User User { get; set; }
    }
}
