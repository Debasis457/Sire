using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sire.Data.Dto.Master;
using Sire.Data.Entities.Master;

namespace Sire.Web.Models
{
    public class OperatorDashboardModel
    {
        //public IEnumerable<IGrouping<FleetKeyModel, User_VesselDto>> FleetVessels { get; set; }
        //public IEnumerable<IGrouping<Fleet, User_VesselDto>> FleetVessels { get; set; }
        //public IList<FleetVesselModel> FleetVessels { get; set; }

       public IDictionary<string, IList<User_VesselDto>> FleetVessels { get; set; }
    }
}
