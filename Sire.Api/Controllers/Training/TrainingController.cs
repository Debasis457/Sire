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
using Sire.Respository.Question;
using Sire.Data.Dto.Question;

namespace Sire.Api.Controllers.Training
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainingController : ControllerBase
    {
            private readonly IJwtTokenAccesser _jwtTokenAccesser;
            private readonly IMapper _mapper;
            private readonly ITrainingRepository _trainingRepository;
            private readonly IQuestionRepository _questionRepository;
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
        [HttpGet]
        [Route("GetDifference/{id}")]
        public IActionResult GetDifference(int id)
        {
            var QuestionCount = (from quetion in _uow.Context.Question.Where(x => x.Id != null)
                                 select new QuestionDto
                                 {
                                     Id = quetion.Id,
                                 }
                                 
                                 
                                 ).Count();

            var ResponseCount = (from response in _uow.Context.TraningResponse.Where(x => x.Trainee_Id == id)
                                 select new TraningResponseDto
                                 {
                                     Id = response.Id,
                                 }).Count();
          
            var Diff = QuestionCount - ResponseCount;
            return Ok(Diff);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("GetLastTrainingID/{id}")]
        public IActionResult GetLastTrainingID(int id)
        {
            var LastId = (from training in _uow.Context.Training.Where(x => x.Operator_id == id)
                                 select new TrainingDto
                                 {
                                     Id = training.Id,
                                 }


                                 ).LastOrDefault();

            
            return Ok(LastId);
        }

        [AllowAnonymous]
        [HttpGet("GetStatus/{id}")]
        public IActionResult GetStatus(int id)
        {
            var tests = _trainingRepository.FindByInclude(x => x.Operator_id == id)
                .OrderByDescending(x => x.Id).ToList();
            var testsDto = _mapper.Map<IEnumerable<TrainingDto>>(tests);
            /*var user = (from sec in _uow.Context.Training.Where(x => x.Id == id)
                        join sub in _uow.Context.User on sec.Id equals sub.
*/

            foreach (var item in testsDto)
            {
                /*item.Operator_id = _userRepository.FindByInclude(x => x.Id == item.Operator_id, x => x.User).FirstOrDefault();
*/

                var QuestionCount = (from quetion in _uow.Context.Question.Where(x => x.Id != null)
                                     select new QuestionDto
                                     {
                                         Id = quetion.Id,
                                     }).Count();
                var ResponseCount = (from response in _uow.Context.TraningResponse.Where(x => x.Trainee_Id == id)
                                     select new TraningResponseDto
                                     {
                                         Id = response.Id,
                                     }).Count();

                item.ResTotal = ResponseCount;
                item.Total = QuestionCount;

                var Diff = QuestionCount - ResponseCount;

                item.Difference = Diff;

            }


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
            //var validate = _trainingRepository.Duplicate(test);
            //if (!string.IsNullOrEmpty(validate))
            //{
            //    ModelState.AddModelError("Message", validate);
            //    return BadRequest(ModelState);
            //}

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
