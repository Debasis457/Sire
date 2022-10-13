using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Sire.Data.Dto.Master;
using Sire.Data.Dto.Question;

namespace Sire.Web.Controllers
{
    [Authorize]
    public class TrainingQuestionController : Controller
    {
        private readonly ILogger<TrainingQuestionController> _logger;
        private readonly IConfiguration _iConfig;

        readonly string apiBaseUrl = string.Empty;
        readonly string apiBaseQuestionUrl = string.Empty;
        readonly string apiBaseUserRankUrl = string.Empty;
        readonly string apiBaseRankGroupUrl = string.Empty;
        readonly string apiBaseTrainingUrl = string.Empty;

        public TrainingQuestionController(ILogger<TrainingQuestionController> logger, IConfiguration iConfig)
        {
            _logger = logger;
            _iConfig = iConfig;
            apiBaseUrl = _iConfig.GetValue<string>("apiUrl:url").ToString() + "/trainingquestion";
            apiBaseQuestionUrl = _iConfig.GetValue<string>("apiUrl:url").ToString() + "/question";
            apiBaseUserRankUrl = _iConfig.GetValue<string>("apiUrl:url").ToString() + "/User_Rank";
            apiBaseRankGroupUrl = _iConfig.GetValue<string>("apiUrl:url").ToString() + "/RankGroup";
            apiBaseTrainingUrl = _iConfig.GetValue<string>("apiUrl:url").ToString() + "/Training";
        }
        //Dharini

        public async Task<IActionResult> Index(int? id)
        {
            TempData["TrainingId"] = id;
            ViewBag.TrainingId = id == null ? 0 : id;
            id = id == null ? 0 : id;
            var vesselId = Convert.ToInt32(TempData["vessselId"]);
            try
            {
                using HttpClient client = new();
                var userid = Convert.ToInt32(HttpContext.Session.GetString("UserId"));
                ViewBag.OperatorId = userid;
                var endpoint = apiBaseUrl + "/" + id + "/" + userid;

                using var Response = await client.GetAsync(endpoint);
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
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return View();
        }

        [Route("TrainingQuestion/GetDetails/{trainingId}/{questionId}")]
        public async Task<IActionResult> GetDetails(int? trainingId, int? questionId)
        {
            ViewBag.TrainingId = trainingId == null ? 0 : trainingId;
            TempData["TrainingId"] = trainingId;
            TempData["QuestionId"] = questionId;
            //trainingId = (int) TempData["TrainingId"];
            var endquestion = apiBaseQuestionUrl + "/" + questionId;

            using HttpClient client = new();
            using var Response = await client.GetAsync(endquestion);
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

        public async Task<PartialViewResult> GetQuestion(int? id)
        {
            TempData["TrainingId"] = id;
            id = id == null ? 0 : id;
            try
            {
                using HttpClient client = new();
                var userid = Convert.ToInt32(HttpContext.Session.GetString("UserId"));
                var endpoint = apiBaseUrl + "/" + id + "/" + userid;

                using var Response = await client.GetAsync(endpoint);
                if (Response.StatusCode == HttpStatusCode.OK)
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
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }

        public async Task<PartialViewResult> GetRenkBaseQuestion(int? id, int? rankGroupId)
        {
            id ??= 0;
            TempData["TrainingId"] = id;

            rankGroupId ??= Convert.ToInt32(HttpContext.Session.GetString("RankGroupId"));
            ViewBag.TrainingId = id;
            ViewBag.RankGroupId = rankGroupId;

            id = id == null ? 0 : id;
            var endUserRank = apiBaseRankGroupUrl + "/GetRankGroupDropDown";
            try
            {
                using HttpClient client = new();
                var userid = Convert.ToInt32(HttpContext.Session.GetString("UserId"));
                var endpoint = $"{apiBaseUrl}/GetSectionListRankBasedQuestions/{rankGroupId}/{id}/{userid}";

                using var Response = await client.GetAsync(endpoint);
                if (Response.StatusCode == HttpStatusCode.OK)
                {
                    var data = JsonConvert.DeserializeObject<List<QuetionSectionDto>>(Response.Content.ReadAsStringAsync().Result);
                    using (var IUserResponse = await client.GetAsync(endUserRank))
                    {
                        if (Response.StatusCode == HttpStatusCode.OK)
                        {
                            var RankData = JsonConvert.DeserializeObject<IEnumerable<DropDownDto>>(IUserResponse.Content.ReadAsStringAsync().Result);
                            
                            var RankGroupListData = new List<SelectListItem>();
                            foreach (var rankGroup in RankData)
                            {
                                RankGroupListData.Add(new SelectListItem()
                                {
                                    Text = rankGroup.Value,
                                    Value = rankGroup.Id.ToString(),
                                    Selected = rankGroup.Id == rankGroupId
                                });
                            }
                            //ViewBag.RankGroupData = RankGroupListData;
                            ViewBag.Rank = RankGroupListData;
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
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }

        public async Task<JsonResult> GetQuestionByRank(int Id)
        {
            var endvessel = apiBaseQuestionUrl + "/GetQuestionsByRankId/" + Id;

            using HttpClient client = new();
            using var Response = await client.GetAsync(endvessel);

            var data = JsonConvert.DeserializeObject<List<QuestionDto>>(Response.Content.ReadAsStringAsync().Result);
            
            return Json(data);
        }

        public async Task<JsonResult> GetDifference(int? id)
        {
            TempData["TrainingId"] = id;
            var userid = Convert.ToInt32(HttpContext.Session.GetString("UserId"));
            var endvessel = apiBaseTrainingUrl + "/GetDifference/" + userid;

            using HttpClient client = new();
            using var Response = await client.GetAsync(endvessel);
            var data = JsonConvert.DeserializeObject<int>(Response.Content.ReadAsStringAsync().Result);
            //var data = JsonConvert.DeserializeObject<TrainingDto>(Response.Content.ReadAsStringAsync().Result);

            return Json(data);
        }

        public async Task<PartialViewResult> GetApplicableQuestions(int? id)
        {
            TempData["TrainingId"] = id;
            id = id == null ? 0 : id;
            var assessorId = Convert.ToInt32(HttpContext.Session.GetString("UserId"));
            var reviewerId = Convert.ToInt32(HttpContext.Session.GetString("UserId"));
            var userId = Convert.ToInt32(HttpContext.Session.GetString("UserId"));
            var vesselId = Convert.ToInt32(HttpContext.Session.GetString("VesselId"));

            try
            {
                using HttpClient client = new();
                var userid = Convert.ToInt32(HttpContext.Session.GetString("UserId"));
                var endpoint = apiBaseUrl + "/GetSectionListApplicableQuestions/" + assessorId + "/" + reviewerId + "/" + id + "/" + vesselId + "/" + userid;

                using var Response = await client.GetAsync(endpoint);
                if (Response.StatusCode == HttpStatusCode.OK)
                {
                    var data = JsonConvert.DeserializeObject<List<QuetionSectionDto>>(Response.Content.ReadAsStringAsync().Result);
                    return PartialView("ApplicableQuestions", data);
                }
                else
                {
                    ModelState.Clear();
                    ModelState.AddModelError(string.Empty, "Invalid Data");
                    return PartialView();
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
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
