using Sire.Common.GenericRespository;
using Sire.Data.Dto.Master;
using Sire.Data.Entities.Master;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sire.Respository.Master
{
    public interface IUser_VesselRepository : IGenericRepository<User_Vessel>
    {
        List<DropDownDto> GetUser_VesselDropDown();
        string Duplicate(User_Vessel vessel);

    }
}