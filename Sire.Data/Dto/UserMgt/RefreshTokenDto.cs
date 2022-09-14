using System;

namespace Sire.Data.Dto.UserMgt
{
    public class RefreshTokenDto
    {
        public string AccessToken { get; set; }
        public DateTime? ExpiredAfter { get; set; }
        public string RefreshToken { get; set; }
    }
}