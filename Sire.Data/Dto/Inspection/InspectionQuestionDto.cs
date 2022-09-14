using Sire.Data.Entities.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sire.Data.Dto.Inspection
{
    public class Inspection_QuestionDto : BaseDto
    {
        public int Inspection_Id { get; set; }
        public int Question_Id { get; set; }
        public int Assessor_Id { get; set; }
        public int Reviewer_Id { get; set; }
        public bool Comment_By_Reviewer { get; set; }
        public bool Assesment_Completed { get; set; }
        public bool Review_Completed { get; set; }
        public string Assessor_Name { get; set; }
        public string Reviewer_Name { get; set; }
        public string Question_Text { get; set; }
        public bool? IsAssesor { get; set; }


    }
}
