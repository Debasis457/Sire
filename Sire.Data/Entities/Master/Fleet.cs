using Sire.Data.Entities.Common;
using Sire.Data.Entities.UserMgt;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sire.Data.Entities.Master
{
    public class Fleet : BaseEntity
    {
        public string Name { get; set; }
        [ForeignKey("User")]
        public int Fleet_Head_Id { get; set; }
        public User User { get; set; }
    }
}
