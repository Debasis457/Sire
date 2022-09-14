using Sire.Common.GenericRespository;
using Sire.Common.UnitOfWork;
using Sire.Data.Dto.Master;
using Sire.Data.Entities.ShipManagement;
using Sire.Domain.Context;
using Sire.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sire.Respository.ShipManagement
{
    public class PIQ_HVPQResponseRepository : GenericRespository<PIQ_HVPQ_Response, SireContext>, IPIQ_HVPQReponseRepository
    {

        private readonly IJwtTokenAccesser _jwtTokenAccesser;

        public PIQ_HVPQResponseRepository(IUnitOfWork<SireContext> uow,
            IJwtTokenAccesser jwtTokenAccesser)
            : base(uow, jwtTokenAccesser)
        {
            _jwtTokenAccesser = jwtTokenAccesser;
        }

        public string Duplicate(PIQ_HVPQ_Response Piq_Hvpq_Response)
        {

            if (All.Any(x => x.Id != Piq_Hvpq_Response.Id && x.value == Piq_Hvpq_Response.value && x.DeletedDate == null))
                return "Duplicate fleet name : " + Piq_Hvpq_Response.value;
            return "";
        }

        public List<DropDownDto> GetPIQHVPQResponseDropDown()
        {
            return All.Where(x => x.DeletedDate == null)
               .Select(c => new DropDownDto { Id = c.Id, Value = c.value }).OrderBy(o => o.Value).ToList();
        }


    }
}
