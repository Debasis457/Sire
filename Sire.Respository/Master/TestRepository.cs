using System.Collections.Generic;
using System.Linq;
using Sire.Common.GenericRespository;
using Sire.Common.UnitOfWork;
using Sire.Data.Dto.Master;
using Sire.Data.Entities.Master;
using Sire.Domain.Context;
using Sire.Helper;

namespace Sire.Respository.Master
{
    public class TestRepository : GenericRespository<Test, SireContext>, ITestRepository
    {
        private readonly IJwtTokenAccesser _jwtTokenAccesser;

        public TestRepository(IUnitOfWork<SireContext> uow,
            IJwtTokenAccesser jwtTokenAccesser)
            : base(uow, jwtTokenAccesser)
        {
            _jwtTokenAccesser = jwtTokenAccesser;
        }

        public List<DropDownDto> GetTestDropDown()
        {
            return All.Where(x =>
                    (x.CompanyId == null || x.CompanyId == _jwtTokenAccesser.CompanyId) && x.DeletedDate == null)
                .Select(c => new DropDownDto {Id = c.Id, Value = c.TestName}).OrderBy(o => o.Value).ToList();
        }

        public List<DropDownDto> GetTestDropDownByTestGroup(int id)
        {
            return All.Where(x => x.TestGroupId == id && x.DeletedDate == null)
                .Select(c => new DropDownDto {Id = c.Id, Value = c.TestName}).OrderBy(o => o.Value).ToList();
        }

        public string Duplicate(Test objSave)
        {
            if (All.Any(x => x.Id != objSave.Id && x.TestName == objSave.TestName && x.DeletedDate == null))
                return "Duplicate Test name : " + objSave.TestName;
            return "";
        }
    }
}