using Sire.Data.Entities.Common;
using Sire.Data.Entities.ShipManagement;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sire.Data.Dto.ShipManagement
{
    public class Vessel_Response_Piq_HvpqDto
    {
        public int? Vessel_Id { get; set; }
        public string Piq_Hvpq_Id { get; set; }
        public string Response { get; set; }
    }

    public class Vessel_Response_Piq_HvpqDto1
    {
        public int? vessel_Id { get; set; }
        public string piq_Hvpq_Id { get; set; }
        public string response { get; set; }
    }

    public class Vessel_Response_Piq_HvpqQuetionsDto
    {
        public int? Vessel_Id { get; set; }
        public string Piq_Hvpq_Id { get; set; }
        public string Response { get; set; }
        public int? QuetionsId { get; set; }
    }

    public class QuestionListFilter
    {
        public int? QuetionsId { get; set; }
    }
}
