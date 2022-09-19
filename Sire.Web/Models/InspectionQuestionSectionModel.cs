using System.Collections.Generic;
using Sire.Data.Dto.Inspection;
using Sire.Data.Dto.Master;

namespace Sire.Web.Models
{
    public class InspectionQuestionSectionModel
    {
        public InspectionDto InspectionDto { get; set; }

        public IList<QuetionSectionDto> QuetionSectionDtos { get; set; }
    }
}
