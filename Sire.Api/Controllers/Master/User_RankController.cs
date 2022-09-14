using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Sire.Common.UnitOfWork;
using Sire.Data.Dto.Master;
using Sire.Data.Entities.Master;
using Sire.Domain.Context;
using Sire.Helper;
using Sire.Respository.Master;
using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Authorization;
using System.Linq;

namespace Sire.Api.Controllers.Master
{
    [Route("api/[controller]")]
    [ApiController]
    public class User_RankController : ControllerBase
    {
        private readonly IJwtTokenAccesser _jwtTokenAccesser;
        private readonly IMapper _mapper;
        private readonly IUser_RankRepository _user_RankRepository;
        private readonly IUnitOfWork<SireContext> _uow;

        public User_RankController(IUser_RankRepository user_RankRepository,
            IUnitOfWork<SireContext> uow, IMapper mapper,
            IJwtTokenAccesser jwtTokenAccesser)
        {
            _user_RankRepository = user_RankRepository;
            _uow = uow;
            _mapper = mapper;
            _jwtTokenAccesser = jwtTokenAccesser;
        }
        [AllowAnonymous]
        [HttpGet]
        [HttpGet("{isDeleted:bool?}")]
        public IActionResult Get(bool isDeleted)
        {
            var tests = _user_RankRepository.FindByInclude(x => x.IsDeleted == isDeleted)
                .OrderByDescending(x => x.Id).ToList();
            var testsDto = _mapper.Map<IEnumerable<User_RankDto>>(tests);

            return Ok(testsDto);
        }
        [AllowAnonymous]
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            if (id <= 0) return BadRequest();
            var test = _user_RankRepository.Find(id);
            var UserRankDto = _mapper.Map<User_RankDto>(test);
            return Ok(UserRankDto);
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Post([FromBody] User_RankDto user_RankDto)
        {
            if (!ModelState.IsValid) return new UnprocessableEntityObjectResult(ModelState);
            var test = _mapper.Map<User_Rank>(user_RankDto);

            if (user_RankDto.Id == 0)
                _user_RankRepository.Add(test);
            else
                _user_RankRepository.Update(test);
            if (_uow.Save() <= 0) throw new Exception("Creating Test failed on save.");
            return Ok(test.Id);
        }
        [AllowAnonymous]
        [HttpPut]
        public IActionResult Put(User_RankDto user_RankDto)
        {
            if (user_RankDto.Id <= 0) return BadRequest();

            if (!ModelState.IsValid) return new UnprocessableEntityObjectResult(ModelState);

            var test = _mapper.Map<User_Rank>(user_RankDto);
            var validate = _user_RankRepository.Duplicate(test);
            if (!string.IsNullOrEmpty(validate))
            {
                ModelState.AddModelError("Message", validate);
                return BadRequest(ModelState);
            }

            /* Added by Vipul for effective Date on 14-10-2019 */
            Delete(test.Id);
            test.Id = 0;
            _user_RankRepository.Add(test);

            if (_uow.Save() <= 0) throw new Exception("Updating Test failed on save.");
            return Ok(test.Id);
        }
        [AllowAnonymous]
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var record = _user_RankRepository.Find(id);

            if (record == null)
                return NotFound();

            _user_RankRepository.Delete(record);
            _uow.Save();


            return Ok();
        }



        [HttpGet]
        [Route("GetUser_RankDropDown")]
        public IActionResult GetUser_RankDropDown()
        {
            return Ok(_user_RankRepository.GetUser_RankDropDown());
        }

    }
}
