using Sire.Data.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Sire.Data.Entities.ShipManagement
{
    public class PIQ_HVPQ_Response : BaseEntity
    {
        public string piq_hvpq_id { get; set; }
        public string value { get; set; }

    }
}
