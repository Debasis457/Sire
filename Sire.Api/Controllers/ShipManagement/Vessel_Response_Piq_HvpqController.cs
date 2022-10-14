using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sire.Common.UnitOfWork;
using Sire.Data.Dto.ShipManagement;
using Sire.Data.Entities.ShipManagement;
using Sire.Domain.Context;
using Sire.Helper;
using Sire.Respository.ShipManagement;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sire.Api.Controllers.ShipManagement
{
    [Route("api/[controller]")]
    [ApiController]
    public class Vessel_Response_Piq_HvpqController : ControllerBase
    {
        private readonly IJwtTokenAccesser _jwtTokenAccesser;
        private readonly IMapper _mapper;
        private readonly IVessel_Response_Piq_HvpqRepository _Vessel_Response_Piq_HvpqRepository;
        private readonly IUnitOfWork<SireContext> _uow;

        public Vessel_Response_Piq_HvpqController(IVessel_Response_Piq_HvpqRepository Vessel_Response_Piq_HvpqRepository,
            IUnitOfWork<SireContext> uow, IMapper mapper,
            IJwtTokenAccesser jwtTokenAccesser)
        {
            _Vessel_Response_Piq_HvpqRepository = Vessel_Response_Piq_HvpqRepository;
            _uow = uow;
            _mapper = mapper;
            _jwtTokenAccesser = jwtTokenAccesser;
        }


        [AllowAnonymous]
        [HttpPost]
        public IActionResult Post([FromBody] List<Vessel_Response_Piq_HvpqDto> HVPQResponseDto)
        {
            if (!ModelState.IsValid) return new UnprocessableEntityObjectResult(ModelState);
            var test = _mapper.Map<List<Vessel_Response_Piq_Hvpq>>(HVPQResponseDto);

            foreach (var item in test)
            {
                if (item.Id == 0)
                    _Vessel_Response_Piq_HvpqRepository.Add(item);
                else
                    _Vessel_Response_Piq_HvpqRepository.Update(item);
            }

            if (_uow.Save() <= 0) throw new Exception("Creating HVPQ ResponseDto failed on save.");

            var HVPQ_PIQDATA = (from data in _uow.Context.Piq_Hvpq
                                select new Piq_HvpqDto
                                {
                                    PIQHVPQCode = data.PIQHVPQCode,
                                    piq_hvpq_question = data.piq_hvpq_question,
                                    Type = data.Type,
                                    ResponseType = data.ResponseType,
                                }).Distinct();


            #region First Logic
            //foreach (var item in HVPQ_PIQDATA)
            //{
            //    // Get Quetion List From HVPQ Code
            //    var QuestionList = _uow.Context.Piq_Hvpq.Where(x => x.PIQHVPQCode == item.PIQHVPQCode).ToList();

            //    // Get Actual Response List From HVPQ Code
            //    var GetUniqueResponse = _uow.Context.Piq_Hvpq.Where(x => x.PIQHVPQCode == item.PIQHVPQCode).Select(x => x.Response).Distinct().ToList();


            //    //if (QuestionList.Any())
            //    //{
            //    //    foreach (var que in QuestionList.Select(x => x.QuestionId).Distinct().ToList())
            //    //    {
            //    //        // Get Question Response Type
            //    //        var AcQuestion = QuestionList.Where(x => x.QuestionId == que).ToList(); // Ex : Response : DP Shuttle tanker (bow loading)

            //    //        var UserReaponse = HVPQResponseDto.Where(x => x.Piq_Hvpq_Id == item.PIQHVPQCode).ToList(); // Ex : Response : DP Shuttle tanker (bow loading)

            //    //        // Get Response From User
            //    //        if (AcQuestion.Count == 0)
            //    //        {
            //    //            if (AcQuestion[0].Response == UserReaponse[0].Response)
            //    //            {
            //    //                // Add Question To database
            //    //            }
            //    //        }
            //    //        if (AcQuestion.Count > 0)   // For More than One HVPQ PIQ 
            //    //        {
            //    //            if (AcQuestion[0].Type == AcQuestion[1].Type)
            //    //            {

            //    //            }
            //    //            else if (AcQuestion[0].Type == AcQuestion[1].Type) // If both type are diffrent
            //    //            {

            //    //            }
            //    //        }
            //    //    }
            //    //}
            //}
            #endregion


            #region Second Logic
            //foreach (var item in HVPQResponseDto)
            //{

            //    var HVPQPIQD = _uow.Context.Piq_Hvpq.Where(x => x.PIQHVPQCode == item.Piq_Hvpq_Id).FirstOrDefault();
            //    if (HVPQPIQD.ResponseType == ResponseTypePIQ.Dropdown)
            //    {
            //        // Convert Response type in Int For Dropdown
            //        int resint = Convert.ToInt32(item.Response);

            //        // Get Response Type Value
            //        string ResponseText = _uow.Context.PIQ_HVPQ_Response.Where(x => x.Id == resint).FirstOrDefault().value;


            //        //var PIQDataGruopByType = _uow.Context.Piq_Hvpq.Where(x => x.PIQHVPQCode == item.Piq_Hvpq_Id).ToList().GroupBy(x => x.Type).ToList();
            //        //if (PIQDataGruopByType.Any() && PIQDataGruopByType.Count == 1)
            //        //{

            //        //}
            //        //else if(PIQDataGruopByType.Any() && PIQDataGruopByType.Count == 2)
            //        //{

            //        //}



            //        // Check Response Value in Main 
            //        var Data = (from data in _uow.Context.Piq_Hvpq.Where(x => x.PIQHVPQCode == item.Piq_Hvpq_Id && x.Response == ResponseText)
            //                    select new Piq_HvpqDto
            //                    {
            //                        PIQHVPQCode = data.PIQHVPQCode,
            //                        QuestionId = data.QuestionId,
            //                    }).Distinct().ToList();

            //        if (Data.Count > 0)
            //        {
            //            foreach (var qued in Data)
            //            {
            //                Piq_Hvpq_Filter_Quetions aa = new Piq_Hvpq_Filter_Quetions();
            //                aa.VesselId = item.Vessel_Id;
            //                aa.QuestionId = qued.QuestionId;
            //                _uow.Context.Piq_Hvpq_Filter_Quetions.Add(aa);
            //                _uow.Save();
            //            }
            //        }
            //    }
            //    else if (HVPQPIQD.ResponseType == ResponseTypePIQ.Textbox)
            //    {
            //        var Data = _uow.Context.Piq_Hvpq.Where(x => x.PIQHVPQCode == item.Piq_Hvpq_Id && x.Response == item.Response).Distinct().ToList();

            //        var PIQDataGruopByType = _uow.Context.Piq_Hvpq.Where(x => x.PIQHVPQCode == item.Piq_Hvpq_Id).ToList().GroupBy(x => x.Type).ToList();
            //        if (PIQDataGruopByType.Any() && PIQDataGruopByType.Count == 1)
            //        {
            //            if (Data != null && Data.Count > 0)
            //            {
            //                var DataNew = _uow.Context.Piq_Hvpq.Where(x => x.PIQHVPQCode == item.Piq_Hvpq_Id).Distinct().ToList();

            //                foreach (var qued in DataNew)
            //                {
            //                    Piq_Hvpq_Filter_Quetions aa = new Piq_Hvpq_Filter_Quetions();
            //                    aa.VesselId = item.Vessel_Id;
            //                    aa.QuestionId = qued.QuestionId;
            //                    _uow.Context.Piq_Hvpq_Filter_Quetions.Add(aa);
            //                    _uow.Save();
            //                }
            //            }
            //        }
            //        else if (PIQDataGruopByType.Any() && PIQDataGruopByType.Count == 2)
            //        {

            //        }


            //    }
            //    else if (HVPQPIQD.ResponseType == ResponseTypePIQ.Switch)
            //    {

            //        var PIQDataGruopByType = _uow.Context.Piq_Hvpq.Where(x => x.PIQHVPQCode == item.Piq_Hvpq_Id).ToList().GroupBy(x => x.Type)
            //            .Select(aa => new
            //            {
            //                QuetionType = aa.Key,
            //                QuestionList = aa.ToList()
            //            }).ToList();

            //        if (PIQDataGruopByType.Any() && PIQDataGruopByType.Count == 1)
            //        {
            //            var Data = _uow.Context.Piq_Hvpq.Where(x => x.PIQHVPQCode == item.Piq_Hvpq_Id && x.Response == item.Response).Distinct().ToList();

            //            if (Data != null && Data.Count > 0)
            //            {
            //                var DataNew = _uow.Context.Piq_Hvpq.Where(x => x.PIQHVPQCode == item.Piq_Hvpq_Id).Distinct().ToList();

            //                foreach (var qued in DataNew)
            //                {
            //                    Piq_Hvpq_Filter_Quetions aa = new Piq_Hvpq_Filter_Quetions();
            //                    aa.VesselId = item.Vessel_Id;
            //                    aa.QuestionId = qued.QuestionId;
            //                    _uow.Context.Piq_Hvpq_Filter_Quetions.Add(aa);
            //                    _uow.Save();
            //                }
            //            }
            //        }
            //        else if (PIQDataGruopByType.Any() && PIQDataGruopByType.Count == 2)
            //        {
            //            if (PIQDataGruopByType[0].QuetionType == PIQDataGruopByType[1].QuetionType )
            //            {

            //            }
            //        }



            //    }



            //}
            #endregion

            List<int> FilterQuestionList = new List<int>();
            #region Third Logic
            var PIQDataGruopByType = _uow.Context.Piq_Hvpq.ToList().GroupBy(x => x.QuestionId)
                .Select(aa => new
                {
                    QuetionId = aa.Key,
                    PIQList = aa.ToList()
                }).ToList();

            int vesselid = HVPQResponseDto.FirstOrDefault().Vessel_Id ?? 0;
            var databasePIQ = _uow.Context.Vessel_Response_Piq_Hvpq.Where(x => x.Vessel_Id == vesselid).ToList();
            HVPQResponseDto = _mapper.Map<List<Vessel_Response_Piq_HvpqDto>>(databasePIQ);
            foreach (var item in PIQDataGruopByType)
            {
                // Get Data By Quetions
                var QuetionsList = _uow.Context.Piq_Hvpq.Where(x => x.QuestionId == item.QuetionId).ToList();
                if (item.PIQList.Count == 1)
                {
                    foreach (var que in item.PIQList)
                    {
                        if (que.ResponseType == ResponseTypePIQ.Dropdown)
                        {

                        }
                        else if (que.ResponseType == ResponseTypePIQ.Textbox)
                        {
                            var ActualResponse = que.Response;
                            var UserResponse = HVPQResponseDto.Where(x => x.Piq_Hvpq_Id == que.PIQHVPQCode).FirstOrDefault().Response;
                            var ListInsert = new List<Piq_Hvpq_Filter_Quetions>();
                            if (ActualResponse == UserResponse)
                            {
                                // ListInsert List Add

                                //                    Piq_Hvpq_Filter_Quetions aa = new Piq_Hvpq_Filter_Quetions();
                                //                    aa.VesselId = item.Vessel_Id;
                                //                    aa.QuestionId = qued.QuestionId;
                                //                    _uow.Context.Piq_Hvpq_Filter_Quetions.Add(aa);
                                //                    _uow.Save();
                            }

                            /// List Distinct
                        }
                        else if (que.ResponseType == ResponseTypePIQ.Switch)
                        {
                            var ActualResponse = que.Response;
                            var UserResponse = HVPQResponseDto.Where(x => x.Piq_Hvpq_Id == que.PIQHVPQCode).FirstOrDefault().Response;
                            var ListInsert = new List<Piq_Hvpq_Filter_Quetions>();
                            if (ActualResponse == UserResponse)
                            {
                                //Piq_Hvpq_Filter_Quetions aa = new Piq_Hvpq_Filter_Quetions();
                                //aa.VesselId = vesselid;
                                //aa.QuestionId = que.QuestionId;
                                //_uow.Context.Piq_Hvpq_Filter_Quetions.Add(aa);
                                //_uow.Save();
                                FilterQuestionList.Add(que.QuestionId ?? 0);
                            }

                        }
                    }
                }
                else if (item.PIQList.Count == 2)
                {
                    var PIQType = false;
                    var HVPQType = false;
                    foreach (var piqhvpq in item.PIQList)
                    {

                        if (piqhvpq.Type == "PIQ")
                        {
                            //foreach (var que in QuetionsList)
                            //{
                            var ActualResponse = piqhvpq.Response;
                            var UserResponse = HVPQResponseDto.Where(x => x.Piq_Hvpq_Id == piqhvpq.PIQHVPQCode).FirstOrDefault().Response;
                            if (ActualResponse == UserResponse)
                            {
                                PIQType = true;
                            }
                            //}
                        }
                        else if (piqhvpq.Type == "HVPQ")
                        {
                            //foreach (var que in QuetionsList)
                            //{
                            var ActualResponse = piqhvpq.Response;
                            var UserResponse = HVPQResponseDto.Where(x => x.Piq_Hvpq_Id == piqhvpq.PIQHVPQCode).FirstOrDefault().Response;
                            if (ActualResponse == UserResponse)
                            {
                                HVPQType = true;
                            }
                            //}
                        }

                    }
                    if (PIQType && HVPQType)
                    {
                        foreach (var q in QuetionsList)
                        {
                            //Piq_Hvpq_Filter_Quetions aa = new Piq_Hvpq_Filter_Quetions();
                            //aa.VesselId = vesselid;
                            //aa.QuestionId = q.QuestionId;
                            //_uow.Context.Piq_Hvpq_Filter_Quetions.Add(aa);
                            //_uow.Save();
                            FilterQuestionList.Add(q.QuestionId ?? 0);
                        }
                    }
                }
            }
            if (FilterQuestionList.Count > 0)
            {
                foreach (var que in FilterQuestionList.Distinct())
                {
                    Piq_Hvpq_Filter_Quetions aa = new Piq_Hvpq_Filter_Quetions();
                    aa.VesselId = vesselid;
                    aa.QuestionId = que;
                    _uow.Context.Piq_Hvpq_Filter_Quetions.Add(aa);
                    _uow.Save();
                }
            }

            #endregion
            return Ok(0);
        }
    }
}
