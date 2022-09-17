using System.Collections.Generic;
using Sire.Data.Dto.Inspection;
using Sire.Data.Dto.Question;

namespace Sire.Web.Models
{
    public class QuestionResponseModel
    {
        public QuestionDto questionDto { get; set; }

        public IList<QuestionResponseDto> questionResponseDtos { get; set; }

        public IList<InspectionResponseDto> inspectionResponseDtos { get; set; }
    }
}
