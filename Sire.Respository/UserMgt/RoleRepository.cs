using Sire.Common.GenericRespository;
using Sire.Common.UnitOfWork;
using Sire.Data.Dto.Master;
using Sire.Data.Entities.UserMgt;
using Sire.Domain.Context;
using Sire.Helper;
using System.Collections.Generic;
using System.Linq;

namespace Sire.Respository.UserMgt
{
    public class RoleRepository : GenericRespository<Role, SireContext>, IRoleRepository
    {
        private readonly IJwtTokenAccesser _jwtTokenAccesser;

        public RoleRepository(IUnitOfWork<SireContext> uow,
            IJwtTokenAccesser jwtTokenAccesser)
            : base(uow, jwtTokenAccesser)
        {
            _jwtTokenAccesser = jwtTokenAccesser;
        }

        public List<DropDownDto> GetRoleDropDown()
        {
            return All.Where(x =>
                    (x.DeletedDate == null))
                .Select(c => new DropDownDto { Id = c.Id, Value = c.Description }).OrderBy(o => o.Value).ToList();
        }

        public string Duplicate(Role objSave)
        {
            if (All.Any(x => x.Id != objSave.Id && x.Description == objSave.Description && x.DeletedDate == null))
                return "Duplicate Description name : " + objSave.Description;
            return "";
        }
    }
}
