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
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace Sire.Web.Controllers
{
    [Authorize]
    public class InspectionController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ILogger<InspectionController> _logger;
        private readonly Microsoft.Extensions.Configuration.IConfiguration _iConfig;
        string apiBaseUrl = string.Empty;
        string apiBaseOperatorUrl = string.Empty;
        string apiBaseVesselUrl = string.Empty;
        string apiBaseQuestionUrl = string.Empty;
        string apiBaseAssesorReviewerUrl = string.Empty;

        public InspectionController(
            ILogger<InspectionController> logger,
            IMapper mapper,
            IConfiguration iConfig)
        {
            _logger = logger;
            _mapper = mapper;
            _iConfig = iConfig;
            apiBaseUrl = _iConfig.GetValue<string>("apiUrl:url").ToString() + "/Inspection";
            apiBaseOperatorUrl = _iConfig.GetValue<string>("apiUrl:url").ToString() + "/operator";
            apiBaseVesselUrl = _iConfig.GetValue<string>("apiUrl:url").ToString() + "/vessel";
            apiBaseQuestionUrl = _iConfig.GetValue<string>("apiUrl:url").ToString() + "/Question";
            apiBaseAssesorReviewerUrl = _iConfig.GetValue<string>("apiUrl:url").ToString() + "/AssesorReviewer";
        }

        public async Task<IActionResult> Index(int? Id)
        {
            var enduser = apiBaseOperatorUrl + "/GetOperatorDropDown";
            var endvessel = apiBaseVesselUrl + "/GetVesselDropDown";
            if (Id == null)
            {
                ViewBag.IsEdit = false;

                using (HttpClient client = new HttpClient())
                {
                    string endpoint = apiBaseUrl + "/" + Id;
                    using (var Response = await client.GetAsync(endpoint))
                    {
                        if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            ViewBag.IsEdit = true;


                            using (var IUserResponse = await client.GetAsync(enduser))
                            {
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

                        }
                        else
                        {
                            ModelState.Clear();
                            ModelState.AddModelError(string.Empty, "Invalid Data");
                            return View();
                        }
                    }
                }


                return View();
            }
            else
            {

                using (HttpClient client = new HttpClient())
                {

                    string endpoint = apiBaseUrl + "/" + Id;
                    using (var Response = await client.GetAsync(endpoint))
                    {
                        if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            ViewBag.IsEdit = true;

                            var data = JsonConvert.DeserializeObject<InspectionDto>(Response.Content.ReadAsStringAsync().Result);

                            using (var IOperatorResponse = await client.GetAsync(enduser))
                            {
                                if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                                {
                                    var OperatorData = JsonConvert.DeserializeObject<IEnumerable<DropDownDto>>(IOperatorResponse.Content.ReadAsStringAsync().Result);
                                    ViewBag.Operator_Id = OperatorData;
                                }
                                else
                                {
                                    ModelState.Clear();
                                }
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
        }


        public async Task<JsonResult> GetVessel(int Id)
        {
            var endvessel = apiBaseVesselUrl + "/GetVesselbyOperator/" + Id;

            using (HttpClient client = new HttpClient())
            {
                using (var Response = await client.GetAsync(endvessel))
                {

                    var data = JsonConvert.DeserializeObject<List<VesselDto>>(Response.Content.ReadAsStringAsync().Result).ToList();
                    return Json(data);

                }
            }

        }

        [HttpPost]
        public async Task<IActionResult> AddInspection(InspectionDto inspectionDto)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {

                    StringContent content = new StringContent(JsonConvert.SerializeObject(inspectionDto), Encoding.UTF8, "application/json");

                    using (var Response = await client.PostAsync(apiBaseUrl, content))
                    {
                        var adddata = JsonConvert.DeserializeObject<int>(Response.Content.ReadAsStringAsync().Result);
                        if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            ViewBag.IsEdit = false;
                            inspectionDto = new InspectionDto();
                            using (var InspectionDtoData = await client.GetAsync(apiBaseUrl))
                            {
                                var data = JsonConvert.DeserializeObject<List<InspectionDto>>(InspectionDtoData.Content.ReadAsStringAsync().Result);

                                return RedirectToAction("Index", "AssesorReviewer", new { @inspectionid = adddata });
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
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return View();
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

        public async Task<PartialViewResult> GetQuestionBySection(int? id, int? inspectionId = 1)
        {
            var endquestion = apiBaseQuestionUrl + "/GetQuestionBySection/" + id;
            var inspectionQuestion = apiBaseAssesorReviewerUrl + "/GetAssesordataByInspection/" + inspectionId + "/" + id;

            using (HttpClient client = new HttpClient())
            {
                using (var Response = await client.GetAsync(endquestion))
                {
                    if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var inspectionQuestionDtoModel = new List<InspectionQuestionDtoModel>();
                        var result = Response.Content.ReadAsStringAsync().Result;

                        var data = JsonConvert.DeserializeObject<IEnumerable<QuestionDto>>(Response.Content.ReadAsStringAsync().Result);

                        using (var inspectionQuestionResponse = await client.GetAsync(inspectionQuestion))
                        {
                            if (inspectionQuestionResponse.StatusCode == System.Net.HttpStatusCode.OK)
                            {
                                var inspectionQuestionResult = inspectionQuestionResponse.Content.ReadAsStringAsync().Result;
                                var inspectionQuestionData = JsonConvert.DeserializeObject<IEnumerable<Inspection_QuestionDto>>(inspectionQuestionResult);

                                foreach (var item in inspectionQuestionData)
                                {
                                    var eachQuestion = data.FirstOrDefault(x => x.Id == item.Question_Id);
                                    if (eachQuestion != null)
                                    {
                                        var mappedInspectionQuestion = _mapper.Map<InspectionQuestionDtoModel>(eachQuestion);
                                        mappedInspectionQuestion.InspectionQuestionId = item.Id;
                                        mappedInspectionQuestion.AssesmentCompleted = item.Assesment_Completed;
                                        mappedInspectionQuestion.ReviewCompleted = item.Review_Completed;

                                        inspectionQuestionDtoModel.Add(mappedInspectionQuestion);
                                    }
                                }
                                //data = data.Where(x => inspectionQuestionData.Any(b => b.Question_Id == x.Id));
                            }
                        }

                        return PartialView("_InspectionQuetions", inspectionQuestionDtoModel);
                        //return View("Index", data);
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



        [HttpPost]
        public async Task<IActionResult> Index(InspectionDto inspectionDto)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {

                    StringContent content = new StringContent(JsonConvert.SerializeObject(inspectionDto), Encoding.UTF8, "application/json");

                    using (var Response = await client.PostAsync(apiBaseUrl, content))
                    {
                        var adddata = JsonConvert.DeserializeObject<int>(Response.Content.ReadAsStringAsync().Result);
                        if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            ViewBag.IsEdit = false;
                            inspectionDto = new InspectionDto();
                            using (var InspectionDtoData = await client.GetAsync(apiBaseUrl))
                            {
                                var data = JsonConvert.DeserializeObject<List<InspectionDto>>(InspectionDtoData.Content.ReadAsStringAsync().Result);

                                return RedirectToAction("Index", "InspectionQuestion", new { @id = adddata });


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
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return View();
        }
    }
}
