using Sire.Common.GenericRespository;
using Sire.Common.UnitOfWork;
using Sire.Data.Dto.Master;
using Sire.Domain.Context;
using Sire.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sire.Respository.Question
{
    public class QuestionRepository : GenericRespository<Sire.Data.Entities.Question.Question, SireContext>, IQuestionRepository
 
    {
        private readonly IJwtTokenAccesser _jwtTokenAccesser;

        public QuestionRepository(IUnitOfWork<SireContext> uow,
            IJwtTokenAccesser jwtTokenAccesser)
            : base(uow, jwtTokenAccesser)
        {
            _jwtTokenAccesser = jwtTokenAccesser;
        }

        public string Duplicate(Data.Entities.Question.Question Question)
        {
            if (All.Any(x => x.Id != Question.Id && x.Questions == Question.Questions && x.DeletedDate == null))
                return "Duplicate Question  : " + Question.Questions;
            return "";
        }

        public List<DropDownDto> GetQuesstionList()
        {
            return All.Where(x =>
                    (x.DeletedDate == null))
                .Select(c => new DropDownDto { Id = c.Id, Value = c.Questions }).OrderBy(o => o.Value).ToList();
        }
    }
}
