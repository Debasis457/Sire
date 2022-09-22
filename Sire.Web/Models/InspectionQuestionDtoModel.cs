using Sire.Data.Dto.Inspection;
using Sire.Data.Dto.Question;

namespace Sire.Web.Models
{
    public class InspectionQuestionDtoModel : QuestionDto
    {
        public int InspectionQuestionId { get; set; }

        public bool AssesmentCompleted { get; set; }

        public bool ReviewCompleted { get; set; }
    }
}
