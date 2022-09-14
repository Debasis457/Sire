using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
    public class LicenseController : ControllerBase

    {
        private readonly IJwtTokenAccesser _jwtTokenAccesser;
        private readonly IMapper _mapper;
        private readonly ILicenseRepository _licenseRepository;
        private readonly IUnitOfWork<SireContext> _uow;

        public LicenseController(ILicenseRepository LicenseRepository,
            IUnitOfWork<SireContext> uow, IMapper mapper,
            IJwtTokenAccesser jwtTokenAccesser)
        {
            _licenseRepository = LicenseRepository;
            _uow = uow;
            _mapper = mapper;
            _jwtTokenAccesser = jwtTokenAccesser;
        }
        [AllowAnonymous]
        [HttpGet]
        [HttpGet("{isDeleted:bool?}")]
        public IActionResult Get(bool isDeleted)
        {
            var tests = _licenseRepository.FindByInclude(x => x.IsDeleted == isDeleted,x=>x.Vessel)
                .OrderByDescending(x => x.Id).ToList();
            var testsDto = _mapper.Map<IEnumerable<LicenseDto>>(tests);

            return Ok(testsDto);
        }
        [AllowAnonymous]
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            if (id <= 0) return BadRequest();
            var test = _licenseRepository.Find(id);
            var LicenseDto = _mapper.Map<LicenseDto>(test);
            return Ok(LicenseDto);
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Post([FromBody] LicenseDto LicenseDto)
        {
            if (!ModelState.IsValid) return new UnprocessableEntityObjectResult(ModelState);
            var test = _mapper.Map<License>(LicenseDto);

            if (LicenseDto.Id == 0)
                _licenseRepository.Add(test);
            else
                _licenseRepository.Update(test);
            if (_uow.Save() <= 0) throw new Exception("Creating Test failed on save.");
            return Ok(test.Id);
        }
        [AllowAnonymous]
        [HttpPut]
        public IActionResult Put(LicenseDto LicenseDto)
        {
            if (LicenseDto.Id <= 0) return BadRequest();

            if (!ModelState.IsValid) return new UnprocessableEntityObjectResult(ModelState);

            var test = _mapper.Map<License>(LicenseDto);
            //var validate = _licenseRepository.Duplicate(test);
            //if (!string.IsNullOrEmpty(validate))
            //{
            //    ModelState.AddModelError("Message", validate);
            //    return BadRequest(ModelState);
            //}

            /* Added by Vipul for effective Date on 14-10-2019 */
            Delete(test.Id);
            test.Id = 0;
            _licenseRepository.Add(test);

            if (_uow.Save() <= 0) throw new Exception("Updating Test failed on save.");
            return Ok(test.Id);
        }
        [AllowAnonymous]
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var record = _licenseRepository.Find(id);

            if (record == null)
                return NotFound();

            _licenseRepository.Delete(record);
            _uow.Save();


            return Ok();
        }



        //[HttpGet]
        //[Route("GetLicenseDropDown")]
        //public IActionResult GetLicenseDropDown()
        //{
        //    return Ok(_licenseRepository.GetLicenseDropDown());
        //}
    }
}
