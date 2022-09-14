using Sire.Data.Entities.Common;
using Sire.Data.Entities.Question;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sire.Data.Dto.Question
{
    public class QuestionResponseDto : BaseDto
    {
        public ResponseTypes Response_Type { get; set; }
        public Response_Type_Category Response_Type_Cateogary { get; set; }
        public string Value { get; set; }
    }


   
}
