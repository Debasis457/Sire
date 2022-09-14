using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sire.Common.UnitOfWork;
using Sire.Data.Dto.Master;
using Sire.Data.Dto.UserMgt;
using Sire.Data.Entities.Master;
using Sire.Domain.Context;
using Sire.Helper;
using Sire.Respository.Master;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sire.Api.Controllers.Master
{
    [Route("api/[controller]")]
    [ApiController]
    public class User_VesselController : ControllerBase

    {
        private readonly IJwtTokenAccesser _jwtTokenAccesser;
        private readonly IMapper _mapper;
        private readonly IUser_VesselRepository _user_VesselRepository;
        private readonly IUnitOfWork<SireContext> _uow;

        public User_VesselController(IUser_VesselRepository uservesselRepository,
            IUnitOfWork<SireContext> uow, IMapper mapper,
            IJwtTokenAccesser jwtTokenAccesser)
        {
            _user_VesselRepository = uservesselRepository;
            _uow = uow;
            _mapper = mapper;
            _jwtTokenAccesser = jwtTokenAccesser;
        }
        [AllowAnonymous]
        [HttpGet]
        [HttpGet("{isDeleted:bool?}")]
        public IActionResult Get(bool isDeleted)
        {
            var tests = _user_VesselRepository.FindByInclude(x => x.IsDeleted == isDeleted, x => x.User)
                .OrderByDescending(x => x.Id).ToList();
            var testsDto = _mapper.Map<IEnumerable<User_VesselDto>>(tests);
            foreach (var item in testsDto)
            {
                item.Vessel = _uow.Context.Vessel.Where(x => x.Id == item.Vessel_Id).FirstOrDefault();
            }
            return Ok(testsDto);
        }
        [AllowAnonymous]
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            if (id <= 0) return BadRequest();
            var test = _user_VesselRepository.Find(id);
            var FleetDto = _mapper.Map<User_VesselDto>(test);
            return Ok(FleetDto);
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Post([FromBody] User_VesselDto User_VesselDto)
        {
            if (!ModelState.IsValid) return new UnprocessableEntityObjectResult(ModelState);
            // User_VesselDto.Id = 0;
            var test = _mapper.Map<User_Vessel>(User_VesselDto);

            if (User_VesselDto.Id == 0)
                _user_VesselRepository.Add(test);
            else
                _user_VesselRepository.Update(test);
            _user_VesselRepository.Add(test);
            if (_uow.Save() <= 0) throw new Exception("Creating Test failed on save.");
            return Ok(test.Id);
        }
        [AllowAnonymous]
        [HttpPut]
        public IActionResult Put(User_VesselDto User_VesselDto)
        {
            if (User_VesselDto.Id <= 0) return BadRequest();

            if (!ModelState.IsValid) return new UnprocessableEntityObjectResult(ModelState);

            var test = _mapper.Map<User_Vessel>(User_VesselDto);
            var validate = _user_VesselRepository.Duplicate(test);
            if (!string.IsNullOrEmpty(validate))
            {
                ModelState.AddModelError("Message", validate);
                return BadRequest(ModelState);
            }

            /* Added by Vipul for effective Date on 14-10-2019 */
            Delete(test.Id);
            test.Id = 0;
            _user_VesselRepository.Add(test);

            if (_uow.Save() <= 0) throw new Exception("Updating User_Vessel failed on save.");
            return Ok(test.Id);
        }
        [AllowAnonymous]
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var record = _user_VesselRepository.Find(id);

            if (record == null)
                return NotFound();

            _user_VesselRepository.Delete(record);
            _uow.Save();

            return Ok();
        }

        [HttpGet]
        [Route("GetUser_VesselDropDown")]
        public IActionResult GetUser_VesselDropDown()
        {
            return Ok(_user_VesselRepository.GetUser_VesselDropDown());
        }


        [HttpGet]
        [Route("GetUser_VesselDropDownForUser")]
        public IActionResult GetUser_VesselDropDownForUser()
        {
            return Ok(_user_VesselRepository.GetUser_VesselDropDown());
        }
    }
}
