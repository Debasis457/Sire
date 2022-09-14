using System.Collections.Generic;
using System.Linq;
using Sire.Common.GenericRespository;
using Sire.Common.UnitOfWork;
using Sire.Data.Dto.Master;
using Sire.Data.Entities.Master;
using Sire.Domain.Context;
using Sire.Helper;

namespace Sire.Respository.Master
{
    public class FleetRepository : GenericRespository<Fleet, SireContext>, IFleetRepository
    {
        private readonly IJwtTokenAccesser _jwtTokenAccesser;

        public FleetRepository(IUnitOfWork<SireContext> uow,
            IJwtTokenAccesser jwtTokenAccesser)
            : base(uow, jwtTokenAccesser)
        {
            _jwtTokenAccesser = jwtTokenAccesser;
        }

        public List<DropDownDto> GetFleetDropDown()
        {
            return All.Where(x => x.DeletedDate == null)
                .Select(c => new DropDownDto { Id = c.Id, Value = c.Name}).OrderBy(o => o.Value).ToList();
        }

        public string Duplicate(Fleet fleet)
        {
            if (All.Any(x => x.Id != fleet.Id && x.Name == fleet.Name && x.DeletedDate == null))
                return "Duplicate fleet name : " + fleet.Name;
            return "";
        }
    }
}
