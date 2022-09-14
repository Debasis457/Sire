using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sire.Common.UnitOfWork;
using Sire.Data.Dto.Operator;
using Sire.Domain.Context;
using Sire.Helper;
using Sire.Respository.Operator;
using System.Collections.Generic;
using System;
using Sire.Respository.Master;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using Sire.Data.Entities.Master;
using Sire.Data.Dto.Master;

namespace Sire.Api.Controllers.Master
{
    [Route("api/[controller]")]
    [ApiController]
    public class FleetController : ControllerBase

    {
        private readonly IJwtTokenAccesser _jwtTokenAccesser;
        private readonly IMapper _mapper;
        private readonly IFleetRepository _fleetRepository;
        private readonly IUnitOfWork<SireContext> _uow;

        public FleetController(IFleetRepository fleetRepository,
            IUnitOfWork<SireContext> uow, IMapper mapper,
            IJwtTokenAccesser jwtTokenAccesser)
        {
            _fleetRepository = fleetRepository;
            _uow = uow;
            _mapper = mapper;
            _jwtTokenAccesser = jwtTokenAccesser;
        }
        [AllowAnonymous]
        [HttpGet]
        [HttpGet("{isDeleted:bool?}")]
        public IActionResult Get(bool isDeleted)
        {
            var tests = _fleetRepository.FindByInclude(x => x.IsDeleted == isDeleted, x => x.User)
                .OrderByDescending(x => x.Id).ToList();
            var testsDto = _mapper.Map<IEnumerable<FleetDto>>(tests);

            return Ok(testsDto);
        }
        [AllowAnonymous]
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            if (id <= 0) return BadRequest();
            var test = _fleetRepository.Find(id);
            var FleetDto = _mapper.Map<FleetDto>(test);
            return Ok(FleetDto);
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Post([FromBody] FleetDto FleetDto)
        {
            if (!ModelState.IsValid) return new UnprocessableEntityObjectResult(ModelState);
            var test = _mapper.Map<Fleet>(FleetDto);

            if (FleetDto.Id == 0)
                _fleetRepository.Add(test);
            else
                _fleetRepository.Update(test);
            if (_uow.Save() <= 0) throw new Exception("Creating Test failed on save.");
            return Ok(test.Id);
        }
        [AllowAnonymous]
        [HttpPut]
        public IActionResult Put(FleetDto FleetDto)
        {
            if (FleetDto.Id <= 0) return BadRequest();

            if (!ModelState.IsValid) return new UnprocessableEntityObjectResult(ModelState);

            var test = _mapper.Map<Fleet>(FleetDto);
            var validate = _fleetRepository.Duplicate(test);
            if (!string.IsNullOrEmpty(validate))
            {
                ModelState.AddModelError("Message", validate);
                return BadRequest(ModelState);
            }

            /* Added by Vipul for effective Date on 14-10-2019 */
            Delete(test.Id);
            test.Id = 0;
            _fleetRepository.Add(test);

            if (_uow.Save() <= 0) throw new Exception("Updating Test failed on save.");
            return Ok(test.Id);
        }
        [AllowAnonymous]
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var record = _fleetRepository.Find(id);

            if (record == null)
                return NotFound();

            _fleetRepository.Delete(record);
            _uow.Save();


            return Ok();
        }



        [HttpGet]
        [Route("GetFleetDropDown")]
        public IActionResult GetFleetDropDown()
        {
            return Ok(_fleetRepository.GetFleetDropDown());
        }
    }
}
