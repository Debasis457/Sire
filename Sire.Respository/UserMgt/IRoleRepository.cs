using Sire.Common.GenericRespository;
using Sire.Data.Dto.Master;
using Sire.Data.Entities.UserMgt;
using System.Collections.Generic;

namespace Sire.Respository.UserMgt
{
    public interface IRoleRepository : IGenericRepository<Role>
    {
        List<DropDownDto> GetRoleDropDown();
        string Duplicate(Role Operator);
    }
}
