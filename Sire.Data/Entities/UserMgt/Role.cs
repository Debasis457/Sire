using Sire.Data.Entities.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sire.Data.Entities.UserMgt
{
    public class Role : BaseEntity
    {
        [Column("Description", TypeName = "VARCHAR(100)")]
        public string Description{ get; set; }
        public Role_Types RoleType { get; set; }
    }

   public  enum Role_Types
    {
        Ecg_Admin=1,
        Operator=2,
        Operator_Admin=3,
        Captain=4,
        Reviewer_Crew=5,
        Non_Reviewer_Crew=6
    }
}
