using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sire.Common.UnitOfWork;
using Sire.Data.Dto.Operator;
using Sire.Domain.Context;
using Sire.Helper;
using Sire.Respository.Operator;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sire.Api.Controllers.Operator
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperatorController : ControllerBase
    {
        private readonly IJwtTokenAccesser _jwtTokenAccesser;
        private readonly IMapper _mapper;
        private readonly IOperatorRepository _operatorRepository;
        private readonly IUnitOfWork<SireContext> _uow;

        public OperatorController(IOperatorRepository testRepository,
            IUnitOfWork<SireContext> uow, IMapper mapper,
            IJwtTokenAccesser jwtTokenAccesser)
        {
            _operatorRepository = testRepository;
            _uow = uow;
            _mapper = mapper;
            _jwtTokenAccesser = jwtTokenAccesser;
        }

        [AllowAnonymous]
        [HttpGet]
        [HttpGet("{isDeleted:bool?}")]
        public IActionResult Get(bool isDeleted)
        {
            var tests = _operatorRepository.FindByInclude(x => x.IsDeleted == isDeleted)
                .OrderByDescending(x => x.Id).ToList();
            var testsDto = _mapper.Map<IEnumerable<OperatorDto>>(tests);

            return Ok(testsDto);
        }
        [AllowAnonymous]
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            if (id <= 0) return BadRequest();
            var test = _operatorRepository.Find(id);
            var OperatorDto = _mapper.Map<OperatorDto>(test);
            return Ok(OperatorDto);
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Post([FromBody] OperatorDto OperatorDto)
        {
            if (!ModelState.IsValid) return new UnprocessableEntityObjectResult(ModelState);
            var test = _mapper.Map<Sire.Data.Entities.Operator.Operator>(OperatorDto);
            var validate = _operatorRepository.Duplicate(test);
            if (!string.IsNullOrEmpty(validate))
            {
                ModelState.AddModelError("Message", validate);
                return BadRequest(ModelState);
            }

            if (OperatorDto.Id == 0)
                _operatorRepository.Add(test);
            else
                _operatorRepository.Update(test);

            if (_uow.Save() <= 0) throw new Exception("Creating Test failed on save.");
            return Ok(test.Id);
        }
        [AllowAnonymous]
        [HttpPut]
        public IActionResult Put(OperatorDto OperatorDto)
        {
            if (OperatorDto.Id <= 0) return BadRequest();

            if (!ModelState.IsValid) return new UnprocessableEntityObjectResult(ModelState);

            var test = _mapper.Map<Sire.Data.Entities.Operator.Operator>(OperatorDto);
            var validate = _operatorRepository.Duplicate(test);
            if (!string.IsNullOrEmpty(validate))
            {
                ModelState.AddModelError("Message", validate);
                return BadRequest(ModelState);
            }

            /* Added by Vipul for effective Date on 14-10-2019 */
            Delete(test.Id);
            test.Id = 0;
            _operatorRepository.Add(test);

            if (_uow.Save() <= 0) throw new Exception("Updating Test failed on save.");
            return Ok(test.Id);
        }
        [AllowAnonymous]
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var record = _operatorRepository.Find(id);

            if (record == null)
                return NotFound();

            _operatorRepository.Delete(record);
            _uow.Save();

            return Ok();
        }

        [HttpGet]
        [Route("GetOperatorDropDown")]
        public IActionResult GetOperatorDropDown()
        {
            return Ok(_operatorRepository.GetOperatorDropDown());
        }
    }
}
