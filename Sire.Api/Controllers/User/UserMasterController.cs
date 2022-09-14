using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sire.Common.UnitOfWork;
using Sire.Data.Dto.UserMgt;
using Sire.Domain.Context;
using Sire.Helper;
using Sire.Respository.UserMgt;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sire.Api.Controllers.User
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserMasterController : ControllerBase
    {
        private readonly IJwtTokenAccesser _jwtTokenAccesser;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork<SireContext> _uow;

        public UserMasterController(IUserRepository userRepository,
            IUnitOfWork<SireContext> uow, IMapper mapper,
            IJwtTokenAccesser jwtTokenAccesser)
        {
            _userRepository = userRepository;
            _uow = uow;
            _mapper = mapper;
            _jwtTokenAccesser = jwtTokenAccesser;
        }
        [AllowAnonymous]
        [HttpGet]
        [HttpGet("{isDeleted:bool?}")]
        public IActionResult Get(bool isDeleted)
        {
            var tests = _userRepository.FindByInclude(x => x.IsDeleted == isDeleted, x => x.User_Rank, x => x.RankGroup)
                .OrderByDescending(x => x.Id).ToList();
            var testsDto = _mapper.Map<IEnumerable<UserDto>>(tests);

            return Ok(testsDto);
        }
        [AllowAnonymous]
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            if (id <= 0) return BadRequest();
            var test = _userRepository.Find(id);
            var OperatorDto = _mapper.Map<UserDto>(test);
            return Ok(OperatorDto);
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Post([FromBody] UserDto userDto)
        {
            if (!ModelState.IsValid) return new UnprocessableEntityObjectResult(ModelState);
            //userDto.Id = 0;
            var test = _mapper.Map<Sire.Data.Entities.UserMgt.User>(userDto);
           /* var validate = _userRepository.Duplicate(test);
            if (!string.IsNullOrEmpty(validate))
            {
                ModelState.AddModelError("Message", validate);
                return BadRequest(ModelState);
            }*/
            if (userDto.Id == 0)
                _userRepository.Add(test);
            else
                _userRepository.Update(test);
            if (_uow.Save() <= 0) throw new Exception("Creating User failed on save.");
            return Ok(test.Id);
        }
        [AllowAnonymous]
        [HttpPut]
        public IActionResult Put(UserDto userDto)
        {
            if (userDto.Id <= 0) return BadRequest();

            if (!ModelState.IsValid) return new UnprocessableEntityObjectResult(ModelState);

            var test = _mapper.Map<Sire.Data.Entities.UserMgt.User>(userDto);
            var validate = _userRepository.Duplicate(test);
            if (!string.IsNullOrEmpty(validate))
            {
                ModelState.AddModelError("Message", validate);
                return BadRequest(ModelState);
            }

            /* Added by Vipul for effective Date on 14-10-2019 */
            Delete(test.Id);
            test.Id = 0;
            _userRepository.Add(test);

            if (_uow.Save() <= 0) throw new Exception("Updating user failed on save.");
            return Ok(test.Id);
        }
        [AllowAnonymous]
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var record = _userRepository.Find(id);

            if (record == null)
                return NotFound();

            _userRepository.Delete(record);
            _uow.Save();

            return Ok();
        }

        [HttpGet]
        [Route("GetUserDropDown")]
        public IActionResult GetUserDropDown()
        {
            return Ok(_userRepository.GetUserDropDown());
        }

    }
}
