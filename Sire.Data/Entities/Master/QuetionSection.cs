using Sire.Data.Entities.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sire.Data.Entities.Master
{
    public class QuetionSection : BaseEntity
    {
        public string Name { get; set; }
        public virtual List<QuetionSubSection> QuetionSubSection { get; set; }
    }
}
