using Microsoft.IdentityModel.Tokens;
using Sire.Common.GenericRespository;
using Sire.Common.UnitOfWork;
using Sire.Data.Dto.Master;
using Sire.Data.Dto.UserMgt;
using Sire.Data.Entities.UserMgt;
using Sire.Domain.Context;
using Sire.Helper;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Runtime;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Sire.Respository.UserMgt
{
    public class UserRepository : GenericRespository<User, SireContext>, IUserRepository
    {
        private readonly IJwtTokenAccesser _jwtTokenAccesser;
        private readonly JwtSettings _settings;

        public UserRepository(IUnitOfWork<SireContext> uow,
            JwtSettings settings,
            IJwtTokenAccesser jwtTokenAccesser)
            : base(uow, jwtTokenAccesser)
        {
            _jwtTokenAccesser = jwtTokenAccesser;
            _settings = settings;

        }

        public List<DropDownDto> GetUserDropDown()
        {
            return All.Where(x =>
                    (x.DeletedDate == null))
                .Select(c => new DropDownDto { Id = c.Id, Value = c.Full_Name }).OrderBy(o => o.Value).ToList();
        }

        public string Duplicate(User objSave)
        {
            if (All.Any(x => x.Id != objSave.Id && x.EmailId == objSave.EmailId && x.DeletedDate == null))
                return "Duplicate Email Id name : " + objSave.EmailId;
            return "";
        }

        public string GenerateAccessToken(IEnumerable<Claim> claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.Key));

            var jwtToken = new JwtSecurityToken(issuer: _settings.Issuer,
                audience: _settings.Audience,
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddMinutes(_settings.MinutesToExpiration),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            );
            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        //public void UpdateRefreshToken(int userid, string refreshToken)
        //{
        //    var rereshToken = _refreshTokenRepository.FindBy(t => t.UserId == userid).FirstOrDefault();

        //    if (rereshToken == null)
        //    {
        //        rereshToken = new RefreshToken();
        //        rereshToken.UserId = userid;
        //        rereshToken.Token = refreshToken;
        //        rereshToken.ExpiredOn = DateTime.UtcNow.AddDays(1);
        //        _refreshTokenRepository.Add(rereshToken);
        //    }
        //    else
        //    {
        //        rereshToken.Token = refreshToken;
        //        rereshToken.ExpiredOn = DateTime.UtcNow.AddDays(1);
        //        _refreshTokenRepository.Update(rereshToken);
        //    }
        //}

        //public RefreshTokenDto Refresh(string accessToken, string refreshToken)
        //{
        //    var principal = GetPrincipalFromExpiredToken(accessToken);

        //    var login = Context.RefreshToken.FirstOrDefault(t =>
        //        t.Token == refreshToken && t.ExpiredOn > DateTime.UtcNow);

        //    if (login == null) throw new SecurityTokenException("Refresh token not found or has been expired.");

        //    return new RefreshTokenDto
        //    {
        //        AccessToken = GenerateAccessToken(principal.Claims),
        //        ExpiredAfter = DateTime.UtcNow.AddMinutes(_settings.MinutesToExpiration),
        //        RefreshToken = refreshToken
        //    };
        //}

        //private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        //{
        //    var tokenValidationParameters = new TokenValidationParameters
        //    {
        //        ValidateAudience = false, //you might want to validate the audience and issuer depending on your use case
        //        ValidateIssuer = false,
        //        ValidateIssuerSigningKey = true,
        //        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.Key)),
        //        ValidateLifetime = false //here we are saying that we don't care about the token's expiration date
        //    };

        //    var tokenHandler = new JwtSecurityTokenHandler();
        //    var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);
        //    var jwtSecurityToken = securityToken as JwtSecurityToken;
        //    if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
        //        throw new SecurityTokenException("Invalid token");

        //    return principal;
        //}
    }
}
