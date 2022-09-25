using Sire.Data.Entities.Common;
using Sire.Data.Entities.Master;
using System.ComponentModel.DataAnnotations;

namespace Sire.Data.Dto.UserMgt
{
    public class UserDto : BaseDto
    {
        public string UserID { get; set; }

        [Required(ErrorMessage = "Please Enter Email")]
        public string EmailId { get; set; }

        [Required(ErrorMessage = "Please Select Operator")]
        public int Operator_Id { get; set; }

        [Required(ErrorMessage = "Please Enter Full Name")]
        public string Full_Name { get; set; }

        
        public bool? is_admin { get; set; }

        [Required(ErrorMessage = "Please Select Rank ID")]
        public int Rank_Id { get; set; }

        [Required(ErrorMessage = "Please Select Rank Group ID")]
        public int Rank_Group_Id { get; set; }
        [Required(ErrorMessage = "Please Enter Password ")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Confirm password doesn't match, Type again !")]
        public string ConfirmPassword { get; set; }

        public Sire.Data.Entities.Operator.Operator Operator { get; set; }
        public User_Rank User_Rank { get; set; }

        public RankGroup RankGroup { get; set; }

    }
}
