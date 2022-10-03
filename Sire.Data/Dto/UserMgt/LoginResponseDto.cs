using System;

namespace Sire.Data.Dto.UserMgt
{
    public class LoginResponseDto
    {
        public string Token { get; set; }

        public string RefreshToken { get; set; }

        public DateTime ExpiredAfter { get; set; }

        public bool IsFirstTime { get; set; }

        public bool PassowordExpired { get; set; }

        public int UserId { get; set; }

        public string RoleTokenId { get; set; }

        public string Full_Name { get; set; }

        public string EmailId { get; set; }

        public int RoleId { get; set; }

        public string RoleName { get; set; }

        public int? VesselId { get; set; }

        public int RankId { get; set; }

        public int RankGroupId { get; set; }
    }
}
