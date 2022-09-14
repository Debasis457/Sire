using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sire.Common.UnitOfWork;
using Sire.Data.Dto.Operator;
using Sire.Data.Dto.UserMgt;
using Sire.Data.Entities.UserMgt;
using Sire.Domain.Context;
using Sire.Helper;
using Sire.Respository.Operator;
using Sire.Respository.UserMgt;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sire.Api.Controllers.User
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IJwtTokenAccesser _jwtTokenAccesser;
        private readonly IMapper _mapper;
        private readonly IRoleRepository _roleRepository;
        private readonly IUnitOfWork<SireContext> _uow;

        public RoleController(IRoleRepository roleRepository,
            IUnitOfWork<SireContext> uow, IMapper mapper,
            IJwtTokenAccesser jwtTokenAccesser)
        {
            _roleRepository = roleRepository;
            _uow = uow;
            _mapper = mapper;
            _jwtTokenAccesser = jwtTokenAccesser;
        }
        [AllowAnonymous]
        [HttpGet]
        [HttpGet("{isDeleted:bool?}")]
        public IActionResult Get(bool isDeleted)
        {
            var tests = _roleRepository.FindByInclude(x => x.IsDeleted == isDeleted)
                .OrderByDescending(x => x.Id).ToList();
            var testsDto = _mapper.Map<IEnumerable<RoleDto>>(tests);

            return Ok(testsDto);
        }
        [AllowAnonymous]
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            if (id <= 0) return BadRequest();
            var test = _roleRepository.Find(id);
            var OperatorDto = _mapper.Map<RoleDto>(test);
            return Ok(OperatorDto);
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Post([FromBody] RoleDto RoleDto)
        {
            if (!ModelState.IsValid) return new UnprocessableEntityObjectResult(ModelState);
            var test = _mapper.Map<Role>(RoleDto);
            var validate = _roleRepository.Duplicate(test);
            if (!string.IsNullOrEmpty(validate))
            {
                ModelState.AddModelError("Message", validate);
                return BadRequest(ModelState);
            }

            if (RoleDto.Id == 0)
                _roleRepository.Add(test);
            else
                _roleRepository.Update(test);
            if (_uow.Save() <= 0) throw new Exception("Creating Test failed on save.");
            return Ok(test.Id);
        }
        [AllowAnonymous]
        [HttpPut]
        public IActionResult Put(RoleDto OperatorDto)
        {
            if (OperatorDto.Id <= 0) return BadRequest();

            if (!ModelState.IsValid) return new UnprocessableEntityObjectResult(ModelState);

            var test = _mapper.Map<Role>(OperatorDto);
            var validate = _roleRepository.Duplicate(test);
            if (!string.IsNullOrEmpty(validate))
            {
                ModelState.AddModelError("Message", validate);
                return BadRequest(ModelState);
            }

            /* Added by Vipul for effective Date on 14-10-2019 */
            Delete(test.Id);
            test.Id = 0;
            _roleRepository.Add(test);

            if (_uow.Save() <= 0) throw new Exception("Updating Test failed on save.");
            return Ok(test.Id);
        }
        [AllowAnonymous]
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var record = _roleRepository.Find(id);

            if (record == null)
                return NotFound();

            _roleRepository.Delete(record);
            _uow.Save();

            return Ok();
        }

        [HttpGet]
        [Route("GetRoleDropDown")]
        public IActionResult GetRoleDropDown()
        {
            return Ok(_roleRepository.GetRoleDropDown());
        }

    }
}
