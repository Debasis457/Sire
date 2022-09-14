using Sire.Data.Entities.Common;
using Sire.Data.Entities.Master;
using System.ComponentModel.DataAnnotations;

namespace Sire.Data.Dto.UserMgt
{
    public class UserDto : BaseDto
    {
        public string UserID { get; set; }
        public string EmailId { get; set; }
        public int Operator_Id { get; set; }
        public string Full_Name { get; set; }
        public bool? is_admin { get; set; }
        public int Rank_Id { get; set; }
        public int Rank_Group_Id { get; set; }
        [Required(ErrorMessage = "Please fill this field")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Confirm password doesn't match, Type again !")]
        public string ConfirmPassword { get; set; }

        public Sire.Data.Entities.Operator.Operator Operator { get; set; }
        public User_Rank User_Rank { get; set; }

        public RankGroup RankGroup { get; set; }

    }
}
