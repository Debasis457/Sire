using Sire.Data.Dto.Question;
using Sire.Data.Entities.Common;
using Sire.Data.Entities.Inspection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sire.Data.Entities.Question
{
    public class Question :BaseEntity

    {
        public int Chapter { get; set; }
        public int Section { get; set; }
        public int Question_Number { get; set; }
        public string Questions { get; set; }
        public string Short_Text { get; set; }
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

        public int Rank { get; set; }
        public int Rank_Group_Id { get; set; }

      
    }
}
