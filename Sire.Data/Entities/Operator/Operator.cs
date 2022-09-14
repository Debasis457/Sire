﻿using Sire.Data.Entities.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sire.Data.Entities.Operator
{
    public class Operator : BaseEntity
    {
       /* [Column("Description", TypeName = "VARCHAR(100)")]*/
        public string Name { get; set; }

        public string Description { get; set; }
    }
}
