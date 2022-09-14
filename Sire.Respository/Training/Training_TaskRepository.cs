using Sire.Common.GenericRespository;
using Sire.Common.UnitOfWork;
using Sire.Data.Dto.Master;
using Sire.Data.Entities.Question;
using Sire.Data.Entities.Training;
using Sire.Domain.Context;
using Sire.Helper;
using Sire.Respository.Question;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sire.Respository.Training
{
    public class Training_TaskRepository : GenericRespository<Training_Task, SireContext>, ITraining_TaskRepository
    {
        private readonly IJwtTokenAccesser _jwtTokenAccesser;

        public Training_TaskRepository(IUnitOfWork<SireContext> uow,
            IJwtTokenAccesser jwtTokenAccesser)
            : base(uow, jwtTokenAccesser)
        {
            _jwtTokenAccesser = jwtTokenAccesser;
        }
        public string Duplicate(Training_Task Training_Task)
        {
            if (All.Any(x => x.Id != Training_Task.Id && x.Assessor == Training_Task.Assessor && x.DeletedDate == null))
                return "Duplicate Training no : " + Training_Task.Assessor;
            return "";
        }

        public List<DropDownDto> GetTraining_taskDropDown()
        {
            return All.Where(x => x.DeletedDate == null)
               .Select(c => new DropDownDto { Id = c.Id, Value = c.Assessor}).OrderBy(o => o.Value).ToList();

        }
    }
}
