using Sire.Common.GenericRespository;
using Sire.Data.Dto.Master;
using Sire.Data.Entities.Training;
using Sire.Data.Entities.Master;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sire.Respository.Training
{
    public interface ITrainingRepository : IGenericRepository<Sire.Data.Entities.Training.Training>
    {
        List<DropDownDto> GetTrainingDropDown();
        string Duplicate(Sire.Data.Entities.Training.Training training);
    }
}
