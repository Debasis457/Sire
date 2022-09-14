using Sire.Data.Entities.Common;
using System.Collections.Generic;

namespace Sire.Data.Entities.ShipManagement
{
    public class Piq_Hvpq : BaseEntity
    {

        public int question_id { get; set; }
        public string piq_hvpq_question { get; set; }
        public string Operand { get; set; }
        public string Type { get; set; }
        public string Response { get; set; }
        public ResponseTypePIQ? ResponseType { get; set; }
        public virtual List<PIQ_HVPQ_Response> PIQ_HVPQ_Response { get; set; }
    }
    public enum ResponseTypePIQ
    {
        Dropdown, Switch, Textbox
    }
}
