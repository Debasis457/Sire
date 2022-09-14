using Sire.Common.GenericRespository;
using Sire.Common.UnitOfWork;
using Sire.Data.Dto.Master;
using Sire.Data.Entities.Training;
using Sire.Domain.Context;
using Sire.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sire.Respository.Training
{

    public class TrainingQuestionRepository : GenericRespository<Training_Question, SireContext>, ITrainingQuestionRepository
    {
        private readonly IJwtTokenAccesser _jwtTokenAccesser;

        public TrainingQuestionRepository(IUnitOfWork<SireContext> uow,
            IJwtTokenAccesser jwtTokenAccesser)
            : base(uow, jwtTokenAccesser)
        {
            _jwtTokenAccesser = jwtTokenAccesser;
        }

    }
}

