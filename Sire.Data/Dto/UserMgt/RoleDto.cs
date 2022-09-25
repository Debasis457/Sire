using Sire.Data.Entities.Common;
using Sire.Data.Entities.UserMgt;
using System.ComponentModel.DataAnnotations;

namespace Sire.Data.Dto.UserMgt
{
    public class RoleDto : BaseDto
    {
        [Required(ErrorMessage = "Please Enter Description")]
        public string Description { get; set; }
        public Role_Types RoleType { get; set; }

        public int User_Id { get; set; }

        public User User { get; set; }

    }

    public enum Role_Types
    {
        Ecg_Admin = 1,
        Operator = 2,
        Operator_Admin = 3,
        Captain = 4,
        Reviewer_Crew = 5,
        Non_Reviewer_Crew = 6
    }
}
