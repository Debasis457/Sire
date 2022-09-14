using Sire.Common.GenericRespository;
using Sire.Common.UnitOfWork;
using Sire.Data.Dto.Master;
using Sire.Domain.Context;
using Sire.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Sire.Respository.Master
{
    public class LicenseRepository : GenericRespository<Data.Entities.Master.License, SireContext>, ILicenseRepository
    {
        private readonly IJwtTokenAccesser _jwtTokenAccesser;

        public LicenseRepository(IUnitOfWork<SireContext> uow,
            IJwtTokenAccesser jwtTokenAccesser)
            : base(uow, jwtTokenAccesser)
        {
            _jwtTokenAccesser = jwtTokenAccesser;
        }

       
    }
}
