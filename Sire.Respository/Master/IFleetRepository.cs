using Sire.Common.GenericRespository;
using Sire.Data.Dto.Master;
using Sire.Data.Entities.Master;
using System.Collections.Generic;

namespace Sire.Respository.Master
{
    public interface IFleetRepository : IGenericRepository<Fleet>
    {
        List<DropDownDto> GetFleetDropDown();
        string Duplicate(Fleet fleet);

    }
}
