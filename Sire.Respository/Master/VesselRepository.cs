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
    public class VesselRepository : GenericRespository<Vessel, SireContext>, IVesselRepository
    {
        private readonly IJwtTokenAccesser _jwtTokenAccesser;

        public VesselRepository(IUnitOfWork<SireContext> uow,
            IJwtTokenAccesser jwtTokenAccesser)
            : base(uow, jwtTokenAccesser)
        {
            _jwtTokenAccesser = jwtTokenAccesser;
        }

        public List<DropDownDto> GetVesselDropDown()
        {
            return All.Where(x => x.DeletedDate == null)
                .Select(c => new DropDownDto { Id = c.Id, Value = c.Name }).OrderBy(o => o.Value).ToList();
        }

        public string Duplicate(Vessel  vessel)
        {
            if (All.Any(x => x.Id != vessel.Id && x.Name == vessel.Name && x.DeletedDate == null))
                return "Duplicate Vessel  name : " + vessel.Name;
            return "";
        }
    }
}
