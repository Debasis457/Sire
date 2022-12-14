using Sire.Common.GenericRespository;
using Sire.Common.UnitOfWork;
using Sire.Data.Entities.Master;
using Sire.Domain.Context;
using Sire.Helper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sire.Respository.Master
{
    public class QuetionSubSectionRepository : GenericRespository<QuetionSubSection, SireContext>, IQuetionSubSectionRepository
    {
        private readonly IJwtTokenAccesser _jwtTokenAccesser;
        public QuetionSubSectionRepository(IUnitOfWork<SireContext> uow,
            IJwtTokenAccesser jwtTokenAccesser)
            : base(uow, jwtTokenAccesser)
        {
            _jwtTokenAccesser = jwtTokenAccesser;
        }
    }
}
