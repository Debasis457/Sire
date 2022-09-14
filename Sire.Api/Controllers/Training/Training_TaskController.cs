using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Sire.Common.UnitOfWork;
using Sire.Data.Dto.Training;
using Sire.Domain.Context;
using Sire.Helper;
using Sire.Respository.Training;
using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Authorization;
using Sire.Data.Entities.Training;
using System.Linq;

namespace Sire.Api.Controllers.Training
{
    [Route("api/[controller]")]
    [ApiController]
    public class Training_TaskController : ControllerBase
    {
        private readonly IJwtTokenAccesser _jwtTokenAccesser;
        private readonly IMapper _mapper;
        private readonly ITraining_TaskRepository _training_TaskRepository;
        private readonly IUnitOfWork<SireContext> _uow;

        public Training_TaskController(ITraining_TaskRepository testRepository,
            IUnitOfWork<SireContext> uow, IMapper mapper,
            IJwtTokenAccesser jwtTokenAccesser)
        {
            _training_TaskRepository = testRepository;
            _uow = uow;
            _mapper = mapper;
            _jwtTokenAccesser = jwtTokenAccesser;
        }

        [AllowAnonymous]
        [HttpGet]
        [HttpGet("{isDeleted:bool?}")]
        public IActionResult Get(bool isDeleted)
        {
            var tests = _training_TaskRepository.FindByInclude(x => x.IsDeleted == isDeleted)
                .OrderByDescending(x => x.Id).ToList();
            var testsDto = _mapper.Map<IEnumerable<Training_Task>>(tests);

            return Ok(testsDto);
        }
        [AllowAnonymous]
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            if (id <= 0) return BadRequest();
            var test = _training_TaskRepository.Find(id);
            var TrainingTaskDto = _mapper.Map<Training_Task>(test);
            return Ok(TrainingTaskDto);
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Post([FromBody] Training_TaskDto Training_TaskDto)
        {
            if (!ModelState.IsValid) return new UnprocessableEntityObjectResult(ModelState);
            var test = _mapper.Map<Training_Task>(Training_TaskDto);
            var validate = _training_TaskRepository.Duplicate(test);
            if (!string.IsNullOrEmpty(validate))
            {
                ModelState.AddModelError("Message", validate);
                return BadRequest(ModelState);
            }
            if (Training_TaskDto.Id == 0)
                _training_TaskRepository.Add(test);
            else
                _training_TaskRepository.Update(test);

            if (_uow.Save() <= 0) throw new Exception("Creating Test failed on save.");
            return Ok(test.Id);
        }
        [AllowAnonymous]
        [HttpPut]
        public IActionResult Put(Training_TaskDto Training_TaskDto)
        {
            if (Training_TaskDto.Id <= 0) return BadRequest();

            if (!ModelState.IsValid) return new UnprocessableEntityObjectResult(ModelState);

            var test = _mapper.Map<Training_Task>(Training_TaskDto);
            var validate = _training_TaskRepository.Duplicate(test);
            if (!string.IsNullOrEmpty(validate))
            {
                ModelState.AddModelError("Message", validate);
                return BadRequest(ModelState);
            }


            Delete(test.Id);
            test.Id = 0;
            _training_TaskRepository.Add(test);

            if (_uow.Save() <= 0) throw new Exception("Updating Test failed on save.");
            return Ok(test.Id);
        }
        [AllowAnonymous]
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var record = _training_TaskRepository.Find(id);

            if (record == null)
                return NotFound();

            _training_TaskRepository.Delete(record);
            _uow.Save();

            return Ok();
        }
    }
}
