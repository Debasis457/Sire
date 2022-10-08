using Sire.Data.Dto.ShipManagement;
using Sire.Data.Entities.Common;
using Sire.Data.Entities.Question;
using System;
using System.Collections.Generic;

namespace Sire.Data.Entities.ShipManagement
{
    public class Piq_Hvpq : BaseEntity
    {

        //public Nullable<int>  question_id { get; set; }
        public string PIQHVPQCode { get; set; }
        public string piq_hvpq_question { get; set; }
        public string Operand { get; set; }
        public string Type { get; set; }
        public string Response { get; set; }
        public int?  QuestionId { get; set; }
        public virtual Sire.Data.Entities.Question.Question Question { get; set; }
        public virtual ResponseTypePIQ ResponseType { get; set; }

        public virtual List<PIQ_HVPQ_Response> PIQ_HVPQ_Response { get; set; }
    }
    //public enum ResponseTypePIQ
    //{
    //    Dropdown, Switch, Textbox
    //}
}
