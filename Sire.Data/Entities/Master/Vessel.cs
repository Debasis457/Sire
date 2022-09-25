using Sire.Data.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Sire.Data.Entities.Master
{
    public class Vessel : BaseEntity
    {
        [ForeignKey("Operator")]
        public int Operator_id { get; set; }

        [ForeignKey("Fleet")]
        public int Fleet_id { get; set; }
        public string  Name{ get; set; }
        public Vessel_Types Vessel_Type { get; set; }
        public Sire.Data.Entities.Operator.Operator Operator { get; set; }
        public Fleet Fleet { get; set; }
        public string IMO{ get; set; }
        public string Flag { get; set; }


    }

  public  enum Vessel_Types
    {
        Oil, LPG,LNG,Chemical
  
    }

}
