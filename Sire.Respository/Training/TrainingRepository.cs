using Sire.Common.GenericRespository;
using Sire.Common.UnitOfWork;
using Sire.Data.Dto.Master;
using Sire.Data.Entities.Training;
using Sire.Domain.Context;
using Sire.Helper;
using Sire.Respository.ShipManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sire.Respository.Training
{
    public class TrainingRepository : GenericRespository<Data.Entities.Training.Training, SireContext>, ITrainingRepository
    {
        private readonly IJwtTokenAccesser _jwtTokenAccesser;

        public TrainingRepository(IUnitOfWork<SireContext> uow,
            IJwtTokenAccesser jwtTokenAccesser)
            : base(uow, jwtTokenAccesser)
        {
            _jwtTokenAccesser = jwtTokenAccesser;
        }

        public string Duplicate(Sire.Data.Entities.Training.Training Training)
        {
            if (All.Any(x => x.Id != Training.Id && x.Training_number == Training.Training_number && x.DeletedDate == null))
                return "Duplicate Training no : " + Training.Training_number;
            return "";
        }

        public List<DropDownDto> GetTrainingDropDown()
        {
            return All.Where(x => x.DeletedDate == null)
               .Select(c => new DropDownDto { Id = c.Id, Value = Convert.ToString(c.Training_number)}).OrderBy(o => o.Value).ToList();

        }
    }
}
