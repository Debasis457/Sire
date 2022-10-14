using Sire.Data.Entities.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sire.Data.Entities.Master
{
    public class QuetionSubSection : BaseEntity
    {
        [ForeignKey("QuetionSection")]
        public int QuetionSectionId { get; set; }
        public string Name { get; set; }
        public  int? SectionId{ get; set; }
        public QuetionSection QuetionSection { get; set; }
    }
}
