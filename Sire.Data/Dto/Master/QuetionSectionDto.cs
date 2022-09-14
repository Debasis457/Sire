using Sire.Data.Entities.Common;
using Sire.Data.Entities.Master;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sire.Data.Dto.Master
{
    public class QuetionSectionDto : BaseDto
    {
        public string Name { get; set; }
        public virtual List<QuetionSubSectionDto> QuetionSubSection { get; set; }
        public int? Total { get; set; }
    }
}
