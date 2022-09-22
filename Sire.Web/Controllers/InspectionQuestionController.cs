using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Sire.Data.Dto.Inspection;
using Sire.Data.Dto.Master;
using Sire.Data.Dto.Question;
using Sire.Web.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Sire.Web.Controllers
{
    [Authorize]
    public class InspectionQuestionController : Controller
    {
        private readonly ILogger<FleetController> _logger;
        private readonly IMapper _mapper;
        private readonly IConfiguration _iConfig;
        string apiBaseUrl = string.Empty;
        string apiBaseUserUrl = string.Empty;
        string apiBaseQuestionUrl = string.Empty;
        string apiBaseInspectionUrl = string.Empty;

        public InspectionQuestionController(ILogger<FleetController> logger, IMapper mapper, IConfiguration iConfig)
        {
            _logger = logger;
            _mapper = mapper;
            _iConfig = iConfig;
            apiBaseUrl = _iConfig.GetValue<string>("apiUrl:url").ToString() + "/AssesorReviewer";
            apiBaseQuestionUrl = _iConfig.GetValue<string>("apiUrl:url").ToString() + "/Question";
            apiBaseInspectionUrl = _iConfig.GetValue<string>("apiUrl:url").ToString() + "/Inspection";
        }

        public async Task<IActionResult> Index(int? id = 1)
        {
            var inspectionQuestionSectionModel = new InspectionQuestionSectionModel();
            try
            {
                using HttpClient client = new();
                using (var inspectionResponse = await client.GetAsync(apiBaseInspectionUrl + "/" + id))
                {
                    if (inspectionResponse.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var data = JsonConvert.DeserializeObject<InspectionDto>(inspectionResponse.Content.ReadAsStringAsync().Result);
                        inspectionQuestionSectionModel.InspectionDto = data;

                        TempData["InspectionCompleted"] = data.Completed_At != DateTime.MinValue;
                    }
                }

                var url = apiBaseUrl + "/GetSectionListByInspectionId" + "/" + id;
                using var Response = await client.GetAsync(url);
                if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var data = JsonConvert.DeserializeObject<List<QuetionSectionDto>>(Response.Content.ReadAsStringAsync().Result);
                    inspectionQuestionSectionModel.QuetionSectionDtos = data;

                    return View(inspectionQuestionSectionModel);
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

        public async Task<IActionResult> AddEdit(int? id)
        {
            var inspectionQuestionUrl = apiBaseUrl + "/GetInspectionQuestion" + "/" + id;
            try
            {
                using HttpClient client = new();
                using var inspectionQuestionResponse = await client.GetAsync(inspectionQuestionUrl);
                if (inspectionQuestionResponse.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var inspectionQuestionData = JsonConvert.DeserializeObject<Inspection_QuestionDto>(inspectionQuestionResponse.Content.ReadAsStringAsync().Result);

                    var questionUrl = apiBaseQuestionUrl + "/" + inspectionQuestionData.Question_Id;
                    using var questionResponse = await client.GetAsync(questionUrl);
                    if (questionResponse.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var questionResponseData = JsonConvert.DeserializeObject<QuestionDto>(questionResponse.Content.ReadAsStringAsync().Result);
                        var data = _mapper.Map<InspectionQuestionDtoModel>(questionResponseData);
                        data.InspectionQuestionId = inspectionQuestionData.Id;

                        return View("~/Views/InspectionFlow/Index.cshtml", data);
                    }
                }

                ModelState.Clear();
                ModelState.AddModelError(string.Empty, "Invalid Data");
                return View();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }

        public async Task<IActionResult> GetDetails(int? id)
        {
            var endquestion = apiBaseQuestionUrl + "/" + id;

            using (HttpClient client = new HttpClient())
            {
                using (var Response = await client.GetAsync(endquestion))
                {
                    if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var result = Response.Content.ReadAsStringAsync().Result;

                        var data = JsonConvert.DeserializeObject<QuestionDto>(Response.Content.ReadAsStringAsync().Result);

                        return View("~/Views/InspectionFlow/Index.cshtml", data);
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


        [HttpGet]
        public async Task<IActionResult> CompleteInspection(int id)
        {
            using var client = new HttpClient();
            using var inspectionResponse = await client.GetAsync(apiBaseInspectionUrl + "/" + id);
            if (inspectionResponse.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var data = JsonConvert.DeserializeObject<InspectionDto>(inspectionResponse.Content.ReadAsStringAsync().Result);
                data.Completed_At = DateTime.Now;

                var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
                using var inspectionUpdateResponse = await client.PostAsync(apiBaseInspectionUrl, content);

                //return View();
                return RedirectToAction("Index", "Dashboard");
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
