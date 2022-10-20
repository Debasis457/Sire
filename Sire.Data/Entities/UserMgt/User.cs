using Sire.Data.Entities.Common;
using Sire.Data.Entities.Master;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sire.Data.Entities.UserMgt
{
    public class User : BaseEntity
    {
        public string UserID { get; set; }
        public string EmailId { get; set; }
        public int? Vessel_Id { get; set; }
        public string UserName { get; set; }
        public bool? is_admin { get; set; }
        [ForeignKey("User_Rank")]
        public int? Rank_Id { get; set; }
        [ForeignKey("RankGroup")]
        public int? Rank_Group_Id { get; set; }
        public string Password { get; set; }   
        public virtual User_Rank User_Rank { get; set; }
        public virtual RankGroup RankGroup { get; set; }
        public virtual User_Types UserType { get; set; }
        public string MobileNo { get; set; }
        
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
