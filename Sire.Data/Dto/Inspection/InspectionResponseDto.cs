using Sire.Data.Dto.Question;
using Sire.Data.Entities.Common;
using Sire.Data.Entities.Question;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sire.Data.Dto.Inspection
{
    public class InspectionResponseDto : BaseDto
    {
        public int Inspection_Question_id { get; set; }
        public ResponseTypes ResponseType { get; set; }
        public string Response_Value { get; set; }
        public string Response_Comment { get; set; }
        public bool Is_Answerable { get; set; }
        public string media_link_1 { get; set; }
        public string media_link_2 { get; set; }
        public string media_link_3 { get; set; }


    }
     
}
