using Sire.Common.GenericRespository;
using Sire.Common.UnitOfWork;
using Sire.Data.Entities.Inspection;
using Sire.Domain.Context;
using Sire.Helper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sire.Respository.Inspection
{
  
    public class InspectionResponseRepository : GenericRespository<InspectionResponse, SireContext>, IInspectionResponseRepository
    {
        private readonly IJwtTokenAccesser _jwtTokenAccesser;

        public InspectionResponseRepository(IUnitOfWork<SireContext> uow,
            IJwtTokenAccesser jwtTokenAccesser)
            : base(uow, jwtTokenAccesser)
        {
            _jwtTokenAccesser = jwtTokenAccesser;
        }
    }
}
