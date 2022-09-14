using Sire.Common.GenericRespository;
using Sire.Data.Dto.Master;
using Sire.Data.Entities.Master;
using System.Collections.Generic;

namespace Sire.Respository.Master
{
    public interface IVesselRepository : IGenericRepository<Vessel>
    {
        List<DropDownDto> GetVesselDropDown();
        string Duplicate(Vessel vessel);

    }
}
