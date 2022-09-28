using Sire.Data.Entities.Common;
using Sire.Data.Entities.UserMgt;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Sire.Data.Entities.Master
{
    public class User_Vessel : BaseEntity
    {
        [ForeignKey("User")]
        public int User_Id { get; set; }
        [ForeignKey("Vessel")]
        public int Vessel_Id { get; set; }
        public bool? is_own_vessel { get; set; }
        public User User{ get; set; }
        public Vessel Vessel { get; set; }
    }
}
