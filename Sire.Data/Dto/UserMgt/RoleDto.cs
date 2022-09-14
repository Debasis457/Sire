using Sire.Data.Entities.Common;

namespace Sire.Data.Dto.UserMgt
{
    public class RoleDto : BaseDto
    {
        public string Description { get; set; }
        public Role_Types RoleType { get; set; }
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
