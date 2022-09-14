using Sire.Common.GenericRespository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sire.Respository.Inspection
{
    public interface IInspectionRepository : IGenericRepository<Sire.Data.Entities.Inspection.Inspection>
    {
       // string Duplicate(Sire.Data.Entities.Inspection.Inspection inspection);
    }
}
