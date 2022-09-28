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

        [Required(ErrorMessage = "Please Select Vessel")]
        public int Vessel_Id { get; set; }

        [Required(ErrorMessage = "Please Enter User Name")]
        public string UserName { get; set; }
        public string  VesselIMo { get; set; }

        /* public bool? is_admin { get; set; }*/

        [Required(ErrorMessage = "Please Select Rank ID")]
        public int Rank_Id { get; set; }

        [Required(ErrorMessage = "Please Select Rank Group ID")]
        public int Rank_Group_Id { get; set; }
        [Required(ErrorMessage = "Please Enter Password ")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Confirm password doesn't match, Type again !")]
        public string ConfirmPassword { get; set; }

        public Vessel Vessel { get; set; }
        public User_Rank User_Rank { get; set; }

        public RankGroup RankGroup { get; set; }
        public User_Types UserType { get; set; }
        public int MobileNo { get; set; }

    }

    public enum User_Types
    {
        Ecg_Admin = 1,
        Operator = 2,
        Operator_Admin = 3,
        Captain = 4,
        Reviewer_Crew = 5,
        Non_Reviewer_Crew = 6
    }
}
