using Sire.Data.Entities.Common;
using Sire.Data.Entities.ShipManagement;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sire.Data.Dto.ShipManagement
{
    public class Piq_HvpqDto : BaseDto
    {
        public string piq_hvpq_question { get; set; }
        public string Operand { get; set; }
        public string Type { get; set; }
        public string Response { get; set; }
        public int? QuestionId { get; set; }
        public string PIQHVPQCode { get; set; }
        public string Answered { get; set; }
        public ResponseTypePIQ ResponseType { get; set; }
        public List<PIQ_HVPQ_ResponseDto> PIQ_HVPQ_Response { get; set; }
    }

    public enum ResponseTypePIQ
    {
        Dropdown, Switch, Textbox
    }
    public class PIQ_HVPQWrapper : BaseDto
    {
        public List<Piq_HvpqDto> PIQ { get; set; }
        public List<Piq_HvpqDto> HVPQ { get; set; }
    }
}
