using System.Collections.Generic;
using Sire.Common.GenericRespository;
using Sire.Data.Dto.Master;
using Sire.Data.Entities.Master;

namespace Sire.Respository.Master
{
    public interface ITestRepository : IGenericRepository<Test>
    {
        List<DropDownDto> GetTestDropDown();

        List<DropDownDto> GetTestDropDownByTestGroup(int id);
        string Duplicate(Test objSave);
    }
}