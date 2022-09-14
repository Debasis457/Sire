using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Sire.Common.UnitOfWork;
using Sire.Data.Dto.Operator;
using Sire.Domain.Context;
using Sire.Helper;
using Sire.Respository.Operator;
using System.Collections.Generic;
using System;
using Sire.Respository.Training;
using Microsoft.AspNetCore.Authorization;
using Sire.Data.Dto.Training;
using System.Linq;

namespace Sire.Api.Controllers.Training
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainingController : ControllerBase
    {
            private readonly IJwtTokenAccesser _jwtTokenAccesser;
            private readonly IMapper _mapper;
            private readonly ITrainingRepository _trainingRepository;
            private readonly IUnitOfWork<SireContext> _uow;

            public TrainingController(ITrainingRepository testRepository,
                IUnitOfWork<SireContext> uow, IMapper mapper,
                IJwtTokenAccesser jwtTokenAccesser)
            {
            _trainingRepository = testRepository;
                _uow = uow;
                _mapper = mapper;
                _jwtTokenAccesser = jwtTokenAccesser;
            }

            [AllowAnonymous]
            [HttpGet]
            [HttpGet("{isDeleted:bool?}")]
            public IActionResult Get(bool isDeleted)
            {
                var tests = _trainingRepository.FindByInclude(x => x.IsDeleted == isDeleted)
                    .OrderByDescending(x => x.Id).ToList();
                var testsDto = _mapper.Map<IEnumerable<TrainingDto>>(tests);

                return Ok(testsDto);
            }
            [AllowAnonymous]
            [HttpGet("{id}")]
            public IActionResult Get(int id)
            {
                if (id <= 0) return BadRequest();
                var test = _trainingRepository.Find(id);
                var TrainingDto = _mapper.Map<TrainingDto>(test);
                return Ok(TrainingDto);
            }

            [AllowAnonymous]
            [HttpPost]
            public IActionResult Post([FromBody] TrainingDto TrainingDto)
            {
                if (!ModelState.IsValid) return new UnprocessableEntityObjectResult(ModelState);
                var test = _mapper.Map<Sire.Data.Entities.Training.Training>(TrainingDto);
                /*var validate = _trainingRepository.Duplicate(test);
                if (!string.IsNullOrEmpty(validate))
                {
                    ModelState.AddModelError("Message", validate);
                    return BadRequest(ModelState);
                }*/

                if (TrainingDto.Id == 0)
                _trainingRepository.Add(test);
                else
                _trainingRepository.Update(test);

                if (_uow.Save() <= 0) throw new Exception("Creating Test failed on save.");
                return Ok(test.Id);
            }
            [AllowAnonymous]
            [HttpPut]
            public IActionResult Put(TrainingDto TrainingDto)
            {
                if (TrainingDto.Id <= 0) return BadRequest();

                if (!ModelState.IsValid) return new UnprocessableEntityObjectResult(ModelState);

                var test = _mapper.Map<Sire.Data.Entities.Training.Training>(TrainingDto);
                var validate = _trainingRepository.Duplicate(test);
                if (!string.IsNullOrEmpty(validate))
                {
                    ModelState.AddModelError("Message", validate);
                    return BadRequest(ModelState);
                }

                /* Added by Vipul for effective Date on 14-10-2019 */
                Delete(test.Id);
                test.Id = 0;
            _trainingRepository.Add(test);

                if (_uow.Save() <= 0) throw new Exception("Updating Test failed on save.");
                return Ok(test.Id);
            }
            [AllowAnonymous]
            [HttpDelete("{id}")]
            public ActionResult Delete(int id)
            {
                var record = _trainingRepository.Find(id);

                if (record == null)
                    return NotFound();

            _trainingRepository.Delete(record);
                _uow.Save();

                return Ok();
            }
/*
            [HttpGet]
            [Route("GetTrainingDropDown")]
            public IActionResult GetTrainingDropDown()
            {
                return Ok(_trainingRepository.GetTrainingDropDown());
            }
*/    }
}
