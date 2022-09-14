using System.Collections.Generic;
using Sire.Data.Dto.Question;
using Sire.Data.Dto.Training;

namespace Sire.Web.Models
{
    public class QuestionTrainingModel
    {
        public IEnumerable<QuestionDto> QuestionDtos { get; set; }

        public IList<TraningResponseDto> TraningResponseDtos { get; set; }
    }
}
