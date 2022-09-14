using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sire.Common.UnitOfWork;
using Sire.Data.Entities.ShipManagement;
using Sire.Domain.Context;
using Sire.Helper;
using Sire.Respository.Master;
using Sire.Respository.ShipManagement;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sire.Api.Controllers.ShipManagement
{
    [Route("api/[controller]")]
    [ApiController]
    public class PIQ_HVPQResponseController : ControllerBase

    {
        private readonly IJwtTokenAccesser _jwtTokenAccesser;
        private readonly IMapper _mapper;
        private readonly IPIQ_HVPQReponseRepository _PIQ_HVPQResponseRepository;
        private readonly IUnitOfWork<SireContext> _uow;

        public PIQ_HVPQResponseController(IPIQ_HVPQReponseRepository PiqHvpqResponseRepository,
            IUnitOfWork<SireContext> uow, IMapper mapper,
            IJwtTokenAccesser jwtTokenAccesser)
        {
            _PIQ_HVPQResponseRepository = PiqHvpqResponseRepository;
            _uow = uow;
            _mapper = mapper;
            _jwtTokenAccesser = jwtTokenAccesser;
        }
        [AllowAnonymous]
        [HttpGet]
        [HttpGet("{isDeleted:bool?}")]
        public IActionResult Get(bool isDeleted)
        {
            var tests = _PIQ_HVPQResponseRepository.FindByInclude(x => x.IsDeleted == isDeleted)
                .OrderByDescending(x => x.Id).ToList();
            var testsDto = _mapper.Map<IEnumerable<PIQ_HVPQ_ResponseDto>>(tests);

            return Ok(testsDto);
        }
        [AllowAnonymous]
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            if (id <= 0) return BadRequest();
            var test = _PIQ_HVPQResponseRepository.Find(id);
            var FleetDto = _mapper.Map<PIQ_HVPQ_ResponseDto>(test);
            return Ok(FleetDto);
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Post([FromBody] PIQ_HVPQ_ResponseDto FleetDto)
        {
            if (!ModelState.IsValid) return new UnprocessableEntityObjectResult(ModelState);
            var test = _mapper.Map<PIQ_HVPQ_Response>(FleetDto);
            var validate = _PIQ_HVPQResponseRepository.Duplicate(test);
            if (!string.IsNullOrEmpty(validate))
            {
                ModelState.AddModelError("Message", validate);
                return BadRequest(ModelState);
            }
            if (FleetDto.Id == 0)
                _PIQ_HVPQResponseRepository.Add(test);
            else
                _PIQ_HVPQResponseRepository.Update(test);
            if (_uow.Save() <= 0) throw new Exception("Creating Test failed on save.");
            return Ok(test.Id);
        }
        [AllowAnonymous]
        [HttpPut]
        public IActionResult Put(PIQ_HVPQ_ResponseDto FleetDto)
        {
            if (FleetDto.Id <= 0) return BadRequest();

            if (!ModelState.IsValid) return new UnprocessableEntityObjectResult(ModelState);

            var test = _mapper.Map<PIQ_HVPQ_Response>(FleetDto);
            var validate = _PIQ_HVPQResponseRepository.Duplicate(test);
            if (!string.IsNullOrEmpty(validate))
            {
                ModelState.AddModelError("Message", validate);
                return BadRequest(ModelState);
            }

            /* Added by Vipul for effective Date on 14-10-2019 */
            Delete(test.Id);
            test.Id = 0;
            _PIQ_HVPQResponseRepository.Add(test);

            if (_uow.Save() <= 0) throw new Exception("Updating Test failed on save.");
            return Ok(test.Id);
        }
        [AllowAnonymous]
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var record = _PIQ_HVPQResponseRepository.Find(id);

            if (record == null)
                return NotFound();

            _PIQ_HVPQResponseRepository.Delete(record);
            _uow.Save();

            return Ok();
        }

        [HttpGet]
        [Route("GetPIQHVPQResponseDropDown")]
        public IActionResult GetResponseDropDown()
        {
            return Ok(_PIQ_HVPQResponseRepository.GetPIQHVPQResponseDropDown());
        }
    }
}

