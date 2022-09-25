using Sire.Data.Entities.Common;
using System.ComponentModel.DataAnnotations;

namespace Sire.Data.Dto.Operator
{
    public class OperatorDto : BaseDto
    {
        [Required(ErrorMessage = "Please Enter Operator")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please Enter Description")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Please Enter Imo Number")]
        public string Imo_Number { get; set; }
        public int Vessel_Count { get; set; }
    }
}
