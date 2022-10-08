using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Amqp.Framing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Sire.Data.Dto.Inspection;
using Sire.Data.Dto.Master;
using Sire.Data.Dto.Question;
using Sire.Data.Dto.ShipManagement;
using Sire.Data.Entities.Question;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Sire.Web.Controllers
{
    [Authorize]
    public class AssesorReviewerController : Controller
    {
        private readonly ILogger<AssesorReviewerController> _logger;
        private readonly Microsoft.Extensions.Configuration.IConfiguration _iConfig;
        string apiBaseUrl = string.Empty;
        string apiBaseUserlUrl = string.Empty;
        string apiBaseResponseUrl = string.Empty;
        string apiBaseTraningQuetionUrl = string.Empty;
        string apiAssesorUrl = string.Empty;
        string apiRankGroupUrl = string.Empty;

        public AssesorReviewerController(ILogger<AssesorReviewerController> logger,
            Microsoft.Extensions.Configuration.IConfiguration iConfig
            )
        {
            _logger = logger;
            _iConfig = iConfig;
            apiBaseUrl = _iConfig.GetValue<string>("apiUrl:url").ToString() + "/Question";
            apiBaseUserlUrl = _iConfig.GetValue<string>("apiUrl:url").ToString() + "/UserMaster";
            apiBaseTraningQuetionUrl = _iConfig.GetValue<string>("apiUrl:url").ToString() + "/trainingquestion";
            apiAssesorUrl = _iConfig.GetValue<string>("apiUrl:url").ToString() + "/AssesorReviewer";
            apiRankGroupUrl = _iConfig.GetValue<string>("apiUrl:url").ToString() + "/RankGroup";

        }

        public async Task<IActionResult> Index(int? inspectionid)
        
        {

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var endpoint = apiBaseTraningQuetionUrl;
                    using (var Response = await client.GetAsync(apiAssesorUrl))
                    {
                        if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            ViewBag.InspectionId = inspectionid;
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
        }


        [HttpPost]
        public async Task<IActionResult> AddEdit([FromBody]  Inspection_QuestionDto[] data)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
                    using (var Response = await client.PostAsync(apiAssesorUrl, content))
                    {
                        if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            ViewBag.IsEdit = false;
                            return Json(true);

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
        }

        public async Task<PartialViewResult> GetQuestionBySection(int? id, int? inspectionId)
        {
            var endquestion = apiBaseUrl + "/GetQuestionBySection/" + id;
            var enduser = apiBaseUserlUrl + "/GetUserDropDown";
            var endAssesor = apiRankGroupUrl + "/GetRankGroupDropDown";

            using (HttpClient client = new HttpClient())
            {
                using (var Response = await client.GetAsync(endAssesor))
                {
                    if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        ViewBag.IsEdit = true;
                        using (var IUserResponse = await client.GetAsync(endAssesor))
                        {
                            if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                            {
                                var UserData = JsonConvert.DeserializeObject<IEnumerable<DropDownDto>>(IUserResponse.Content.ReadAsStringAsync().Result);
                                ViewBag.User_Id = UserData;
                            }
                            else
                            {
                                ModelState.Clear();
                            }
                        }

                        // return View(data);
                    }
                    else
                    {
                        ModelState.Clear();
                        ModelState.AddModelError(string.Empty, "Invalid Data");
                    }
                }

                inspectionId = inspectionId == null ? 0 : inspectionId;
                var queendpoint = apiAssesorUrl + "/GetAssesordataByInspectionSeperate/" + id + "/" + inspectionId;
                using (var Response = await client.GetAsync(queendpoint))
                {
                    if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var result = Response.Content.ReadAsStringAsync().Result;
                        var data = JsonConvert.DeserializeObject<IEnumerable<Inspection_QuestionDto>>(Response.Content.ReadAsStringAsync().Result);
                        return PartialView("AssesorQuetionsList", data);
                    }
                    else
                    {
                        ModelState.Clear();
                        ModelState.AddModelError(string.Empty, "Data Not Found");
                        return PartialView();
                    }
                }
                return PartialView("AssesorQuetionsList", new Inspection_QuestionDto());
            }
        }
    }
}
