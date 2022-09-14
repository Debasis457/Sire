using Sire.Data.Entities.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sire.Data.Entities.ShipManagement
{
    public class Vessel_Response_Piq_Hvpq : BaseEntity
    {
        [ForeignKey("Piq_Hvpq")]

        public int Vessel_Id { get; set; }
        public string Response { get; set; }
        public int Piq_Hvpq_Id { get; set; }
        public Piq_Hvpq Piq_Hvpq { get; set; }
    }
}
