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
using Sire.Data.Dto.Master;
using Sire.Respository.Master;
using Sire.Data.Dto.Question;
using Sire.Respository.Question;

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
        private readonly IQuestionRepository _questionRepository;

        public TrainingQuestionController(
            IQuestionRepository questionRepository,
            ITrainingQuestionRepository testRepository,
            IUnitOfWork<SireContext> uow, IMapper mapper,
             IQuetionSectionRepository quetionSectionRepository,
            IJwtTokenAccesser jwtTokenAccesser)
        {
            _trainingQuestionRepository = testRepository;
            _uow = uow;
            _mapper = mapper;
            _jwtTokenAccesser = jwtTokenAccesser;
            _quetionSectionRepository = quetionSectionRepository;
            _questionRepository = questionRepository;
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
        [HttpGet("{traningid}/{userid}")]
        public IActionResult GetSectionList(int? traningid, int? userid)
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
                var responseCount = (from sec in _uow.Context.QuetionSection.Where(x => x.Id == item.Id)
                                     join sub in _uow.Context.QuetionSubSection on sec.Id equals sub.QuetionSectionId
                                     join que in _uow.Context.Question on sub.Id equals que.Section
                                     join res in _uow.Context.TraningResponse.Where(x => x.Training_Id == traningid && x.Trainee_Id == userid) on que.Id equals res.Question_Id
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

                    subb.ResTotal = (from sec in _uow.Context.QuetionSection.Where(x => x.Id == item.Id)
                                     join sub in _uow.Context.QuetionSubSection.Where(x => x.Id == subb.Id) on sec.Id equals sub.QuetionSectionId
                                     join que in _uow.Context.Question on sub.Id equals que.Section
                                     join res in _uow.Context.TraningResponse.Where(x => x.Training_Id == traningid && x.Trainee_Id == userid) on que.Id equals res.Question_Id
                                     select new
                                     {
                                         que.Id

                                     }).ToList().Count;
                }
                item.Total = count;
                item.ResTotal = responseCount;
            }
            return Ok(testsDto);
        }

        [AllowAnonymous]
        [HttpGet("GetSectionListRankBasedQuestions/{rankGroupId}/{trainingId}/{userId}")]
        public IActionResult GetSectionListRankBasedQuestions(int rankGroupId, int trainingId, int userId)
        {
            var questionSections = _quetionSectionRepository.FindByInclude(x => x.Id > 0, x => x.QuetionSubSection).ToList();
            var questionSectionsDto = _mapper.Map<IEnumerable<QuetionSectionDto>>(questionSections);

            foreach (var questionSectionDto in questionSectionsDto)
            {
                var count = (from sec in _uow.Context.QuetionSection.Where(x => x.Id == questionSectionDto.Id)
                             join sub in _uow.Context.QuetionSubSection
                                on sec.Id equals sub.QuetionSectionId
                             join que in _uow.Context.Question.Where(x => x.Rank_Group_Id == rankGroupId)
                                on sub.Id equals que.Section
                             select new
                             {
                                 que.Id

                             }).ToList().Count;

                var responseCount = (from sec in _uow.Context.QuetionSection.Where(x => x.Id == questionSectionDto.Id)
                                     join sub in _uow.Context.QuetionSubSection on sec.Id equals sub.QuetionSectionId
                                     join que in _uow.Context.Question on sub.Id equals que.Section
                                     join res in _uow.Context.TraningResponse.Where(x => x.Training_Id == trainingId && x.Trainee_Id == userId) on que.Id equals res.Question_Id
                                     select new
                                     {
                                         que.Id

                                     }).ToList().Count;

                foreach (var subb in questionSectionDto.QuetionSubSection)
                {
                    subb.Total = (from sec in _uow.Context.QuetionSection.Where(x => x.Id == questionSectionDto.Id)
                                  join sub in _uow.Context.QuetionSubSection.Where(x => x.Id == subb.Id) on sec.Id equals sub.QuetionSectionId
                                  join que in _uow.Context.Question.Where(x => x.Rank_Group_Id == rankGroupId)
                                    on sub.Id equals que.Section
                                  select new
                                  {
                                      que.Id

                                  }).ToList().Count;

                    subb.ResTotal = (from sec in _uow.Context.QuetionSection.Where(x => x.Id == questionSectionDto.Id)
                                     join sub in _uow.Context.QuetionSubSection.Where(x => x.Id == subb.Id) on sec.Id equals sub.QuetionSectionId
                                     join que in _uow.Context.Question on sub.Id equals que.Section
                                     join res in _uow.Context.TraningResponse.Where(x => x.Training_Id == trainingId && x.Trainee_Id == userId) on que.Id equals res.Question_Id
                                     select new
                                     {
                                         que.Id

                                     }).ToList().Count;
                }

                questionSectionDto.Total = count;
                questionSectionDto.ResTotal = responseCount;
            }

            return Ok(questionSectionsDto);
        }

        [AllowAnonymous]
        [HttpGet("GetRankBasedQuestionsBySection/{sectionId}/{rankGroupId}/{trainingId}/{userId}")]
        public IActionResult GetRankBasedQuestionsBySection(int sectionId, int rankGroupId, int trainingId, int userId)
        {
            var questionDtos = new List<QuestionDto>();

            var rankBasedQuestionIds = (from quetion in _uow.Context.Question.Where(x => x.Section == sectionId && x.Rank_Group_Id == rankGroupId)
                                      select new
                                      {
                                          Id = quetion.Id

                                      }).ToList();

            questionDtos = _mapper.Map<IEnumerable<QuestionDto>>(_questionRepository.FindByInclude(x => rankBasedQuestionIds.Select(y => y.Id).Contains(x.Id))).ToList();

            return Ok(questionDtos);
        }

        [AllowAnonymous]
        [HttpGet("GetSectionListApplicableQuestions/{assessorId}/{reviewerId}/{trainingId}/{vesselId}/{userId}")]
        public IActionResult GetSectionListApplicableQuestions(int assessorId, int reviewerId, int trainingId, int vesselId, int userId)
        {
            var questionSections = _quetionSectionRepository.FindByInclude(x => x.Id > 0, x => x.QuetionSubSection).ToList();
            var questionSectionsDto = _mapper.Map<IEnumerable<QuetionSectionDto>>(questionSections);

            foreach (var questionSectionDto in questionSectionsDto)
            {
                var assRevQuestionIds = (from sec in _uow.Context.QuetionSection.Where(x => x.Id == questionSectionDto.Id)
                                         join sub in _uow.Context.QuetionSubSection
                                            on sec.Id equals sub.QuetionSectionId
                                         join que in _uow.Context.Question.Where(x => x.DAssessore == assessorId && x.DReviewer == reviewerId)
                                            on sub.Id equals que.Section
                                         select new
                                         {
                                             Id = que.Id

                                         }).ToList();

                var piqHvpqQuestionsIds = (from sec in _uow.Context.QuetionSection.Where(x => x.Id == questionSectionDto.Id)
                                           join sub in _uow.Context.QuetionSubSection on sec.Id equals sub.QuetionSectionId
                                           join que in _uow.Context.Question on sub.Id equals que.Section
                                           join piqque in _uow.Context.Piq_Hvpq_Filter_Quetions.Where(x => x.VesselId == vesselId) on que.Id equals piqque.QuestionId
                                           select new
                                           {
                                               Id = piqque.QuestionId

                                           }).ToList();

                var mergedQuestionIds = assRevQuestionIds.Union(piqHvpqQuestionsIds);

                var count = mergedQuestionIds.Count();

                var responseCount = (from sec in _uow.Context.QuetionSection.Where(x => x.Id == questionSectionDto.Id)
                                     join sub in _uow.Context.QuetionSubSection on sec.Id equals sub.QuetionSectionId
                                     join que in _uow.Context.Question on sub.Id equals que.Section
                                     join res in _uow.Context.TraningResponse.Where(x => x.Training_Id == trainingId && x.Trainee_Id == userId) on que.Id equals res.Question_Id
                                     select new
                                     {
                                         que.Id

                                     }).ToList().Count;

                foreach (var subb in questionSectionDto.QuetionSubSection)
                {
                    var assRevTotalQuestionIds = (from sec in _uow.Context.QuetionSection.Where(x => x.Id == questionSectionDto.Id)
                                                  join sub in _uow.Context.QuetionSubSection.Where(x => x.Id == subb.Id) on sec.Id equals sub.QuetionSectionId
                                                  join que in _uow.Context.Question.Where(x => x.DAssessore == assessorId && x.DReviewer == reviewerId)
                                                    on sub.Id equals que.Section
                                                  select new
                                                  {
                                                      Id = que.Id

                                                  }).ToList();

                    var piqHvpqTotalQuestionsIds = (from sec in _uow.Context.QuetionSection.Where(x => x.Id == questionSectionDto.Id)
                                                    join sub in _uow.Context.QuetionSubSection.Where(x => x.Id == subb.Id) on sec.Id equals sub.QuetionSectionId
                                                    join que in _uow.Context.Question on sub.Id equals que.Section
                                                    join piqque in _uow.Context.Piq_Hvpq_Filter_Quetions.Where(x => x.VesselId == vesselId) on que.Id equals piqque.QuestionId
                                                    select new
                                                    {
                                                        Id = piqque.QuestionId

                                                    }).ToList();

                    subb.Total = assRevTotalQuestionIds.Union(piqHvpqTotalQuestionsIds).Count();

                    subb.ResTotal = (from sec in _uow.Context.QuetionSection.Where(x => x.Id == questionSectionDto.Id)
                                     join sub in _uow.Context.QuetionSubSection.Where(x => x.Id == subb.Id) on sec.Id equals sub.QuetionSectionId
                                     join que in _uow.Context.Question on sub.Id equals que.Section
                                     join res in _uow.Context.TraningResponse.Where(x => x.Training_Id == trainingId && x.Trainee_Id == userId) on que.Id equals res.Question_Id
                                     select new
                                     {
                                         que.Id

                                     }).ToList().Count;
                }

                questionSectionDto.ResTotal = responseCount;
                questionSectionDto.Total = count;
            }

            return Ok(questionSectionsDto);
        }

        [AllowAnonymous]
        [HttpGet("GetApplicationQuestionsBySection/{sectionId}/{assessorId}/{reviewerId}/{trainingId}/{vesselId}/{userId}")]
        public IActionResult GetApplicationQuestionsBySection(int sectionId, int assessorId, int reviewerId, int trainingId, int vesselId, int userId)
        {
            var questionDtos = new List<QuestionDto>();
            //var assRevQuestionsData = (from quetion in _uow.Context.Question.Where(x => x.Section == sectionId && x.DAssessore == assessorId && x.DReviewer == reviewerId)
            //                           select new Training_QuestionDto
            //                           {
            //                               Id = 0,
            //                               Completed = false,
            //                               IsDeleted = false,
            //                               Question_Id = quetion.Id,
            //                               Trainee_Id = userId,
            //                               Training_Id = trainingId
            //                           }).ToList();

            //var piqHvpqQuestionsData = (from quetion in _uow.Context.Question.Where(x => x.Section == sectionId)
            //                            join piqque in _uow.Context.Piq_Hvpq_Filter_Quetions.Where(x => x.VesselId == vesselId) on quetion.Id equals piqque.QuestionId
            //                            select new Training_QuestionDto
            //                            {
            //                                Id = 0,
            //                                Completed = false,
            //                                IsDeleted = false,
            //                                Question_Id = quetion.Id,
            //                                Trainee_Id = userId,
            //                                Training_Id = trainingId,
            //                            }).ToList();



            //var mergedQuestions = new List<Training_QuestionDto>(assRevQuestionsData);
            //mergedQuestions.AddRange(piqHvpqQuestionsData.Where(p2 => assRevQuestionsData.All(x => x.Question_Id != x.Question_Id)));
            var assRevQuestionsData = (from quetion in _uow.Context.Question.Where(x => x.Section == sectionId && x.DAssessore == assessorId && x.DReviewer == reviewerId)
                                       select new
                                       {
                                           Id = quetion.Id

                                       }).ToList();

            var piqHvpqQuestionsData = (from quetion in _uow.Context.Question.Where(x => x.Section == sectionId)
                                        join piqque in _uow.Context.Piq_Hvpq_Filter_Quetions.Where(x => x.VesselId == vesselId) on quetion.Id equals piqque.QuestionId
                                        select new
                                        {
                                            Id = quetion.Id

                                        }).ToList();


            var mergedQuestions = new List<int>(assRevQuestionsData.Select(x => x.Id));
            mergedQuestions.AddRange(piqHvpqQuestionsData.Where(p2 => assRevQuestionsData.All(x => x.Id != x.Id)).Select(x => x.Id));

            questionDtos = _mapper.Map<IEnumerable<QuestionDto>>(_questionRepository.FindByInclude(x => mergedQuestions.Select(y => y).Contains(x.Id))).ToList();

            return Ok(questionDtos);
        }
    }
}

