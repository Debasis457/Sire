using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sire.Common.UnitOfWork;
using Sire.Data.Dto.Inspection;
using Sire.Data.Dto.Question;
using Sire.Data.Entities.Inspection;
using Sire.Domain.Context;
using Sire.Helper;
using Sire.Respository.Inspection;
using Sire.Respository.Question;
using System;
using System.Collections.Generic;

namespace Sire.Api.Controllers.Inspection
{
    [Route("api/[controller]")]
    public class InspectionResponseController : ControllerBase
    {
        private readonly IJwtTokenAccesser _jwtTokenAccesser;
        private readonly IMapper _mapper;
        private readonly IInspectionResponseRepository _inspection_QuestionRepository;
        private readonly IQuestionRepository _iQuestionRepository;
        private readonly IUnitOfWork<SireContext> _uow;
       

        public InspectionResponseController(
            IInspectionResponseRepository inspection_QuestionRepository,
            IQuestionRepository iQuestionRepository,
            IUnitOfWork<SireContext> uow, IMapper mapper,
        
            IJwtTokenAccesser jwtTokenAccesser)
        {
            _inspection_QuestionRepository = inspection_QuestionRepository;
            _iQuestionRepository = iQuestionRepository;
            _uow = uow;
            _mapper = mapper;
            _jwtTokenAccesser = jwtTokenAccesser;
          
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            if (id <= 0) return BadRequest();
            var test = _iQuestionRepository.Find(id);
            var QuestionDto = _mapper.Map<QuestionDto>(test);
            return Ok(QuestionDto);
        }


        [AllowAnonymous]
        [HttpPost]
        public IActionResult Post([FromBody] List<InspectionResponseDto> inspection_QuestionDtos)
        {
            if (!ModelState.IsValid) return new UnprocessableEntityObjectResult(ModelState);
            var test = _mapper.Map<List<InspectionResponse>>(inspection_QuestionDtos);

            foreach (var item in test)
            {
                if (item.Id == 0)
                    _inspection_QuestionRepository.Add(item);
                else
                    _inspection_QuestionRepository.Update(item);
            }

            if (_uow.Save() <= 0) throw new Exception("Saving Assesor Reviewer failed on save.");
            return Ok(0);
        }
      



    }
}
