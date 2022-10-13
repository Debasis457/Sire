using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Sire.Data.Dto.Master;
using Sire.Data.Dto.Question;
using Sire.Data.Dto.Training;
using Sire.Web.Models;

namespace Sire.Web.Controllers
{
    public class TrainingController : Controller
    {
        private readonly ILogger<TrainingController> _logger;
        private readonly IConfiguration _iConfig;

        string apiBaseUrl = string.Empty;
        string apiBaseOperatorUrl = string.Empty;
        string apiBaseVesselUrl = string.Empty;
        string apiBaseQuestionUrl = string.Empty;
        string apiBaseTrainingQuestionUrl = string.Empty;
        string apiBaseTrainingResponseUrl = string.Empty;

        public TrainingController(ILogger<TrainingController> logger,
            IConfiguration iConfig
            )
        {
            _logger = logger;
            _iConfig = iConfig;
            apiBaseUrl = _iConfig.GetValue<string>("apiUrl:url").ToString() + "/training";
            apiBaseOperatorUrl = _iConfig.GetValue<string>("apiUrl:url").ToString() + "/operator";
            apiBaseVesselUrl = _iConfig.GetValue<string>("apiUrl:url").ToString() + "/vessel";
            apiBaseQuestionUrl = _iConfig.GetValue<string>("apiUrl:url").ToString() + "/Question";
            apiBaseTrainingQuestionUrl = _iConfig.GetValue<string>("apiUrl:url").ToString() + "/TrainingQuestion";
            apiBaseTrainingResponseUrl = _iConfig.GetValue<string>("apiUrl:url").ToString() + "/TraningResponse";
        }

        public async Task<IActionResult> Index(int? Id)
        {
            var enduser = apiBaseOperatorUrl + "/GetOperatorDropDown";
            var endvessel = apiBaseVesselUrl + "/GetVesselDropDown";
            if (Id == null)
            {
                ViewBag.IsEdit = false;

                using (HttpClient client = new())
                {
                    string endpoint = apiBaseUrl + "/" + Id;
                    using var Response = await client.GetAsync(endpoint);
                    if (Response.StatusCode == HttpStatusCode.OK)
                    {
                        ViewBag.IsEdit = true;

                        using var IUserResponse = await client.GetAsync(enduser);
                        if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            var OperatorData = JsonConvert.DeserializeObject<IEnumerable<DropDownDto>>(IUserResponse.Content.ReadAsStringAsync().Result);
                            ViewBag.Operator_Id = OperatorData;
                        }
                        else
                        {
                            ModelState.Clear();
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
            else
            {
                using HttpClient client = new();

                string endpoint = apiBaseUrl + "/" + Id;
                using (var Response = await client.GetAsync(endpoint))
                {
                    if (Response.StatusCode == HttpStatusCode.OK)
                    {
                        ViewBag.IsEdit = true;

                        var data = JsonConvert.DeserializeObject<TrainingDto>(Response.Content.ReadAsStringAsync().Result);

                        using var IOperatorResponse = await client.GetAsync(enduser);
                        if (Response.StatusCode == HttpStatusCode.OK)
                        {
                            var OperatorData = JsonConvert.DeserializeObject<IEnumerable<DropDownDto>>(IOperatorResponse.Content.ReadAsStringAsync().Result);
                            ViewBag.Operator_Id = OperatorData;
                        }
                        else
                        {
                            ModelState.Clear();
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
        }

        public async Task<IActionResult> TrainingList()
        {
            try
            {
                using HttpClient client = new();

                using var Response = await client.GetAsync(apiBaseUrl);
                if (Response.StatusCode == HttpStatusCode.OK)
                {
                    var data = JsonConvert.DeserializeObject<List<TrainingDto>>(Response.Content.ReadAsStringAsync().Result);

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
        }

        public async Task<JsonResult> GetVessel(int Id)
        {
            var endvessel = apiBaseVesselUrl + "/GetVesselbyOperator/" + Id;

            using HttpClient client = new();
            using var Response = await client.GetAsync(endvessel);
            var data = JsonConvert.DeserializeObject<List<VesselDto>>(Response.Content.ReadAsStringAsync().Result).ToList();
            return Json(data);
        }

        [HttpPost]
        public async Task<IActionResult> Index(TrainingDto trainingDto)
        {
            /* trainingDto.Operator_id = trainingDto.Id;
             trainingDto.Id = 0;*/
            var userid = Convert.ToInt32(HttpContext.Session.GetString("UserId"));
            trainingDto.Operator_id  = userid;
            var vesselid = Convert.ToInt32(HttpContext.Session.GetString("VesselId"));
            trainingDto.Vessel_Id = vesselid;
            try
            {
                using HttpClient client = new();

                var content = new StringContent(JsonConvert.SerializeObject(trainingDto), Encoding.UTF8, "application/json");

                using var Response = await client.PostAsync(apiBaseUrl, content);

                if (Response.StatusCode == HttpStatusCode.OK)
                {
                    var adddata = JsonConvert.DeserializeObject<int>(Response.Content.ReadAsStringAsync().Result);
                    ViewBag.IsEdit = false;
                    trainingDto = new TrainingDto();
                    using var TrainingData = await client.GetAsync(apiBaseUrl);
                    var data = JsonConvert.DeserializeObject<List<TrainingDto>>(TrainingData.Content.ReadAsStringAsync().Result);

                    return RedirectToAction("Index", "TrainingQuestion", new { @id = adddata });
                }
                else
                {
                    ViewBag.IsEdit = true;
                    ModelState.Clear();
                    ModelState.AddModelError(string.Empty, "Invalid Data");
                    return View();
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }

        public async Task<PartialViewResult> GetCheck()
        {
            var endquestion = apiBaseQuestionUrl + "/GetQuestion";

            using (HttpClient client = new HttpClient())
            {
                using (var Response = await client.GetAsync(endquestion))
                {
                    if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var result = Response.Content.ReadAsStringAsync().Result;

                        var data = JsonConvert.DeserializeObject<IEnumerable<QuestionDto>>(Response.Content.ReadAsStringAsync().Result);

                        return PartialView("Index", data);

                        //return View("Index", data);
                    }
                    else
                    {
                        ModelState.Clear();
                        ModelState.AddModelError(string.Empty, "Invalid Data");
                        return PartialView();
                    }

                    //  return PartialView("data");
                }
            }
        }

        public async Task<PartialViewResult> GetQuestionBySection(int? id, int? traningId)
        {
            var trainingNumber = traningId;
            var traineeId = Convert.ToInt32(HttpContext.Session.GetString("UserId"));
            var endquestion = apiBaseQuestionUrl + "/GetQuestionBySection/" + id;
            var taskSubmittedDataUrl = apiBaseTrainingResponseUrl + "/GetTriningResponseByTraning/" + trainingNumber;
            ViewBag.TrainingId = traningId;
            QuestionTrainingModel taskModel = new()
            {
                QuestionDtos = new List<QuestionDto>()
            };

            using (HttpClient client = new())
            {
                using var Response = await client.GetAsync(endquestion);
                if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var result = Response.Content.ReadAsStringAsync().Result;

                    if (result.Length > 2)
                    {
                        var data = JsonConvert.DeserializeObject<IEnumerable<QuestionDto>>(result);
                        taskModel.QuestionDtos = data;
                        //dharini
                        TempData["QuestionIdsBySection"] = string.Join(",", data.Select(x => x.Id).ToArray());

                        using var Response1 = await client.GetAsync(taskSubmittedDataUrl);
                        if (Response1.StatusCode == HttpStatusCode.OK)
                        {
                            string traningResponse = Response1.Content.ReadAsStringAsync().Result;
                            if (traningResponse.Length > 2)
                            {
                                var traningResponseData = JsonConvert.DeserializeObject<IList<TraningResponseDto>>(traningResponse);
                                taskModel.TraningResponseDtos = traningResponseData.Where(d => d.Trainee_Id == traineeId).ToList();
                            }
                        }
                    }
                }
                else
                {
                    ModelState.Clear();
                    ModelState.AddModelError(string.Empty, "Invalid Data");
                    return PartialView();
                }
            }

            return PartialView("_TrainingQuestion", taskModel);
        }

        public async Task<PartialViewResult> GetApplicableQuestionBySection(int? id, int? trainingId)
        {
            var traningId = trainingId ??= 0;
            var traineeId = Convert.ToInt32(HttpContext.Session.GetString("UserId"));
            var assessorId = Convert.ToInt32(HttpContext.Session.GetString("UserId"));
            var reviewerId = Convert.ToInt32(HttpContext.Session.GetString("UserId"));
            var vesselId = Convert.ToInt32(HttpContext.Session.GetString("VesselId"));
            var endquestion = $"{apiBaseTrainingQuestionUrl}/GetApplicationQuestionsBySection/{id}/{assessorId}/{reviewerId}/{traningId}/{vesselId}/{traineeId}";
            var taskSubmittedDataUrl = apiBaseTrainingResponseUrl + "/GetTriningResponseByTraning/" + traningId;

            ViewBag.TrainingId = trainingId;
            QuestionTrainingModel taskModel = new()
            {
                QuestionDtos = new List<QuestionDto>()
            };

            using HttpClient client = new();
            using var response = await client.GetAsync(endquestion);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                if (result.Length > 2)
                {
                    var data = JsonConvert.DeserializeObject<IEnumerable<QuestionDto>>(result);
                    taskModel.QuestionDtos = data;
                    //dharini
                    TempData["QuestionIdsBySection"] = string.Join(",", data.Select(x => x.Id).ToArray());

                    using var response1 = await client.GetAsync(taskSubmittedDataUrl);
                    if (response1.StatusCode == HttpStatusCode.OK)
                    {
                        string traningResponse = response1.Content.ReadAsStringAsync().Result;
                        if (traningResponse.Length > 2)
                        {
                            var traningResponseData = JsonConvert.DeserializeObject<IList<TraningResponseDto>>(traningResponse);
                            taskModel.TraningResponseDtos = traningResponseData.Where(d => d.Trainee_Id == traineeId).ToList();
                        }
                    }
                }
            }
            else
            {
                ModelState.Clear();
                ModelState.AddModelError(string.Empty, "Invalid Data");
                return PartialView();
            }

            return PartialView("_TrainingQuestion", taskModel);
        }

        public async Task<PartialViewResult> GetRankBasedQuestionsBySection(int? id, int? rankGroupId, int? trainingId)
        {
            var userId = Convert.ToInt32(HttpContext.Session.GetString("UserId"));
            var endquestion = $"{apiBaseTrainingQuestionUrl}/GetRankBasedQuestionsBySection/{id}/{rankGroupId}/{trainingId}/{userId}";
            var taskSubmittedDataUrl = apiBaseTrainingResponseUrl + "/GetTriningResponseByTraning/" + trainingId;

            ViewBag.TrainingId = trainingId;
            QuestionTrainingModel taskModel = new()
            {
                QuestionDtos = new List<QuestionDto>()
            };

            using HttpClient client = new();
            using var response = await client.GetAsync(endquestion);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                if (result.Length > 2)
                {
                    var data = JsonConvert.DeserializeObject<IEnumerable<QuestionDto>>(result);
                    taskModel.QuestionDtos = data;
                    //dharini
                    TempData["QuestionIdsBySection"] = string.Join(",", data.Select(x => x.Id).ToArray());

                    using var response1 = await client.GetAsync(taskSubmittedDataUrl);
                    if (response1.StatusCode == HttpStatusCode.OK)
                    {
                        string traningResponse = response1.Content.ReadAsStringAsync().Result;
                        if (traningResponse.Length > 2)
                        {
                            var traningResponseData = JsonConvert.DeserializeObject<IList<TraningResponseDto>>(traningResponse);
                            taskModel.TraningResponseDtos = traningResponseData.Where(d => d.Trainee_Id == userId).ToList();
                        }
                    }
                }
            }
            else
            {
                ModelState.Clear();
                ModelState.AddModelError(string.Empty, "Invalid Data");
                return PartialView();
            }

            return PartialView("_TrainingQuestion", taskModel);
        }

        public async Task<IActionResult> Delete(int Id)
        {
            string endpoint = apiBaseUrl + "/" + Id;
            using (HttpClient client = new())
            {
                using var UserData = await client.DeleteAsync(endpoint);
                var data = JsonConvert.DeserializeObject<IEnumerable<TrainingDto>>(UserData.Content.ReadAsStringAsync().Result);
                using var Response = await client.GetAsync(apiBaseUrl);
                if (Response.StatusCode == HttpStatusCode.OK)
                {
                    var getall = JsonConvert.DeserializeObject<List<TrainingDto>>(Response.Content.ReadAsStringAsync().Result);

                    //return View(getall);
                    return View("TrainingList", getall);
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
}
