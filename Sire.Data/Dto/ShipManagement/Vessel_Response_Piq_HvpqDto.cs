﻿using Sire.Data.Entities.Common;
using Sire.Data.Entities.ShipManagement;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sire.Data.Dto.ShipManagement
{
    public class Vessel_Response_Piq_HvpqDto
    {
        public int? Vessel_Id { get; set; }
        public int? Piq_Hvpq_Id { get; set; }
        public string Response { get; set; }
    }

    public class Vessel_Response_Piq_HvpqDto1
    {
        public int? vessel_Id { get; set; }
        public int? piq_Hvpq_Id { get; set; }
        public string response { get; set; }
    }
}
