using Sire.Data.Entities.Common;
using Sire.Data.Entities.Master;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Sire.Data.Dto.Master
{
    public class LicenseDto : BaseDto
    {
        [Required(ErrorMessage = "Please Select Vessel")]
        [ForeignKey("Vessel")]
        public int Vessel_Id { get; set; }
       
        public Vessel Vessel { get; set; }
        [Required(ErrorMessage = "Please Enter Description")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Please Select Date")]
        public DateTime Created_At { get; set; }
        [Required(ErrorMessage = "Please Select Date")]
        public DateTime Expires_At { get; set; }
        
    }
}
