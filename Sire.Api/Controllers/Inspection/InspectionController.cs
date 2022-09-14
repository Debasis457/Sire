using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sire.Common.UnitOfWork;
using Sire.Data.Dto.Inspection;
using Sire.Domain.Context;
using Sire.Helper;
using Sire.Respository.Inspection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sire.Api.Controllers.Inspection
{
    [Route("api/[controller]")]
    [ApiController]
    public class InspectionController : ControllerBase
    {
        private readonly IJwtTokenAccesser _jwtTokenAccesser;
        private readonly IMapper _mapper;
        private readonly IInspectionRepository _InspectionRepository;
        private readonly IUnitOfWork<SireContext> _uow;

        public InspectionController(IInspectionRepository testRepository,
            IUnitOfWork<SireContext> uow, IMapper mapper,
            IJwtTokenAccesser jwtTokenAccesser)
        {
            _InspectionRepository = testRepository;
            _uow = uow;
            _mapper = mapper;
            _jwtTokenAccesser = jwtTokenAccesser;
        }

        [AllowAnonymous]
        [HttpGet]
        [HttpGet("{isDeleted:bool?}")]
        public IActionResult Get(bool isDeleted)
        {
            var tests = _InspectionRepository.FindByInclude(x => x.IsDeleted == isDeleted)
                .OrderByDescending(x => x.Id).ToList();
            var testsDto = _mapper.Map<IEnumerable<InspectionDto>>(tests);

            return Ok(testsDto);
        }
        [AllowAnonymous]
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            if (id <= 0) return BadRequest();
            var test = _InspectionRepository.Find(id);
            var InspectionDto = _mapper.Map<InspectionDto>(test);
            return Ok(InspectionDto);
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Post([FromBody] InspectionDto InspectionDto)
        {
            if (!ModelState.IsValid) return new UnprocessableEntityObjectResult(ModelState);
            var test = _mapper.Map<Sire.Data.Entities.Inspection.Inspection>(InspectionDto);
            //var validate = _InspectionRepository.Duplicate(test);
            //if (!string.IsNullOrEmpty(validate))
            //{
            //    ModelState.AddModelError("Message", validate);
            //    return BadRequest(ModelState);
            //}

            if (InspectionDto.Id == 0)
                _InspectionRepository.Add(test);
            else
                _InspectionRepository.Update(test);

            if (_uow.Save() <= 0) throw new Exception("Creating Test failed on save.");
            return Ok(test.Id);
        }
        [AllowAnonymous]
        [HttpPut]
        public IActionResult Put(InspectionDto InspectionDto)
        {
            if (InspectionDto.Id <= 0) return BadRequest();

            if (!ModelState.IsValid) return new UnprocessableEntityObjectResult(ModelState);

            var test = _mapper.Map<Sire.Data.Entities.Inspection.Inspection>(InspectionDto);
            //var validate = _InspectionRepository.Duplicate(test);
            //if (!string.IsNullOrEmpty(validate))
            //{
            //    ModelState.AddModelError("Message", validate);
            //    return BadRequest(ModelState);
            //}

            /* Added by Vipul for effective Date on 14-10-2019 */
            Delete(test.Id);
            test.Id = 0;
            _InspectionRepository.Add(test);

            if (_uow.Save() <= 0) throw new Exception("Updating Test failed on save.");
            return Ok(test.Id);
        }
        [AllowAnonymous]
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var record = _InspectionRepository.Find(id);

            if (record == null)
                return NotFound();

            _InspectionRepository.Delete(record);
            _uow.Save();

            return Ok();
        }

        //[HttpGet]
        //[Route("GetInspectionDropDown")]
        //public IActionResult GetInspectionDropDown()
        //{
        //    return Ok(_InspectionRepository.GetInspectionDropDown());
        //}
    }
}
