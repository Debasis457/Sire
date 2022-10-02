using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Azure.Amqp.Framing;
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
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Sire.Web.Controllers
{
    [Authorize]
    public class OngoingInspectionController : Controller
    {
        private readonly ILogger<OngoingInspectionController> _logger;
        private readonly Microsoft.Extensions.Configuration.IConfiguration _iConfig;
        string apiBaseUrl = string.Empty;
        string apiBaseQuestionUrl = string.Empty;
        string apiBaseResponseUrl = string.Empty;
        string apiBaseQuestionResponseUrl = string.Empty;
        string apiBaseAssesorReviewerUrl = string.Empty;
        enum InspectionType : int { Standard = 0, Full = 1 };
        public OngoingInspectionController(ILogger<OngoingInspectionController> logger, IConfiguration iConfig)
        {
            _logger = logger;
            _iConfig = iConfig;

            apiBaseUrl = _iConfig.GetValue<string>("apiUrl:url").ToString() + "/Inspection";
        }
        [Route("OngoingInspection/id")]
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
                using (HttpClient client = new HttpClient())
                {
                    string endpoint = apiBaseUrl + "/" + operatorid + "/" + vesselId;
                    using (var Response = await client.GetAsync(endpoint))
                    {
                        if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            var InspectionData = JsonConvert.DeserializeObject<IEnumerable<InspectionDto>>(Response.Content.ReadAsStringAsync().Result);
                            var lastInspectionData = InspectionData.OrderByDescending(d => d.Started_At).FirstOrDefault();
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
                }

            }

            return View();
        }

        [ValidateAntiForgeryToken]
        public async Task<ActionResult> GoToInspections(int? Id, string IsAllowdForNew, string InspectionId)
        {
            var operatorid = Convert.ToInt32(HttpContext.Session.GetString("UserId"));
            var vesselId = Convert.ToInt32(TempData["vessselId"]);
            TempData.Keep();
            if (IsAllowdForNew == "true")
            {
                InspectionDto inspectionDto = new InspectionDto();
                inspectionDto.InspectionType = Id;
                inspectionDto.Vessel_Id = vesselId;
                inspectionDto.Operator_Id = operatorid;
                inspectionDto.Started_At = DateTime.Now;
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
                                using (var InspectionDtoData = await client.GetAsync(apiBaseUrl))
                                {
                                    var data = JsonConvert.DeserializeObject<List<InspectionDto>>(InspectionDtoData.Content.ReadAsStringAsync().Result);

                                    return RedirectToAction("", "InspectionQuestion", new { @id = adddata });
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
            }
            else
            {
                var lastInspectionId = Convert.ToInt32(string.IsNullOrEmpty(InspectionId) ? 0 : InspectionId);
                return RedirectToAction("", "InspectionQuestion", new { @id = lastInspectionId });
            }
            return View();
        }
    }
}
