﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.Expressions;
using Sire.Common.UnitOfWork;
using Sire.Data.Dto.Inspection;
using Sire.Data.Dto.Master;
using Sire.Data.Dto.Operator;
using Sire.Data.Dto.ShipManagement;
using Sire.Data.Entities.Inspection;
using Sire.Data.Entities.Master;
using Sire.Domain.Context;
using Sire.Helper;
using Sire.Respository.Inspection;
using Sire.Respository.Master;
using Sire.Respository.Operator;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Sire.Api.Controllers
{
    [Route("api/[controller]")]
    public class AssesorReviewerController : ControllerBase
    {
        private readonly IJwtTokenAccesser _jwtTokenAccesser;
        private readonly IMapper _mapper;
        private readonly IInspection_QuestionRepository _inspection_QuestionRepository;
        private readonly IUnitOfWork<SireContext> _uow;
        private readonly IQuetionSectionRepository _quetionSectionRepository;

        public AssesorReviewerController(
            IInspection_QuestionRepository inspection_QuestionRepository,
            IUnitOfWork<SireContext> uow, IMapper mapper,
            IQuetionSectionRepository quetionSectionRepository,
            IJwtTokenAccesser jwtTokenAccesser)
        {
            _inspection_QuestionRepository = inspection_QuestionRepository;
            _uow = uow;
            _mapper = mapper;
            _jwtTokenAccesser = jwtTokenAccesser;
            _quetionSectionRepository = quetionSectionRepository;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Post([FromBody] List<Inspection_QuestionDto> inspection_QuestionDtos)
        {
            if (!ModelState.IsValid) return new UnprocessableEntityObjectResult(ModelState);
            //var test = _mapper.Map<List<Inspection_Question>>(inspection_QuestionDtos);

            foreach (var item in inspection_QuestionDtos)
            {
                var exits = _uow.Context.Inspection_Question.Where(x => x.inspection_id == item.Inspection_Id && x.question_id == item.Question_Id).FirstOrDefault();

                if (item.IsAssesor == true)
                {
                    if (exits != null)
                    {
                        exits.assessor_id = item.Assessor_Id;
                        _inspection_QuestionRepository.Update(exits);
                    }
                    else
                    {
                        var test = _mapper.Map<Inspection_Question>(item);
                        _inspection_QuestionRepository.Add(test);
                    }
                }
                else
                {

                    if (exits != null)
                    {
                        exits.reviewer_id = item.Reviewer_Id;
                        _inspection_QuestionRepository.Update(exits);
                    }
                    else
                    {
                        var test = _mapper.Map<Inspection_Question>(item);
                        _inspection_QuestionRepository.Add(test);
                    }
                }
                //if (item.Id == 0)
                //    _inspection_QuestionRepository.Add(item);
                //else
                //    _inspection_QuestionRepository.Update(item);
                //    _inspection_QuestionRepository.Update(item);
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
                                 join sub in _uow.Context.QuetionSubSection.Where(x=>x.Id == subb.Id) on sec.Id equals sub.QuetionSectionId
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

        [AllowAnonymous]
        [HttpGet("GetSectionListByInspectionId/{id}")]
        public IActionResult GetSectionListByInspectionId(int id)
        {
            var tests = _quetionSectionRepository.FindByInclude(x => x.Id > 0, x => x.QuetionSubSection)
                .ToList();
            var testsDto = _mapper.Map<IEnumerable<QuetionSectionDto>>(tests);
            foreach (var item in testsDto)
            {
                var count = (from sec in _uow.Context.QuetionSection.Where(x => x.Id == item.Id)
                             join sub in _uow.Context.QuetionSubSection on sec.Id equals sub.QuetionSectionId
                             join que in _uow.Context.Question on sub.Id equals que.Section
                             join ins in _uow.Context.Inspection_Question.Where(x => x.inspection_id == id) on que.Id equals ins.question_id
                             select new
                             {
                                 que.Id

                             }).ToList().Count;

                foreach (var subb in item.QuetionSubSection)
                {
                    subb.Total = (from sec in _uow.Context.QuetionSection.Where(x => x.Id == item.Id)
                                  join sub in _uow.Context.QuetionSubSection.Where(x => x.Id == subb.Id) on sec.Id equals sub.QuetionSectionId
                                  join que in _uow.Context.Question on sub.Id equals que.Section
                                  join ins in _uow.Context.Inspection_Question.Where(x => x.inspection_id == id) on que.Id equals ins.question_id
                                  select new
                                  {
                                      que.Id

                                  }).ToList().Count;
                }
                item.Total = count;
            }
            return Ok(testsDto);
        }

        [AllowAnonymous]
        [HttpGet("GetAssesordataByInspection/{id}/{sectionId}")]
        public IActionResult GetAssesordataByInspection(int? id, int? sectionId)
        {
            var data = (from quetion in _uow.Context.Question.Where(x => x.Section == sectionId)
                        join assque in _uow.Context.Inspection_Question.Where(x => x.inspection_id == id) on quetion.Id equals assque.question_id //into g
                        //from assque in g.DefaultIfEmpty()
                        join userrew in _uow.Context.User on assque.reviewer_id equals userrew.Id //into r
                        //from userrew in r.DefaultIfEmpty()
                        join userass in _uow.Context.User on assque.assessor_id equals userass.Id //into a
                        //from userass in a.DefaultIfEmpty()
                        select new Inspection_QuestionDto
                        {
                            Id = assque != null ? assque.Id : 0,
                            Reviewer_Id = assque != null ? assque.reviewer_id : 0,
                            Assessor_Id = assque != null ? assque.assessor_id : 0,
                            Inspection_Id = assque != null ? assque.inspection_id : 0,
                            Reviewer_Name = assque != null ? userrew.Full_Name : "",
                            Assessor_Name = assque != null ? userass.Full_Name : "",
                            Question_Id = quetion.Id,
                            Question_Text = quetion.Questions

                        }).ToList();

            return Ok(data);
        }

        [AllowAnonymous]
        [HttpGet("GetInspectionQuestion/{id}")]
        public IActionResult GetInspectionQuestion(int id)
        {
            var inspectionQuestion = _uow.Context.Inspection_Question.First(x => x.Id == id);
            var inspectionQuestioDto = _mapper.Map<InspectionDto>(inspectionQuestion);

            return Ok(inspectionQuestioDto);
        }
    }
}