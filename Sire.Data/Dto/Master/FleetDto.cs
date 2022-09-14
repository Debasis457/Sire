using Sire.Data.Entities.Common;
using Sire.Data.Entities.UserMgt;
using System.Collections.Generic;

namespace Sire.Data.Dto.Master
{
    public class FleetDto : BaseDto
    {
        public string Name { get; set; }
        public int Fleet_Head_Id { get; set; }

        public User User { get; set; }
    }
}
