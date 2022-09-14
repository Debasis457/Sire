using Sire.Common.GenericRespository;
using Sire.Common.UnitOfWork;
using Sire.Data.Entities.ShipManagement;
using Sire.Domain.Context;
using Sire.Helper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sire.Respository.ShipManagement
{
    public class Vessel_Response_Piq_HvpqRepository : GenericRespository<Vessel_Response_Piq_Hvpq, SireContext>, IVessel_Response_Piq_HvpqRepository
    {
        private readonly IJwtTokenAccesser _jwtTokenAccesser;

        public Vessel_Response_Piq_HvpqRepository(IUnitOfWork<SireContext> uow,
            IJwtTokenAccesser jwtTokenAccesser)
            : base(uow, jwtTokenAccesser)
        {
            _jwtTokenAccesser = jwtTokenAccesser;
        }

    }
}
