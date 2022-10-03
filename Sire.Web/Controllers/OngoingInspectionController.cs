using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Sire.Data.Dto.Inspection;
using Sire.Data.Dto.Question;

namespace Sire.Web.Controllers
{
    [Authorize]
    public class OngoingInspectionController : Controller
    {
        private readonly ILogger<OngoingInspectionController> _logger;
        private readonly IConfiguration _iConfig;
        string apiBaseUrl = string.Empty;
        string apiBaseVesselUrl = string.Empty;
        string apiQuestionUrl = string.Empty;
        string apiAssesorReviewerUrl = string.Empty;

        enum InspectionType : int { Standard = 0, Full = 1 };

        public OngoingInspectionController(ILogger<OngoingInspectionController> logger, IConfiguration iConfig)
        {
            _logger = logger;
            _iConfig = iConfig;

            apiBaseUrl = _iConfig.GetValue<string>("apiUrl:url").ToString() + "/Inspection";
            apiBaseVesselUrl = _iConfig.GetValue<string>("apiUrl:url").ToString() + "/Vessel";
            apiQuestionUrl = _iConfig.GetValue<string>("apiUrl:url").ToString() + "/Question";
            apiAssesorReviewerUrl = _iConfig.GetValue<string>("apiUrl:url").ToString() + "/AssesorReviewer";
        }
        
        public async Task<IActionResult> Index()
        {
            ViewBag.EnumList = from InspectionType e in Enum.GetValues(typeof(InspectionType))
                               select new
                               {
                                   Id = (int)e,
                                   Name = e.ToString()
                               };

            var vesselId = Convert.ToInt32(TempData["vessselId"]);
            TempData.Keep();
            var operatorid = Convert.ToInt32(HttpContext.Session.GetString("UserId"));
            if (vesselId > 0)
            {
                using HttpClient client = new();
                string endpoint = apiBaseUrl + "/" + operatorid + "/" + vesselId;
                using var Response = await client.GetAsync(endpoint);
                if (Response.StatusCode == HttpStatusCode.OK)
                {
                    var inspectionData = JsonConvert.DeserializeObject<IEnumerable<InspectionDto>>(Response.Content.ReadAsStringAsync().Result);
                    var lastInspectionData = inspectionData.OrderByDescending(d => d.Started_At).FirstOrDefault();
                    if (lastInspectionData != null)
                    {
                        return View(lastInspectionData);
                    }
                }
                else
                {
                    ModelState.Clear();
                    ModelState.AddModelError(string.Empty, "Invalid Data");
                    return View();
                }
            }

            return View();
        }

        [ValidateAntiForgeryToken]
        public async Task<ActionResult> GoToInspections(int? Id, bool? IsAllowdForNew, int? InspectionId)
        {
            var userId = Convert.ToInt32(HttpContext.Session.GetString("UserId"));
            var rankGroupId = Convert.ToInt32(HttpContext.Session.GetString("RankGroupId"));
            var vesselId = Convert.ToInt32(TempData["vessselId"]);
            TempData.Keep();
            if (IsAllowdForNew == true)
            {
                InspectionDto inspectionDto = new()
                {
                    InspectionType = Id,
                    Vessel_Id = vesselId,
                    Operator_Id = userId,
                    Started_At = DateTime.Now
                };

                try
                {
                    using HttpClient client = new();

                    using var questionsResponse = await client.GetAsync(apiQuestionUrl + "/GetQuestionsByRankId/" + rankGroupId);
                    if (questionsResponse.StatusCode == HttpStatusCode.OK)
                    {
                        var questionsData = JsonConvert.DeserializeObject<List<QuestionDto>>(questionsResponse.Content.ReadAsStringAsync().Result);

                        var inspectionContent = new StringContent(JsonConvert.SerializeObject(inspectionDto), Encoding.UTF8, "application/json");
                        using var inspectionResponse = await client.PostAsync(apiBaseUrl, inspectionContent);
                        if (inspectionResponse.StatusCode == HttpStatusCode.OK)
                        {
                            var newInspectionId = JsonConvert.DeserializeObject<int>(inspectionResponse.Content.ReadAsStringAsync().Result);

                            var inspectionQuestions = new List<Inspection_QuestionDto>();
                            foreach (var item in questionsData)
                            {
                                inspectionQuestions.Add(new Inspection_QuestionDto
                                {
                                    Inspection_Id = newInspectionId,
                                    Question_Id = item.Id,
                                    Assessor_Id = item.DAssessore,
                                    Reviewer_Id = item.DReviewer
                                });
                            }

                            var inspectionQuestionsContent = new StringContent(JsonConvert.SerializeObject(inspectionQuestions), Encoding.UTF8, "application/json");
                            var inspectionQuestionsInsertResponse = await client.PostAsync(apiAssesorReviewerUrl, inspectionQuestionsContent);
                            if (inspectionQuestionsInsertResponse.StatusCode == HttpStatusCode.OK)
                            {
                                return RedirectToAction(string.Empty, "InspectionQuestion", new { @id = newInspectionId });
                            }
                        }
                        else
                        {
                            ModelState.Clear();
                            ModelState.AddModelError(string.Empty, "Invalid Data");
                            return View();
                        }
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
            }
            else
            {
                return RedirectToAction("", "InspectionQuestion", new { @id = InspectionId });
            }

            return View();
        }
    }
}
