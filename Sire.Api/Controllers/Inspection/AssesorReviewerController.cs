﻿using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sire.Common.UnitOfWork;
using Sire.Data.Dto.Inspection;
using Sire.Data.Dto.Master;
using Sire.Data.Dto.Question;
using Sire.Data.Entities.Inspection;
using Sire.Domain.Context;
using Sire.Helper;
using Sire.Respository.Inspection;
using Sire.Respository.Master;
using Sire.Respository.ShipManagement;

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
        private readonly IPiq_HvpqRepository _piq_HvpqRepository;

        public AssesorReviewerController(
            IInspection_QuestionRepository inspection_QuestionRepository,
            IUnitOfWork<SireContext> uow, IMapper mapper,
            IQuetionSectionRepository quetionSectionRepository,
            IPiq_HvpqRepository piqHvpqRepository,
            IJwtTokenAccesser jwtTokenAccesser)
        {
            _inspection_QuestionRepository = inspection_QuestionRepository;
            _uow = uow;
            _mapper = mapper;
            _jwtTokenAccesser = jwtTokenAccesser;
            _quetionSectionRepository = quetionSectionRepository;
            _piq_HvpqRepository = piqHvpqRepository;
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
        [HttpGet("GetSectionList")]
        public IActionResult GetSectionList()
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

                var assesmentCompletionCount = (from sec in _uow.Context.QuetionSection.Where(x => x.Id == item.Id)
                                                join sub in _uow.Context.QuetionSubSection on sec.Id equals sub.QuetionSectionId
                                                join que in _uow.Context.Question on sub.Id equals que.Section
                                                join ins in _uow.Context.Inspection_Question.Where(x => x.inspection_id == id && x.assesment_completed == true) on que.Id equals ins.question_id
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

                    subb.ResTotal = (from sec in _uow.Context.QuetionSection.Where(x => x.Id == item.Id)
                                     join sub in _uow.Context.QuetionSubSection.Where(x => x.Id == subb.Id) on sec.Id equals sub.QuetionSectionId
                                     join que in _uow.Context.Question on sub.Id equals que.Section
                                     join ins in _uow.Context.Inspection_Question.Where(x => x.inspection_id == id && x.assesment_completed == true) on que.Id equals ins.question_id
                                     select new
                                     {
                                         que.Id

                                     }).ToList().Count;
                }
                item.Total = count;
                item.ResTotal = assesmentCompletionCount;
            }
            return Ok(testsDto);
        }

        [AllowAnonymous]
        [HttpGet("GetAssesordataByInspection/{id}/{sectionId}")]
        public IActionResult GetAssesordataByInspection(int? id, int? sectionId)
        {
            var data = (from quetion in _uow.Context.Question.Where(x => x.Section == sectionId)
                        join assque in _uow.Context.Inspection_Question.Where(x => x.inspection_id == id) on quetion.Id equals assque.question_id into g
                        from assque in g.DefaultIfEmpty()
                        join userrew in _uow.Context.User on assque.reviewer_id equals userrew.Id into r
                        from userrew in r.DefaultIfEmpty()
                        join userass in _uow.Context.User on assque.assessor_id equals userass.Id into a
                        from userass in a.DefaultIfEmpty()
                        select new Inspection_QuestionDto
                        {
                            Id = assque != null ? assque.Id : 0,
                            Reviewer_Id = assque != null ? assque.reviewer_id : 0,
                            Assessor_Id = assque != null ? assque.assessor_id : 0,
                            Inspection_Id = assque != null ? assque.inspection_id : 0,
                            Reviewer_Name = assque != null ? userrew.UserName : "",
                            Assessor_Name = assque != null ? userass.UserName : "",
                            Question_Id = quetion.Id,
                            Question_Text = quetion.Questions,
                            Assesment_Completed = assque != null ? assque.assesment_completed : false,
                            Review_Completed = assque != null ? assque.review_completed : false

                        }).ToList();

            return Ok(data);
        }

        [AllowAnonymous]
        [HttpGet("GetInspectionQuestion/{id}")]
        public IActionResult GetInspectionQuestion(int id)
        {
            var inspectionQuestion = _uow.Context.Inspection_Question.First(x => x.Id == id);
            var inspectionQuestioDto = _mapper.Map<Inspection_QuestionDto>(inspectionQuestion);

            return Ok(inspectionQuestioDto);
        }

        [AllowAnonymous]
        [HttpPost("CompleteInspectionQuestion")]
        public IActionResult Post([FromBody] Inspection_QuestionDto InspectionQuestionDto)
        {
            if (!ModelState.IsValid) return new UnprocessableEntityObjectResult(ModelState);
            var mappedData = _mapper.Map<Inspection_Question>(InspectionQuestionDto);

            if (InspectionQuestionDto.Id == 0)
                _inspection_QuestionRepository.Add(mappedData);
            else
                _inspection_QuestionRepository.Update(mappedData);

            if (_uow.Save() <= 0) throw new Exception("Creating Test failed on save.");
            return Ok(mappedData.Id);

        }

        [AllowAnonymous]
        [HttpGet("GetInspectionDetailsForVessel/{vesselId}/{userId}")]
        public IActionResult GetInspectionDetailsForVessel(int vesselId, int userId)
        {
            var data = (from ins in _uow.Context.Inspection.Where(x => x.Vessel_Id == vesselId && x.Operator_id == userId)
                        join insque in _uow.Context.Inspection_Question on ins.Id equals insque.inspection_id into InspectionQuestionGroup
                        select new OngoingInspectionDto
                        {
                            Inspection = _mapper.Map<InspectionDto>(ins),
                            InspectionQuestions = _mapper.Map<IEnumerable<Inspection_QuestionDto>>(InspectionQuestionGroup)
                        }).OrderByDescending(x => x.Inspection.Started_At).ToList();

            return Ok(data);
        }


        [AllowAnonymous]
        [HttpGet("GetAssesordataByInspectionSeperate/{id}/{inspectionId}")]
        public IActionResult GetAssesordataByInspectionSeperate(int? id, int? inspectionId)
        {
            var data = (from quetion in _uow.Context.Question.Where(x => x.Section == id)
                        join assque in _uow.Context.Inspection_Question.Where(x => x.inspection_id == inspectionId) on quetion.Id equals assque.question_id into g
                        from assque in g.DefaultIfEmpty()
                        join userrew in _uow.Context.User on assque.reviewer_id equals userrew.Id into r
                        from userrew in r.DefaultIfEmpty()
                        join userass in _uow.Context.User on assque.assessor_id equals userass.Id into a
                        from userass in a.DefaultIfEmpty()
                        select new Inspection_QuestionDto
                        {
                            Id = assque != null ? assque.Id : 0,
                            Reviewer_Id = assque != null ? assque.reviewer_id : 0,
                            Assessor_Id = assque != null ? assque.assessor_id : 0,
                            Inspection_Id = assque != null ? assque.inspection_id : 0,
                            Reviewer_Name = assque != null ? userrew.UserName : "",
                            Assessor_Name = assque != null ? userass.UserName : "",
                            Question_Id = quetion.Id,
                            Question_Text = quetion.Questions,
                            Assesment_Completed = assque != null ? assque.assesment_completed : false,
                            Review_Completed = assque != null ? assque.review_completed : false

                        }).ToList();

            return Ok(data);
        }

        [AllowAnonymous]
        [HttpGet("GetSectionListQuestionLibrary")]
        public IActionResult GetSectionListQuestionLibrary(int assessorId, int reviewerId, int vesselId)
        {
            var tests = _quetionSectionRepository.FindByInclude(x => x.Id > 0, x => x.QuetionSubSection)
                .ToList();
            var testsDto = _mapper.Map<IEnumerable<QuetionSectionDto>>(tests);
            foreach (var item in testsDto)
            {
                var assRevQuestionIds = (from sec in _uow.Context.QuetionSection.Where(x => x.Id == item.Id)
                                         join sub in _uow.Context.QuetionSubSection
                                            on sec.Id equals sub.QuetionSectionId
                                         join que in _uow.Context.Question.Where(x => x.DAssessore == assessorId && x.DReviewer == reviewerId)
                                            on sub.Id equals que.Section
                                         select new
                                         {
                                             que.Id

                                         }).ToList();

                var piqHvpqQuestionsIds = (from sec in _uow.Context.QuetionSection.Where(x => x.Id == item.Id)
                                         join sub in _uow.Context.QuetionSubSection on sec.Id equals sub.QuetionSectionId
                                         join que in _uow.Context.Question on sub.Id equals que.Section
                                         join piqque in _uow.Context.PIQ_HVPQ_Response_Mapping1 on que.Id equals piqque.QuestionId
                                         select new
                                         {
                                             piqque.Id

                                         }).ToList();

                var count = assRevQuestionIds.Union(piqHvpqQuestionsIds).Count();


                foreach (var subb in item.QuetionSubSection)
                {
                    var assRevTotalQuestionIds = (from sec in _uow.Context.QuetionSection.Where(x => x.Id == item.Id)
                                                  join sub in _uow.Context.QuetionSubSection.Where(x => x.Id == subb.Id) on sec.Id equals sub.QuetionSectionId
                                                  join que in _uow.Context.Question.Where(x => x.DAssessore == assessorId && x.DReviewer == reviewerId)
                                                    on sub.Id equals que.Section
                                                  select new
                                                  {
                                                      que.Id

                                                  }).ToList();

                    var piqHvpqTotalQuestionsIds = (from sec in _uow.Context.QuetionSection.Where(x => x.Id == item.Id)
                                                    join sub in _uow.Context.QuetionSubSection.Where(x => x.Id == subb.Id) on sec.Id equals sub.QuetionSectionId
                                                    join que in _uow.Context.Question on sub.Id equals que.Section
                                                    join piqque in _uow.Context.PIQ_HVPQ_Response_Mapping1 on que.Id equals piqque.QuestionId
                                                    select new
                                                    {
                                                        piqque.Id

                                                    }).ToList();

                    subb.Total = assRevTotalQuestionIds.Union(piqHvpqTotalQuestionsIds).Count();
                }
                item.Total = count;
            }
            return Ok(testsDto);
        }

        [AllowAnonymous]
        [HttpGet("GetInspectionApplicableQuestionBySection")]
        public IActionResult GetInspectionApplicableQuestionBySection(int sectionId, int vesselId, int assessorId, int reviewerId)
        {
            var piqHvpqQuestions = _piq_HvpqRepository.AllIncluding().ToList();
            //var allQuestions = _questionRepository.AllIncluding().ToList();
            var questions = new List<QuestionDto>();

            var assRevQuestionsData = (from quetion in _uow.Context.Question.Where(x => x.Section == sectionId && x.DAssessore == assessorId && x.DReviewer == reviewerId)
                        join userrew in _uow.Context.User on reviewerId equals userrew.Id into r
                        from userrew in r.DefaultIfEmpty()
                        join userass in _uow.Context.User on assessorId equals userass.Id into a
                        from userass in a.DefaultIfEmpty()
                        select new Inspection_QuestionDto
                        {
                            Id = 0,
                            Reviewer_Id = reviewerId,
                            Assessor_Id = assessorId,
                            Inspection_Id = 0,
                            Reviewer_Name = userrew != null ? userrew.UserName : "",
                            Assessor_Name = userass != null ? userass.UserName : "",
                            Question_Id = quetion.Id,
                            Question_Text = quetion.Questions,
                            Assesment_Completed = false,
                            Review_Completed = false
                        }).ToList();

            var piqHvpqQuestionsData = (from quetion in _uow.Context.Question.Where(x => x.Section == sectionId && x.DAssessore == assessorId && x.DReviewer == reviewerId)
                                        join userrew in _uow.Context.User on reviewerId equals userrew.Id into r
                                        from userrew in r.DefaultIfEmpty()
                                        join userass in _uow.Context.User on assessorId equals userass.Id into a
                                        from userass in a.DefaultIfEmpty()
                                        join piqque in _uow.Context.PIQ_HVPQ_Response_Mapping1 on quetion.Id equals piqque.QuestionId into p 
                                        from pique in p.DefaultIfEmpty()
                                        select new Inspection_QuestionDto
                                        {
                                            Id = 0,
                                            Reviewer_Id = reviewerId,
                                            Assessor_Id = assessorId,
                                            Inspection_Id = 0,
                                            Reviewer_Name = userrew != null ? userrew.UserName : "",
                                            Assessor_Name = userass != null ? userass.UserName : "",
                                            Question_Id = quetion.Id,
                                            Question_Text = quetion.Questions,
                                            Assesment_Completed = false,
                                            Review_Completed = false
                                        }).ToList();

            return Ok(data);

            //foreach (var piqHvpqQuestion in piqHvpqQuestions.GroupBy(x => x.QuestionId))
            //{
            //    if (piqHvpqQuestion.Count() == 1)
            //    {
            //        questions.Add(_mapper.Map<QuestionDto>(allQuestions.First(x => x.Id == piqHvpqQuestion.First().QuestionId)));
            //        // Consider the question if there is only 1 PIQ/HVPQ question
            //    }
            //    else if (piqHvpqQuestion.Count() == 2)
            //    {
            //        if (piqHvpqQuestion.ElementAt(0).Type == piqHvpqQuestion.ElementAt(1).Type)
            //        {

            //        }
            //    }
            //}

            return Ok(questions);
        }
    }
}