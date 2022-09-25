using Sire.Data.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Sire.Data.Dto.Question
{
    public class QuestionRoviq : BaseDto
    {
        [ForeignKey("Question")]
        public int? QuestionId { get; set; }

        public string Roviq_Type { get; set; }
    }
}
