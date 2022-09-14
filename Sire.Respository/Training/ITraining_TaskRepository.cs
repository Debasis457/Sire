using Sire.Common.GenericRespository;
using Sire.Data.Dto.Master;
using Sire.Data.Entities.Question;
using Sire.Data.Entities.Training;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sire.Respository.Training
{
    public interface ITraining_TaskRepository : IGenericRepository<Training_Task>
    {
        List<DropDownDto> GetTraining_taskDropDown();
        string Duplicate(Training_Task Training_Task);
    }
}
