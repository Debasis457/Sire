using Sire.Data.Dto.Master;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Sire.Data.Dto.UserMgt
{
    public class LoginDto
    {
        [Required(ErrorMessage = "Please provide username")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Please provide password")]
        public string Password { get; set; }
        public int? RoleId { get; set; }

        //public IList<DropDownDto> Roles { get; set; }
    }
}
