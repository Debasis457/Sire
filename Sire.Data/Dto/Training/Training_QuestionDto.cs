using Sire.Data.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Sire.Data.Dto.Training
{
    public class Training_QuestionDto : BaseDto
    {
        public int Training_Id { get; set; }
        public int Trainee_Id { get; set; }
        public int Question_Id { get; set; }
        public bool? Completed { get; set; }

    }
}
