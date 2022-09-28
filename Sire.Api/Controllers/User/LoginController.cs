using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sire.Api.Controllers.Common;
using Sire.Api.Helpers;
using Sire.Common.UnitOfWork;
using Sire.Data.Dto.Master;
using Sire.Data.Dto.UserMgt;
using Sire.Data.Entities.UserMgt;
using Sire.Domain.Context;
using Sire.Helper;
using Sire.Respository.Master;
using Sire.Respository.UserMgt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Sire.Api.Controllers.User
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : BaseController
    {

        private readonly IJwtTokenAccesser _jwtTokenAccesser;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;

        private readonly IUnitOfWork<SireContext> _uow;

        public LoginController(IUserRepository userRepository,
            IUnitOfWork<SireContext> uow, IMapper mapper,
            IJwtTokenAccesser jwtTokenAccesser)
        {
            _userRepository = userRepository;
            _uow = uow;
            _mapper = mapper;
            _jwtTokenAccesser = jwtTokenAccesser;
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = _uow.Context.User.Where(x => x.EmailId == dto.UserName && x.Password == dto.Password).FirstOrDefault();
            if (user == null)
            {
                ModelState.AddModelError("UserName", "Invalid username or password");
                return BadRequest(ModelState);
            }


            var roleId = _uow.Context.Role.Where(x => x.User_Id == user.Id).FirstOrDefault();


            return Ok(BuildUserAuthObject(user, (int)roleId.RoleType));
        }

        private LoginResponseDto BuildUserAuthObject(Sire.Data.Entities.UserMgt.User authUser, int roleId)
        {
            var roleTokenId = new Guid().ToString();
            var vessel = _uow.Context.User_Vessel.Where(x => x.User_Id == authUser.Id).FirstOrDefault();

            var login = new LoginResponseDto
            {
                Token = roleId == 0 ? null : BuildJwtToken(authUser, roleId),
                RefreshToken = null,
                ExpiredAfter = DateTime.UtcNow.AddMinutes(60),
                IsFirstTime = true,
                UserId = authUser.Id,
                RoleTokenId = roleTokenId,
                Full_Name = authUser.UserName,
                EmailId = authUser.EmailId,
                RoleId = roleId,
                VesselId = vessel != null ? vessel.Vessel_Id : 0
            };


            /*var imageUrl = _uploadSettingRepository.All
               .FirstOrDefault()?.ImageUrl;*/
            /*
                        login.RoleName = _uow.Context.SecurityRole.Find(roleId)?.RoleName;

                        authUser.RoleTokenId = roleTokenId;
                        if (roleId > 0)
                        {
                            authUser.IsLogin = true;
                            authUser.RoleTokenId = null;
                            authUser.LastLoginDate = DateTime.Now;

                        }*/

            //if (!string.IsNullOrEmpty(login.Token))
            //{
            //    _userRepository.UpdateRefreshToken(login.UserId, login.RefreshToken);
            //    _userRepository.Update(authUser);
            //}

            _uow.Save();
            return login;
        }

        private string BuildJwtToken(Sire.Data.Entities.UserMgt.User authUser, int roleId)
        {
            var userInfo = new UserInfo();
            userInfo.UserId = authUser.Id;
            userInfo.UserName = authUser.EmailId;
            userInfo.CompanyId = 1;
            userInfo.RoleId = roleId;
            var claims = new List<Claim> { new Claim("sire_user_token", userInfo.ToJsonString()) };
            return _userRepository.GenerateAccessToken(claims);
        }

    }
}
