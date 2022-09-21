using System.Collections.Generic;
using Sire.Data.Dto.Inspection;
using Sire.Data.Dto.Question;

namespace Sire.Web.Models
{
    public class InspectionQuestionResponseModel
    {
        public QuestionDto questionDto { get; set; }

        public Inspection_QuestionDto inspectionQuestionDto { get; set; }

        public IList<QuestionResponseDto> questionResponseDtos { get; set; }

        public IList<InspectionResponseDto> inspectionResponseDtos { get; set; }
    }
}
