using Sire.Data.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Sire.Data.Entities.ShipManagement
{
    public class PIQ_HVPQ_Response : BaseEntity
    {

        [ForeignKey("Piq_Hvpq")]
        public int piq_hvpq_id { get; set; }
        public string value { get; set; }
        public Piq_Hvpq Piq_Hvpq { get; set; }

    }
}
