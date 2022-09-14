using Sire.Common.GenericRespository;
using Sire.Common.UnitOfWork;
using Sire.Data.Dto.Master;
using Sire.Data.Entities.Master;
using Sire.Domain.Context;
using Sire.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sire.Respository.Master
{
    public class User_RankRepository : GenericRespository<User_Rank, SireContext>, IUser_RankRepository
    {
        private readonly IJwtTokenAccesser _jwtTokenAccesser;

        public User_RankRepository(IUnitOfWork<SireContext> uow,
            IJwtTokenAccesser jwtTokenAccesser)
            : base(uow, jwtTokenAccesser)
        {
            _jwtTokenAccesser = jwtTokenAccesser;
        }

        public List<DropDownDto> GetUser_RankDropDown()
        {
            return All.Where(x => x.DeletedDate == null)
                .Select(c => new DropDownDto { Id = c.Id, Value = c.Rank }).OrderBy(o => o.Value).ToList();
        }

        public string Duplicate(User_Rank user_rank)
        {
            if (All.Any(x => x.Id != user_rank.Id && x.Rank == user_rank.Rank && x.DeletedDate == null))
                return "Duplicate Rank name : " + user_rank.Rank;
            return "";
        }
    }
}
