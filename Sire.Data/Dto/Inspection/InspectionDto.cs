using Sire.Data.Entities.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sire.Data.Dto.Inspection
{
    public class InspectionDto : BaseDto
    {

        public int Operator_Id { get; set; }
        public int Vessel_Id { get; set; }
        public int Description { get; set; }
        public InspectionType? InspectionType { get; set; }
        public DateTime Started_At { get; set; }
        public DateTime Completed_At { get; set; }
    }

    public enum InspectionType { Standard = 0, Full = 1 };
}
