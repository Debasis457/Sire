using Sire.Common.GenericRespository;
using Sire.Data.Dto.Master;
using Sire.Data.Entities.UserMgt;
using System.Collections.Generic;
using System.Security.Claims;

namespace Sire.Respository.UserMgt
{
    public interface IUserRepository : IGenericRepository<User>
    {
        List<DropDownDto> GetUserDropDown();
        string Duplicate(User Operator);
        string GenerateAccessToken(IEnumerable<Claim> claims);
        string GenerateRefreshToken();
        //void UpdateRefreshToken(int userid, string refreshToken);

    }
}
