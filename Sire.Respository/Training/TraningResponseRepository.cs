using Sire.Common.GenericRespository;
using Sire.Common.UnitOfWork;
using Sire.Data.Entities.Training;
using Sire.Domain.Context;
using Sire.Helper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sire.Respository.Training
{
    public class TraningResponseRepository : GenericRespository<TraningResponse, SireContext>, ITraningResponseRepository
    {
        private readonly IJwtTokenAccesser _jwtTokenAccesser;

        public TraningResponseRepository(IUnitOfWork<SireContext> uow,
            IJwtTokenAccesser jwtTokenAccesser)
            : base(uow, jwtTokenAccesser)
        {
            _jwtTokenAccesser = jwtTokenAccesser;
        }

    }
}

