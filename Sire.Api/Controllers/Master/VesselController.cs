using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sire.Common.UnitOfWork;
using Sire.Data.Dto.Master;
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
    public class VesselController : ControllerBase

    {
        private readonly IJwtTokenAccesser _jwtTokenAccesser;
        private readonly IMapper _mapper;
        private readonly IVesselRepository _vesselRepository;
        private readonly IUnitOfWork<SireContext> _uow;

        public VesselController(IVesselRepository vesselRepository,
            IUnitOfWork<SireContext> uow, IMapper mapper,
            IJwtTokenAccesser jwtTokenAccesser)
        {
            _vesselRepository = vesselRepository;
            _uow = uow;
            _mapper = mapper;
            _jwtTokenAccesser = jwtTokenAccesser;
        }
        [AllowAnonymous]
        [HttpGet]
        [HttpGet("{isDeleted:bool?}")]
        public IActionResult Get(bool isDeleted)
        {
            var tests = _vesselRepository.FindByInclude(x => x.IsDeleted == isDeleted, x => x.Operator, x => x.Fleet)
                .OrderByDescending(x => x.Id).ToList();
            var testsDto = _mapper.Map<IEnumerable<VesselDto>>(tests);

            return Ok(testsDto);
        }
        [AllowAnonymous]
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            if (id <= 0) return BadRequest();
            var test = _vesselRepository.FindByInclude(x => x.Id == id, x => x.Fleet).FirstOrDefault();
            var VesselDto = _mapper.Map<VesselDto>(test);
            return Ok(VesselDto);
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Post([FromBody] VesselDto VesselDto)
        {
            if (!ModelState.IsValid) return new UnprocessableEntityObjectResult(ModelState);
            //VesselDto.Id = 0;
            var test = _mapper.Map<Vessel>(VesselDto);
            /* var validate = _vesselRepository.Duplicate(test);
             if (!string.IsNullOrEmpty(validate))
             {
                 ModelState.AddModelError("Message", validate);
                 return BadRequest(ModelState);
             }*/

            if (VesselDto.Id == 0)
                _vesselRepository.Add(test);
            else
                _vesselRepository.Update(test);

            if (_uow.Save() <= 0) throw new Exception("Creating Test failed on save.");
            return Ok(test.Id);
        }
        [AllowAnonymous]
        [HttpPut]
        public IActionResult Put(VesselDto VesselDto)
        {
            if (VesselDto.Id <= 0) return BadRequest();

            if (!ModelState.IsValid) return new UnprocessableEntityObjectResult(ModelState);

            var test = _mapper.Map<Vessel>(VesselDto);
            var validate = _vesselRepository.Duplicate(test);
            if (!string.IsNullOrEmpty(validate))
            {
                ModelState.AddModelError("Message", validate);
                return BadRequest(ModelState);
            }

            /* Added by Vipul for effective Date on 14-10-2019 */
            Delete(test.Id);
            test.Id = 0;
            _vesselRepository.Add(test);

            if (_uow.Save() <= 0) throw new Exception("Updating Test failed on save.");
            return Ok(test.Id);
        }
        [AllowAnonymous]
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var record = _vesselRepository.Find(id);

            if (record == null)
                return NotFound();

            _vesselRepository.Delete(record);
            _uow.Save();

            return Ok();
        }

        [HttpGet]
        [Route("GetVesselDropDown")]
        public IActionResult GetVesselDropDown()
        {
            return Ok(_vesselRepository.GetVesselDropDown());
        }

        
        [HttpGet]
        [Route("GetVesselbyOperator/{id}")]
        public IActionResult GetVesselbyOperator(int id)
        {

            var list = _vesselRepository.FindByInclude(x => x.Operator_id == id)
                    .OrderByDescending(x => x.Id).ToList();

            return Ok(list);


        }

        [HttpGet]
        [Route("getVesselDetails/{id}")]
        public IActionResult getVesselDetails(int id)
        {
            var test = _vesselRepository.Find(id);
            var VesselDto = _mapper.Map<VesselDto>(test);
            return Ok(VesselDto);

        }
    }
}
