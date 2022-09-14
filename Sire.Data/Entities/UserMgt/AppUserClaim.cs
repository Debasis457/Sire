using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Sire.Data.Entities.Common;

namespace Sire.Data.Entities.UserMgt
{
    [Table("UserClaims")]
    public class AppUserClaim : BaseEntity
    {
        [Required] [Key] public Guid ClaimId { get; set; }

        [Required] public Guid UserId { get; set; }

        [Required] public string ClaimType { get; set; }

        [Required] public string ClaimValue { get; set; }
    }
}