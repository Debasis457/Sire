using Sire.Data.Entities.Common;
using Sire.Data.Entities.UserMgt;
using Sire.Data.Entities.Question;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Sire.Data.Entities.Inspection
{
    public class Inspection_Question :BaseEntity
    {
        public int? inspection_id { get; set; }

        [ForeignKey("Question")]
        public int question_id { get; set; }
        public int? assessor_id { get; set; }
        public User User { get; set; }
        public int? reviewer_id { get; set; }
        public bool? comment_by_reviewer { get; set; }
        public bool? assesment_completed { get; set; }
        public bool? review_completed { get; set; }

        
    }
}
