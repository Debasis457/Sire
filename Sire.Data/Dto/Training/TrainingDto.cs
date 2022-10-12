using Sire.Data.Entities.Common;
using Sire.Data.Entities.UserMgt;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Sire.Data.Dto.Training
{
    public class TrainingDto :BaseDto
    {
        [Required(ErrorMessage = "Operator Name Is Required")]
        public int Operator_id { get; set; }
        [Required(ErrorMessage = "Vessel Name Is Required")]
        public int Vessel_Id { get; set; }
        [Required(ErrorMessage = "Training Number Is Required")]
        public int Training_number { get; set; }
        public string Description { get; set; }
        public DateTime? Started_at { get; set; }
        public DateTime? Completed_at { get; set; }
        public int? Total { get; set; }
        public int? ResTotal { get; set; }
        public int? Difference { get; set; }
    
    }
}
