using System;
using System.Collections.Generic;
using System.Text;

namespace Sire.Data.Entities.Common
{
    public class GridAuditBase
    {
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public string CreatedByUser { get; set; }
        public string DeletedByUser { get; set; }
        public string ModifiedByUser { get; set; }
    }
}
