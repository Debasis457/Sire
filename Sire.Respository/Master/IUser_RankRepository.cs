using Sire.Common.GenericRespository;
using Sire.Data.Dto.Master;
using Sire.Data.Entities.Master;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sire.Respository.Master
{
    public interface IUser_RankRepository : IGenericRepository<User_Rank>
    {
        List<DropDownDto> GetUser_RankDropDown();
        string Duplicate(User_Rank user_rank);
    }
}
