using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Amqp.Framing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Sire.Data.Dto.Master;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Sire.Web.Controllers
{
    public class VesselPopUpController : Controller
    {
        private readonly ILogger<UserVesselController> _logger;
        private readonly Microsoft.Extensions.Configuration.IConfiguration _iConfig;
        string apiBaseUrl = string.Empty;
        string apiBaseUserUrl = string.Empty;
        string apiBaseVesselUrl = string.Empty;
        string apiBaseOperatorVesselUrl = string.Empty;

        public VesselPopUpController(ILogger<UserVesselController> logger,
            Microsoft.Extensions.Configuration.IConfiguration iConfig
            )
        {
            _logger = logger;
            _iConfig = iConfig;

            apiBaseVesselUrl = _iConfig.GetValue<string>("apiUrl:url").ToString() + "/Vessel";
            apiBaseOperatorVesselUrl = _iConfig.GetValue<string>("apiUrl:url").ToString() + "/User_Vessel";
        }

        public async Task<IActionResult> SelectVessel()
        {

            using (HttpClient client = new HttpClient())
            {

                var endvessel = apiBaseVesselUrl + "/GetVesselDropDown";

                using (var IUserResponse = await client.GetAsync(endvessel))
                {
                    if (IUserResponse.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var VesselData = JsonConvert.DeserializeObject<IEnumerable<DropDownDto>>(IUserResponse.Content.ReadAsStringAsync().Result);
                        ViewBag.Vessel = VesselData;

                    }
                    else
                    {
                        ModelState.Clear();
                    }
                }
            }
            ViewBag.IsEdit = true;

            return View();



        }
        public async Task<IActionResult> Index()
        {

            using (HttpClient client = new HttpClient())
            {

                var endvessel = apiBaseVesselUrl + "/GetVesselDropDown";

                using (var IUserResponse = await client.GetAsync(endvessel))
                {
                    if (IUserResponse.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var VesselData = JsonConvert.DeserializeObject<IEnumerable<DropDownDto>>(IUserResponse.Content.ReadAsStringAsync().Result);
                        ViewBag.Vessel = VesselData;

                    }
                    else
                    {
                        ModelState.Clear();
                    }
                }
            }
            ViewBag.IsEdit = true;

            return View();



        }
        public async Task<JsonResult> GetVesselList()

        {
            using (HttpClient client = new HttpClient())
            {

                var endvessel = apiBaseVesselUrl + "/GetVesselDropDown";

                using (var IUserResponse = await client.GetAsync(endvessel))
                {
                    if (IUserResponse.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var VesselData = JsonConvert.DeserializeObject<IEnumerable<DropDownDto>>(IUserResponse.Content.ReadAsStringAsync().Result);
                        return Json(VesselData);

                    }
                    else
                    {
                        return Json(0);
                    }
                }
            }

        }

        [ValidateAntiForgeryToken]
        public async Task<ActionResult> GetVesselDetails(int? Id)
        {
            var endvessel = apiBaseVesselUrl + "/GetVesselData/" + Id;

            using (HttpClient client = new HttpClient())
            {

                using (var Response = await client.GetAsync(endvessel))
                {

                    var data = JsonConvert.DeserializeObject<VesselDto>(Response.Content.ReadAsStringAsync().Result);


                    //  return RedirectToAction("Index", "VesselDetails", new { @id = data });

                    return View("~/Views/VesselDetails/Index.cshtml", data);
                }
            }

        }

        public async Task<IActionResult> OperatorWiseVessels()
        {
            using (HttpClient client = new HttpClient())
            {
                var userid = Convert.ToInt32(HttpContext.Session.GetString("UserId"));
                var endvessel = apiBaseOperatorVesselUrl + "/GetVessel/" + userid;

                using (var IUserResponse = await client.GetAsync(endvessel))
                {
                    if (IUserResponse.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var VesselData = JsonConvert.DeserializeObject<IEnumerable<User_VesselDto>>(IUserResponse.Content.ReadAsStringAsync().Result);
                        List<VesselDto> vesselDtos = new List<VesselDto>();
                        foreach (User_VesselDto uvd in VesselData)
                        {
                            vesselDtos.Add(new VesselDto() { Id = uvd.Id, Name = uvd.Vessel.Name });
                        }
                        ViewBag.Vessel = vesselDtos;

                    }
                    else
                    {
                        ModelState.Clear();
                    }
                }
            }

            return View();
        }

        [ValidateAntiForgeryToken]
        public ActionResult GetVesselWiseInspections(int? Id)
        {
            TempData["vessselId"] = Id;
            return RedirectToAction("Index", "OngoingInspection");
        }
    }
}
