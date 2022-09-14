using Sire.Common.GenericRespository;
using Sire.Common.UnitOfWork;
using Sire.Data.Dto.Inspection;
using Sire.Data.Entities.Inspection;
using Sire.Domain.Context;
using Sire.Helper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sire.Respository.Inspection
{
    public class Inspection_QuestionRepository : GenericRespository<Inspection_Question, SireContext>, IInspection_QuestionRepository
    {
        private readonly IJwtTokenAccesser _jwtTokenAccesser;

        public Inspection_QuestionRepository(IUnitOfWork<SireContext> uow,
            IJwtTokenAccesser jwtTokenAccesser)
            : base(uow, jwtTokenAccesser)
        {
            _jwtTokenAccesser = jwtTokenAccesser;
        }
    }
}
