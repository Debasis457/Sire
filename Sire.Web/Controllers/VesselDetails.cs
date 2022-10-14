using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Sire.Data.Dto.Master;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Sire.Data.Dto.Inspection;
using Microsoft.EntityFrameworkCore;
using System.Text;
using Sire.Data.Dto.Question;
using System.Net;
using System;
using Sire.Data.Dto.Training;
using Microsoft.Azure.Amqp.Framing;

namespace Sire.Web.Controllers
{
    [Authorize]
    public class VesselDetails : Controller
    {

        private readonly ILogger<VesselController> _logger;
        private readonly Microsoft.Extensions.Configuration.IConfiguration _iConfig;
        string apiBaseUrl = string.Empty;
        string apiQuestionUrl = string.Empty;
        string apiTrainingUrl = string.Empty;
        public VesselDetails(ILogger<VesselController> logger,
            Microsoft.Extensions.Configuration.IConfiguration iConfig
            )
        {
            _logger = logger;
            _iConfig = iConfig;
            apiBaseUrl = _iConfig.GetValue<string>("apiUrl:url").ToString() + "/Vessel";
            apiQuestionUrl = _iConfig.GetValue<string>("apiUrl:url").ToString() + "/Question";
            apiTrainingUrl = _iConfig.GetValue<string>("apiUrl:url").ToString() + "/Training";
        }

        public async Task<IActionResult> Index()
        {
            using (HttpClient client = new HttpClient())
            {
                string endpoint = apiBaseUrl + "/" + this.HttpContext.Session.GetString("VesselId"); ;
                using (var Response = await client.GetAsync(endpoint))
                {
                    var data = JsonConvert.DeserializeObject<VesselDto>(Response.Content.ReadAsStringAsync().Result);
                    return View(data);

                }
            }

        }


        public async Task<IActionResult> OngoingTraining(int id)
        {
            var userid = Convert.ToInt32(HttpContext.Session.GetString("UserId"));
            id = userid;
            using (HttpClient client = new HttpClient())
            {
                string endpoint = apiTrainingUrl + "/GetLastTrainingID/" + id;
                using (var Response = await client.GetAsync(endpoint))
                {
                    var data = JsonConvert.DeserializeObject<TrainingDto>(Response.Content.ReadAsStringAsync().Result);
                    var lastdata= data.Id;
                    ViewBag.lasttrainingid = lastdata;
                   
                    return RedirectToAction("Index", "TrainingQuestion", new { @id = lastdata });
                }
            }
            return View();

        }

        public async Task<PartialViewResult> GetTrainingList()
        {

            using (HttpClient client = new HttpClient())
            {
                var endpoint = apiTrainingUrl + "/GetStatus/" + this.HttpContext.Session.GetString("UserId"); ;
                using (var Response = await client.GetAsync(endpoint))
                {
                    if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {

                        var data = JsonConvert.DeserializeObject<List<TrainingDto>>(Response.Content.ReadAsStringAsync().Result);


                        return PartialView("TrainingStatus", data);
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
/*
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> GoToTraining(int? Id, bool? IsAllowdForNew, int? TrainingId)
        {
            var userId = Convert.ToInt32(HttpContext.Session.GetString("UserId"));
            var rankGroupId = Convert.ToInt32(HttpContext.Session.GetString("RankGroupId"));
            var vesselId = Convert.ToInt32(TempData["vessselId"]);
            TempData.Keep();
            if (IsAllowdForNew == true)
            {
                TrainingDto trainingDto = new()
                {

                    Vessel_Id = vesselId,
                    Operator_id = userId,
                    Started_at = DateTime.Now
                };

                try
                {
                    using HttpClient client = new();

                    using var questionsResponse = await client.GetAsync(apiQuestionUrl + "/GetQuestionsByRankId/" + rankGroupId);
                    if (questionsResponse.StatusCode == HttpStatusCode.OK)
                    {
                        var questionsData = JsonConvert.DeserializeObject<List<QuestionDto>>(questionsResponse.Content.ReadAsStringAsync().Result);

                        var trainingContent = new StringContent(JsonConvert.SerializeObject(trainingDto), Encoding.UTF8, "application/json");
                        using var trainingResponse = await client.PostAsync(apiBaseUrl, trainingContent);
                        if (trainingResponse.StatusCode == HttpStatusCode.OK)
                        {
                            var newTrainingId = JsonConvert.DeserializeObject<int>(trainingResponse.Content.ReadAsStringAsync().Result);

                            var trainingQuestions = new List<TraningResponseDto>();
                            foreach (var item in questionsData)
                            {
                                trainingQuestions.Add(new TraningResponseDto
                                {
                                    Training_Id = newTrainingId,
                                    Question_Id = item.Id,


                                });
                            }

                            var trainingQuestionsContent = new StringContent(JsonConvert.SerializeObject(trainingQuestions), Encoding.UTF8, "application/json");
                            var trainingQuestionsInsertResponse = await client.PostAsync(apiTrainingUrl, trainingQuestionsContent);
                            if (trainingQuestionsInsertResponse.StatusCode == HttpStatusCode.OK)
                            {
                                return RedirectToAction(string.Empty, "TrainingQuestion", new { @id = newTrainingId });
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
                return RedirectToAction("", "TrainingQuestion", new { @id = TrainingId });
            }

            return View();
        }
*/
    }
}