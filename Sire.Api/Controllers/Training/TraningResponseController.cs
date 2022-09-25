using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sire.Common.UnitOfWork;
using Sire.Data.Dto.Master;
using Sire.Data.Dto.Training;
using Sire.Data.Entities.Training;
using Sire.Domain.Context;
using Sire.Helper;
using Sire.Respository.Master;
using Sire.Respository.Training;
using System.Collections.Generic;
using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using System.Security.Cryptography;

namespace Sire.Api.Controllers.Training
{
    [Route("api/[controller]")]
    [ApiController]
    public class TraningResponseController : ControllerBase
    {
        private readonly IJwtTokenAccesser _jwtTokenAccesser;
        private readonly IMapper _mapper;
        private readonly ITraningResponseRepository _traningResponseRepository;
        private readonly IUnitOfWork<SireContext> _uow;
        private readonly IQuetionSectionRepository _quetionSectionRepository;

        public TraningResponseController(ITraningResponseRepository testRepository,
            IUnitOfWork<SireContext> uow, IMapper mapper,
             IQuetionSectionRepository quetionSectionRepository,
            IJwtTokenAccesser jwtTokenAccesser)
        {
            _traningResponseRepository = testRepository;
            _uow = uow;
            _mapper = mapper;
            _jwtTokenAccesser = jwtTokenAccesser;
            _quetionSectionRepository = quetionSectionRepository;
        }

        [AllowAnonymous]
        [HttpGet("GetTriningResponseByTraning/{traningId}")]
        public IActionResult GetTriningResponseByTraning(int? traningId)
        {
            var tests = _traningResponseRepository.FindBy(x => x.Training_Id == traningId).OrderByDescending(x => x.Id).ToList();

            var testsDto = _mapper.Map<IEnumerable<TraningResponseDto>>(tests);

            return Ok(testsDto);
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Post([FromBody] TraningResponseDto traningResponseDtos)
        {
            if (!ModelState.IsValid) return new UnprocessableEntityObjectResult(ModelState);
            var test = _mapper.Map<TraningResponse>(traningResponseDtos);

            var exit = _uow.Context.TraningResponse.Where(x => x.Training_Id == traningResponseDtos.Training_Id && x.Question_Id == traningResponseDtos.Question_Id && x.Trainee_Id == traningResponseDtos.Trainee_Id).FirstOrDefault();

            if (exit == null)
                _traningResponseRepository.Add(test);
           
            if (_uow.Save() <= 0) throw new Exception("Saving Assesor Reviewer failed on save.");
            return Ok(0);
        }

        [AllowAnonymous]
        [HttpGet("GetTriningResponseByTraningByUser/{traningId}/{quetionId}/{userId}")]
        public IActionResult GetTriningResponseByTraningByUser(int? traningId, int? quetionId, int? userId)
        {
            var tests = _traningResponseRepository.FindBy(x => x.Trainee_Id == userId && x.Training_Id == traningId  &&  x.Question_Id == quetionId).OrderByDescending(x => x.Id).ToList();

            var testsDto = _mapper.Map<IEnumerable<TraningResponseDto>>(tests);

            return Ok(testsDto);
        }


        [AllowAnonymous]
        [HttpPost("GetTraningTaskByQuetion")]
        public IActionResult GetTraningTaskByQuetion(TraningResponseDto obj)
        {
            //var tests = _traningResponseRepository.FindBy(x => x.Trainee_Id == userId && x.Training_Id == traningId && x.Question_Id == quetionId).OrderByDescending(x => x.Id).ToList();
            var Question = _uow.Context.Question.Where(x => x.Id == obj.Question_Id).FirstOrDefault();
            string questionText = Question.Chapter + "." + Question.Section + "." + Question.Question_Number;
            var data = _uow.Context.Training_Task.Where(x => x.Wbs_Number == questionText).FirstOrDefault();
            var ResponseData = _uow.Context.TraningResponse.Where(x => x.Question_Id == obj.Question_Id && x.Trainee_Id == obj.Trainee_Id && x.Training_Id == obj.Training_Id ).FirstOrDefault();

            var testsDto = _mapper.Map<Training_TaskDto>(data);
            if(ResponseData != null)
            {
                testsDto.IsResponse = true;
            }
            return Ok(testsDto);
        }
    }
}