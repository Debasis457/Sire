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
using System.Linq;
using Sire.Data.Entities.Training;
using Sire.Data.Dto.ShipManagement;
using Sire.Data.Dto.Master;
using Sire.Data.Dto.Inspection;
using Sire.Data.Entities.Inspection;
using Sire.Respository.Master;

namespace Sire.Api.Controllers.Training
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainingQuestionController : ControllerBase
    {
        private readonly IJwtTokenAccesser _jwtTokenAccesser;
        private readonly IMapper _mapper;
        private readonly ITrainingQuestionRepository _trainingQuestionRepository;
        private readonly IUnitOfWork<SireContext> _uow;
        private readonly IQuetionSectionRepository _quetionSectionRepository;

        public TrainingQuestionController(ITrainingQuestionRepository testRepository,
            IUnitOfWork<SireContext> uow, IMapper mapper,
             IQuetionSectionRepository quetionSectionRepository,
            IJwtTokenAccesser jwtTokenAccesser)
        {
            _trainingQuestionRepository = testRepository;
            _uow = uow;
            _mapper = mapper;
            _jwtTokenAccesser = jwtTokenAccesser;
            _quetionSectionRepository = quetionSectionRepository;
        }

        [AllowAnonymous]
        [HttpGet("GetTriningQuestion")]
        public IActionResult Get()
        {
            var tests = _trainingQuestionRepository.All.OrderByDescending(x => x.Id).ToList();

            var testsDto = _mapper.Map<IEnumerable<Training_QuestionDto>>(tests);

            return Ok(testsDto);
        }


        [AllowAnonymous]
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            if (id <= 0) return BadRequest();
            var test = _trainingQuestionRepository.Find(id);
            var TrainingDto = _mapper.Map<TrainingDto>(test);
            return Ok(TrainingDto);
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Post([FromBody] List<Training_QuestionDto> training_QuestionDto)
        {
            if (!ModelState.IsValid) return new UnprocessableEntityObjectResult(ModelState);
            var test = _mapper.Map<List<Training_Question>>(training_QuestionDto);

            foreach (var item in test)
            {
                if (item.Id == 0)
                    _trainingQuestionRepository.Add(item);
                else
                    _trainingQuestionRepository.Update(item);
            }

            if (_uow.Save() <= 0) throw new Exception("Saving Assesor Reviewer failed on save.");
            return Ok(0);
        }
       

        [AllowAnonymous]
        [HttpGet]
        public IActionResult GetSectionList(bool isDeleted)
        {
            var tests = _quetionSectionRepository.FindByInclude(x => x.Id > 0, x => x.QuetionSubSection)
                .ToList();
            var testsDto = _mapper.Map<IEnumerable<QuetionSectionDto>>(tests);
            foreach (var item in testsDto)
            {
                var count = (from sec in _uow.Context.QuetionSection.Where(x => x.Id == item.Id)
                             join sub in _uow.Context.QuetionSubSection on sec.Id equals sub.QuetionSectionId
                             join que in _uow.Context.Question on sub.Id equals que.Section
                             select new
                             {
                                 que.Id

                             }).ToList().Count;

                foreach (var subb in item.QuetionSubSection)
                {
                    subb.Total = (from sec in _uow.Context.QuetionSection.Where(x => x.Id == item.Id)
                                  join sub in _uow.Context.QuetionSubSection.Where(x => x.Id == subb.Id) on sec.Id equals sub.QuetionSectionId
                                  join que in _uow.Context.Question on sub.Id equals que.Section
                                  select new
                                  {
                                      que.Id

                                  }).ToList().Count;
                }
                item.Total = count;
            }
            return Ok(testsDto);
        }

    }
}

