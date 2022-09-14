using Sire.Common.GenericRespository;
using Sire.Common.UnitOfWork;
using Sire.Domain.Context;
using Sire.Helper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sire.Respository.Inspection
{
    public class InspectionRepository : GenericRespository<Sire.Data.Entities.Inspection.Inspection, SireContext>, IInspectionRepository
    {
        private readonly IJwtTokenAccesser _jwtTokenAccesser;

        public InspectionRepository(IUnitOfWork<SireContext> uow,
            IJwtTokenAccesser jwtTokenAccesser)
            : base(uow, jwtTokenAccesser)
        {
            _jwtTokenAccesser = jwtTokenAccesser;
        }
    }
}
