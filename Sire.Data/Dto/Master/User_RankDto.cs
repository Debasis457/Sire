using Sire.Data.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Sire.Data.Dto.Master
{
    public class User_RankDto:BaseDto
    {
        [Required(ErrorMessage ="Please Enter rank")]
        public string Rank { get; set; }
    }
}
