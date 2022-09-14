using Sire.Common.GenericRespository;
using Sire.Data.Dto.Master;
using Sire.Data.Entities.Training;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sire.Respository.Question
{
    public interface IQuestionRepository : IGenericRepository<Sire.Data.Entities.Question.Question>

    {
        public List<DropDownDto> GetQuesstionList();
        string Duplicate(Sire.Data.Entities.Question.Question Question);
    }
}
