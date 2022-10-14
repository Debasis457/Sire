using Sire.Data.Entities.Common;
using Sire.Data.Entities.Master;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sire.Data.Dto.Master
{
    public class QuetionSubSectionDto : BaseDto
    {
        public int QuetionSectionId { get; set; }
        public int? SectionId { get; set; }
        public string Name { get; set; }
        public int?  Total{ get; set; }
        public int? ResTotal { get; set; }

    }
}
