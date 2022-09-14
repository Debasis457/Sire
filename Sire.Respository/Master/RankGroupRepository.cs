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
    
    public class RankGroupRepository : GenericRespository<RankGroup, SireContext>, IRankGroupRepository
    {
        private readonly IJwtTokenAccesser _jwtTokenAccesser;

        public RankGroupRepository(IUnitOfWork<SireContext> uow,
            IJwtTokenAccesser jwtTokenAccesser)
            : base(uow, jwtTokenAccesser)
        {
            _jwtTokenAccesser = jwtTokenAccesser;
        }

        public List<DropDownDto> GetRankGroupDropDown()
        {
            return All.Where(x => x.DeletedDate == null)
                .Select(c => new DropDownDto { Id = c.Id, Value = c.Rank_Group }).OrderBy(o => o.Value).ToList();
        }

        public string Duplicate(RankGroup rankGroup)
        {
            if (All.Any(x => x.Id != rankGroup.Id && x.Rank_Group == rankGroup.Rank_Group && x.DeletedDate == null))
                return "Duplicate Rank name : " + rankGroup.Rank_Group;
            return "";
        }
    }
}
