using Sire.Common.GenericRespository;
using Sire.Common.UnitOfWork;
using Sire.Data.Dto.Master;
using Sire.Data.Entities.Master;
using Sire.Domain.Context;
using Sire.Helper;
using System.Collections.Generic;
using System.Linq;

namespace Sire.Respository.Master
{
    public class User_VesselRepository : GenericRespository<User_Vessel, SireContext>, IUser_VesselRepository
    {
        private readonly IJwtTokenAccesser _jwtTokenAccesser;

        public User_VesselRepository(IUnitOfWork<SireContext> uow,
            IJwtTokenAccesser jwtTokenAccesser)
            : base(uow, jwtTokenAccesser)
        {
            _jwtTokenAccesser = jwtTokenAccesser;
        }

        public List<DropDownDto> GetUser_VesselDropDown()
        {
            return All.Where(x => x.DeletedDate == null)
                .Select(c => new DropDownDto { Id = c.Id, Value = c.Vessel_Id.ToString() }).OrderBy(o => o.Value).ToList();
        }

        public string Duplicate(User_Vessel user_Vessel)
        {
            if (All.Any(x => x.Id != user_Vessel.Id && x.User_Id == user_Vessel.User_Id && x.DeletedDate == null))
                return "Duplicate user_Vessel";
            return "";
        }
    }
}

     