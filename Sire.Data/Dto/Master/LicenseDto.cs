using Sire.Data.Entities.Common;
using Sire.Data.Entities.Master;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Sire.Data.Dto.Master
{
    public class LicenseDto : BaseDto
    {
        [ForeignKey("Vessel")]
        public int Vessel_Id { get; set; }
       
        public Vessel Vessel { get; set; }
        public string Description { get; set; }
        public DateTime Created_At { get; set; }
        public DateTime Expires_At { get; set; }
        
    }
}
