using System.Collections.Generic;

namespace Sire.Data.Dto.Inspection
{
    public class OngoingInspectionDto
    {
        public InspectionDto Inspection { get; set; }

        public IEnumerable<Inspection_QuestionDto> InspectionQuestions { get; set; }
    }
}
