using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Sire.Data.Dto.Master;
using Sire.Data.Dto.Operator;
using Sire.Data.Dto.Question;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Sire.Web.Controllers
{
    [Authorize]
    public class TrainingQuestionController : Controller
    {


        private readonly ILogger<TrainingQuestionController> _logger;
        private readonly Microsoft.Extensions.Configuration.IConfiguration _iConfig;
        string apiBaseUrl = string.Empty;
        string apiBaseQuestionUrl = string.Empty;
        string apiBaseUserRankUrl = string.Empty;
        public TrainingQuestionController(ILogger<TrainingQuestionController> logger,
            Microsoft.Extensions.Configuration.IConfiguration iConfig
            )
        {
            _logger = logger;
            _iConfig = iConfig;
            apiBaseUrl = _iConfig.GetValue<string>("apiUrl:url").ToString() + "/trainingquestion";
            apiBaseQuestionUrl = _iConfig.GetValue<string>("apiUrl:url").ToString() + "/question";
            apiBaseUserRankUrl = _iConfig.GetValue<string>("apiUrl:url").ToString() + "/User_Rank";
        }
        //Dharini
       
            public async Task<IActionResult> Index(int? id)
        {
            TempData["TrainingId"] = id;

            id = id == null ? 0 : id;
            try {

             
                    using (HttpClient client = new HttpClient())
                    {
                        var userid = Convert.ToInt32(HttpContext.Session.GetString("UserId"));
                        var endpoint = apiBaseUrl + "/" + id + "/" + userid;

                        using (var Response = await client.GetAsync(endpoint))
                        {
                            if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                            {

                                var data = JsonConvert.DeserializeObject<List<QuetionSectionDto>>(Response.Content.ReadAsStringAsync().Result);
                            return View(data);
                        }
                            else
                            {
                                ModelState.Clear();
                                ModelState.AddModelError(string.Empty, "Invalid Data");
                                return View();
                            }
                        }


                    }
                
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            return View();
           
        }

        [Route("TrainingQuestion/GetDetails/{trainingId}/{questionId}")]
        public async Task<IActionResult> GetDetails(int? trainingId, int? questionId)
        {
            TempData["TrainingId"] = trainingId;
            TempData["QuestionId"] = questionId;

            var endquestion = apiBaseQuestionUrl + "/" + questionId;

            using (HttpClient client = new HttpClient())
            {
                using (var Response = await client.GetAsync(endquestion))
                {
                    if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var result = Response.Content.ReadAsStringAsync().Result;

                        var data = JsonConvert.DeserializeObject<QuestionDto>(Response.Content.ReadAsStringAsync().Result);

                        return View("~/Views/TrainingFlow/Index.cshtml", data);
                    }
                    else
                    {
                        ModelState.Clear();
                        ModelState.AddModelError(string.Empty, "Invalid Data");
                        return View();
                    }
                }
            }
        }
        public async Task<PartialViewResult> GetQuestion(int? id)
        {
            TempData["TrainingId"] = id;

            id = id == null ? 0 : id;
            try
            {


                using (HttpClient client = new HttpClient())
                {
                    var userid = Convert.ToInt32(HttpContext.Session.GetString("UserId"));
                    var endpoint = apiBaseUrl + "/" + id + "/" + userid;

                    using (var Response = await client.GetAsync(endpoint))
                    {
                        if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                        {

                            var data = JsonConvert.DeserializeObject<List<QuetionSectionDto>>(Response.Content.ReadAsStringAsync().Result);
                            return PartialView("QuestionLibrary", data);
                        }
                        else
                        {
                            ModelState.Clear();
                            ModelState.AddModelError(string.Empty, "Invalid Data");
                            return PartialView();
                        }
                    }


                }

            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }


        }

        public async Task<PartialViewResult> GetRenkBaseQuestion(int? id)
        {
            TempData["TrainingId"] = id;

            id = id == null ? 0 : id;
            var endUserRank = apiBaseUserRankUrl + "/GetUser_RankDropDown";
            try
            {


                using (HttpClient client = new HttpClient())
                {
                    var userid = Convert.ToInt32(HttpContext.Session.GetString("UserId"));
                    var endpoint = apiBaseUrl + "/" + id + "/" + userid;

                    using (var Response = await client.GetAsync(endpoint))
                    {
                        if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            var data = JsonConvert.DeserializeObject<List<QuetionSectionDto>>(Response.Content.ReadAsStringAsync().Result);


                            using (var IUserResponse = await client.GetAsync(endUserRank))
                            {
                                if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                                {
                                    var RankData = JsonConvert.DeserializeObject<IEnumerable<DropDownDto>>(IUserResponse.Content.ReadAsStringAsync().Result);
                                    ViewBag.Rank = RankData;
                                }
                                else
                                {
                                    ModelState.Clear();
                                }
                            }



                            return PartialView("RankBaseQuestion", data);
                        }
                        else
                        {
                            ModelState.Clear();
                            ModelState.AddModelError(string.Empty, "Invalid Data");
                            return PartialView();
                        }
                    }


                }

            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }


        }
        public async Task<PartialViewResult> GetApplicableQuestions(int? id)
        {
            return PartialView("ApplicableQuestions");
        }

        public async Task<PartialViewResult> GetCIVQquestion(int? id)
        {
            return PartialView("PredictedCIVQ");
        }

        public async Task<PartialViewResult> GetTagQuestion(int? id)
        {
            return PartialView("TaggedQuestions");
        }


    }

}

