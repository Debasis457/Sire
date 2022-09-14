using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Sire.Api.Controllers.Common;
using Sire.Common.UnitOfWork;
using Sire.Data.Dto.Master;
using Sire.Data.Entities.Master;
using Sire.Domain.Context;
using Sire.Helper;
using Sire.Respository.Master;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Sire.Api.Controllers.Master
{
    [Route("api/[controller]")]
    public class TestController : BaseController
    {
        private readonly IJwtTokenAccesser _jwtTokenAccesser;
        private readonly IMapper _mapper;
        private readonly ITestRepository _testRepository;
        private readonly IUnitOfWork<SireContext> _uow;

        public TestController(ITestRepository testRepository,
            IUnitOfWork<SireContext> uow, IMapper mapper,
            IJwtTokenAccesser jwtTokenAccesser)
        {
            _testRepository = testRepository;
            _uow = uow;
            _mapper = mapper;
            _jwtTokenAccesser = jwtTokenAccesser;
        }
        [AllowAnonymous]
        [HttpGet]
        [HttpGet("{isDeleted:bool?}")]
        public IActionResult Get(bool isDeleted)
        {
            var tests = _testRepository.FindByInclude(x => x.IsDeleted == isDeleted)
                .OrderByDescending(x => x.Id).ToList();
            var testsDto = _mapper.Map<IEnumerable<TestDto>>(tests);
            return Ok(testsDto);
        }
        [AllowAnonymous]
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            if (id <= 0) return BadRequest();
            var test = _testRepository.Find(id);
            var testDto = _mapper.Map<TestDto>(test);
            return Ok(testDto);
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Post([FromBody] TestDto testDto)
        {
            if (!ModelState.IsValid) return new UnprocessableEntityObjectResult(ModelState);
            testDto.Id = 0;
            var test = _mapper.Map<Test>(testDto);
            var validate = _testRepository.Duplicate(test);
            if (!string.IsNullOrEmpty(validate))
            {
                ModelState.AddModelError("Message", validate);
                return BadRequest(ModelState);
            }

            _testRepository.Add(test);
            if (_uow.Save() <= 0) throw new Exception("Creating Test failed on save.");
            return Ok(test.Id);
        }
        [AllowAnonymous]
        [HttpPut]
        public IActionResult Put([FromBody] TestDto testDto)
        {
            if (testDto.Id <= 0) return BadRequest();

            if (!ModelState.IsValid) return new UnprocessableEntityObjectResult(ModelState);

            var test = _mapper.Map<Test>(testDto);
            var validate = _testRepository.Duplicate(test);
            if (!string.IsNullOrEmpty(validate))
            {
                ModelState.AddModelError("Message", validate);
                return BadRequest(ModelState);
            }

            /* Added by Vipul for effective Date on 14-10-2019 */
            Delete(test.Id);
            test.Id = 0;
            _testRepository.Add(test);

            if (_uow.Save() <= 0) throw new Exception("Updating Test failed on save.");
            return Ok(test.Id);
        }
        [AllowAnonymous]
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var record = _testRepository.Find(id);

            if (record == null)
                return NotFound();

            _testRepository.Delete(record);
            _uow.Save();

            return Ok();
        }
        [AllowAnonymous]
        [HttpPatch("{id}")]
        public ActionResult Active(int id)
        {
            var record = _testRepository.Find(id);

            if (record == null)
                return NotFound();

            var validate = _testRepository.Duplicate(record);
            if (!string.IsNullOrEmpty(validate))
            {
                ModelState.AddModelError("Message", validate);
                return BadRequest(ModelState);
            }

            _testRepository.Active(record);
            _uow.Save();

            return Ok();
        }

        [HttpGet]
        [Route("GetTestDropDown")]
        public IActionResult GetTestDropDown()
        {
            return Ok(_testRepository.GetTestDropDown());
        }

        [HttpGet]
        [Route("GetTestDropDownByTestGroup/{id}")]
        public IActionResult GetTestDropDownByTestGroup(int id)
        {
            return Ok(_testRepository.GetTestDropDownByTestGroup(id));
        }
    }
}