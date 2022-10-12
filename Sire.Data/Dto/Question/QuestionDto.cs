using Sire.Data.Entities.Common;
using Sire.Data.Entities.Question;
using Sire.Data.Entities.UserMgt;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sire.Data.Dto.Question
{
    public class QuestionDto :BaseDto
    {
        public int Chapter { get; set; }
        public int Section { get; set; }
        public int Question_Number { get; set; }
        public string Questions { get; set; }
        public string Short_Question { get; set; }
        public string Question_Type { get; set; }
        public bool? Chemical { get; set; }
        public bool? LNG { get; set; }
        public bool? LPG { get; set; }
        public bool? OIL { get; set; }
        public bool? Conditional { get; set; }
        public int Hardware_Response_Type { get; set; }
        public int Human_Response_Type { get; set; }
        public int Process_Response_Type { get; set; }
        public string Objective { get; set; }
        public string Industry_Guidance { get; set; }
        public string Inspection_Guidance { get; set; }
        public string Suggested_Inspection_actions { get; set; }
        public string Potential_for_Negative { get; set; }
        public string Checklist { get; set; }
        public string Expected_Evidence { get; set; }
        public string Section_Name { get; set; }
    
        public int Rank { get; set; }
        public int Rank_Group_Id { get; set; }
        public int DAssessore { get; set; }
        public User User { get; set; }
        public int DReviewer { get; set; }
        public int? Total { get; set; }
        public int? ResTotal { get; set; }

    }
}
