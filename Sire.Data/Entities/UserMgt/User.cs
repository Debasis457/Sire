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
        public int Operator_Id { get; set; }
        public string Full_Name { get; set; }
        public bool? is_admin { get; set; }
        [ForeignKey("User_Rank")]
        public int Rank_Id { get; set; }
        [ForeignKey("RankGroup")]
        public int Rank_Group_Id { get; set; }
        public string Password { get; set; }

        public User_Rank User_Rank { get; set; }

        public RankGroup RankGroup { get; set; }


    }
}
