using Sire.Common.GenericRespository;
using Sire.Data.Dto.Master;
using Sire.Data.Entities.ShipManagement;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sire.Respository.ShipManagement
{
    public interface IPIQ_HVPQReponseRepository : IGenericRepository<PIQ_HVPQ_Response>
    {
        List<DropDownDto> GetPIQHVPQResponseDropDown();
        string Duplicate(PIQ_HVPQ_Response Piq_Hvpq_Response);
    }

 
}
