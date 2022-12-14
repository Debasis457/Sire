using System;
using System.ComponentModel.DataAnnotations;
using Sire.Data.Entities.Common;

namespace Sire.Data.Dto.UserMgt
{
    public class SecurityRoleDto : BaseDto
    {
        [Required(ErrorMessage = "Role short name is required.")]
        public string RoleShortName { get; set; }

        [Required(ErrorMessage = "Role name is required.")]
        public string RoleName { get; set; }

        public bool IsSystemRole { get; set; }


        public string CreatedByUser { get; set; }
        public string DeletedByUser { get; set; }
        public string ModifiedByUser { get; set; }
        public int? CreatedBy { get; set; }
        public int? DeletedBy { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public int? CompanyId { get; set; }
        public string CompanyName { get; set; }
    }
}