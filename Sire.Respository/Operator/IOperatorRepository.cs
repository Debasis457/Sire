using Sire.Common.GenericRespository;
using Sire.Data.Dto.Master;
using System.Collections.Generic;

namespace Sire.Respository.Operator
{
    public interface IOperatorRepository : IGenericRepository<Sire.Data.Entities.Operator.Operator>
    {
        List<DropDownDto> GetOperatorDropDown();
        string Duplicate(Sire.Data.Entities.Operator.Operator Operator);
    }
}
