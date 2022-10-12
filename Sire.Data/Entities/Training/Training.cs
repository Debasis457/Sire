using Sire.Data.Entities.Common;
using Sire.Data.Entities.UserMgt;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Sire.Data.Entities.Training
{
    public class Training : BaseEntity
    {
        [ForeignKey("Operator")]
        public int Operator_id { get; set; }
        [ForeignKey("Vessel")]
        public int Vessel_Id { get; set; }
        public int Training_number { get; set; }
        public string Description { get; set; }
        public DateTime? Started_at { get; set; }
        public DateTime? Completed_at { get; set; }
      
    }
}
