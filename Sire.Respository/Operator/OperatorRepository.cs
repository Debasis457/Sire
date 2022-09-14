using Sire.Common.GenericRespository;
using Sire.Common.UnitOfWork;
using Sire.Data.Dto.Master;
using Sire.Domain.Context;
using Sire.Helper;
using System.Collections.Generic;
using System.Linq;

namespace Sire.Respository.Operator
{
    public class OperatorRepository : GenericRespository<Sire.Data.Entities.Operator.Operator, SireContext>, IOperatorRepository
    {
        private readonly IJwtTokenAccesser _jwtTokenAccesser;

        public OperatorRepository(IUnitOfWork<SireContext> uow,
            IJwtTokenAccesser jwtTokenAccesser)
            : base(uow, jwtTokenAccesser)
        {
            _jwtTokenAccesser = jwtTokenAccesser;
        }

        public List<DropDownDto> GetOperatorDropDown()
        {
            return All.Where(x => x.DeletedDate == null)
                .Select(c => new DropDownDto { Id = c.Id, Value = c.Name }).OrderBy(o => o.Value).ToList();
        }

        public string Duplicate(Sire.Data.Entities.Operator.Operator Operator)
        {
            if (All.Any(x => x.Id != Operator.Id && x.Name == Operator.Name && x.DeletedDate == null))
                return "Duplicate Operator name : " + Operator.Name;
            return "";
        }
    }
}
