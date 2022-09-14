using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sire.Common.UnitOfWork;
using Sire.Data.Dto.Question;
using Sire.Domain.Context;
using Sire.Helper;
using Sire.Respository.Question;
using System.Collections.Generic;
using System.Linq;

namespace Sire.Api.Controllers.Question
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionResponseController : Controller
    {
        private readonly IJwtTokenAccesser _jwtTokenAccesser;
        private readonly IMapper _mapper;
        private readonly IQuestionResponseRepository _questionResponseRepository;
        private readonly IUnitOfWork<SireContext> _uow;

        public QuestionResponseController(IQuestionResponseRepository testRepository,
            IUnitOfWork<SireContext> uow, IMapper mapper,
            IJwtTokenAccesser jwtTokenAccesser)
        {
            _questionResponseRepository = testRepository;
            _uow = uow;
            _mapper = mapper;
            _jwtTokenAccesser = jwtTokenAccesser;
        }

        [AllowAnonymous]
        [HttpGet("GetQuestion")]

        public IActionResult Get()
        {
            var tests = _questionResponseRepository.All.OrderByDescending(x => x.Id).ToList();

            var testsDto = _mapper.Map<IEnumerable<QuestionResponseDto>>(tests);

            return Ok(tests);
        }

        [AllowAnonymous]
        [HttpGet("GetResponse")]
        public IActionResult GetResponse()
        {

            var ResponseValue = _questionResponseRepository.AllIncluding().ToList();

            var QuestionResponseDto = _mapper.Map<List<QuestionResponseDto>>(ResponseValue);
            return Ok(QuestionResponseDto);
        }

    }
}
