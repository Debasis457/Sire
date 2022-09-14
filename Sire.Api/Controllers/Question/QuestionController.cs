using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sire.Common.UnitOfWork;
using Sire.Data.Dto.Operator;
using Sire.Data.Dto.Question;
using Sire.Data.Dto.Training;
using Sire.Domain.Context;
using Sire.Helper;
using Sire.Respository.Question;
using Sire.Respository.Training;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sire.Api.Controllers.Question
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : Controller
    {
        private readonly IJwtTokenAccesser _jwtTokenAccesser;
        private readonly IMapper _mapper;
        private readonly IQuestionRepository _questionRepository;
        private readonly IQuestionResponseRepository _questionResponseRepository;
        private readonly IUnitOfWork<SireContext> _uow;

        public QuestionController(IQuestionRepository testRepository, IQuestionResponseRepository testResponseRepository,
            IUnitOfWork<SireContext> uow, IMapper mapper,
            IJwtTokenAccesser jwtTokenAccesser)
        {
            _questionRepository = testRepository;
            _questionResponseRepository = testResponseRepository;
            _uow = uow;
            _mapper = mapper;
            _jwtTokenAccesser = jwtTokenAccesser;
        }

        [AllowAnonymous]
        [HttpGet("GetQuestion")]

        public IActionResult Get()
        {
            var tests = _questionRepository.All.OrderByDescending(x => x.Id).ToList();

            var testsDto = _mapper.Map<IEnumerable<QuestionDto>>(tests);


            return Ok(testsDto);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            if (id <= 0) return BadRequest();
            var test = _questionRepository.Find(id);           
            var QuestionDto = _mapper.Map<QuestionDto>(test);
          
            return Ok(QuestionDto);
        }


        [AllowAnonymous]
        [HttpGet("GetResponse")]
        public IActionResult GetResponse()
        {
           
            var ResponseValue = _questionResponseRepository.AllIncluding().ToList();

            var QuestionResponseDto = _mapper.Map<List<QuestionResponseDto>>(ResponseValue);
            return Ok(QuestionResponseDto);
        }

        [AllowAnonymous]
        [HttpPut]
        public IActionResult Put(QuestionDto QuestionDto)
        {
            if (QuestionDto.Id <= 0) return BadRequest();

            if (!ModelState.IsValid) return new UnprocessableEntityObjectResult(ModelState);

            var test = _mapper.Map<Sire.Data.Entities.Question.Question>(QuestionDto);
            var validate = _questionRepository.Duplicate(test);
            if (!string.IsNullOrEmpty(validate))
            {
                ModelState.AddModelError("Message", validate);
                return BadRequest(ModelState);
            }
            return Ok(test.Id);
        }

        [AllowAnonymous]
        [HttpGet("GetQuestionBySection/{id}")]
        public IActionResult GetQuestionBySection(int? id)
        {
            var tests = _questionRepository.FindBy(x=>x.Section == id).OrderByDescending(x => x.Id).ToList();
            var testsDto = _mapper.Map<IEnumerable<QuestionDto>>(tests);

            return Ok(testsDto);
        }

        [AllowAnonymous]
        [HttpGet("GetQuestionById/{Id}")]
        public IActionResult GetQuestionById(int? Id)
        {
            var tests = _questionRepository.FindBy(x => x.Id == Id).OrderByDescending(x => x.Id).ToList();
            var testsDto = _mapper.Map<IEnumerable<QuestionDto>>(tests);

            return Ok(testsDto);
        }
    }
}