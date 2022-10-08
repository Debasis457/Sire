using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Sire.Data.Dto.Master;
using Sire.Data.Dto.Operator;
using Sire.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Sire.Web.Controllers
{
    [Authorize]
    public class OperatorDashboard : Controller
    {
        private readonly ILogger<OperatorDashboard> _logger;
        private readonly Microsoft.Extensions.Configuration.IConfiguration _iConfig;
        string apiBaseUrl = string.Empty;
        string apiBaseOperatorUrl = string.Empty;
        string apiBaseVesselUrl = string.Empty;
        string apiBaseQuestionUrl = string.Empty;
        string apiBaseVessel_Url = string.Empty;
        public OperatorDashboard(ILogger<OperatorDashboard> logger,
     IConfiguration iConfig
            )
        {
            _logger = logger;
            _iConfig = iConfig;

            apiBaseVesselUrl = _iConfig.GetValue<string>("apiUrl:url").ToString() + "/User_Vessel";
            apiBaseVessel_Url = _iConfig.GetValue<string>("apiUrl:url").ToString() + "/Vessel";
        }

    public async Task<IActionResult> Index(int? Id)
    {
        var operatorDashboardModel = new OperatorDashboardModel();
        var usertype = Convert.ToInt32(HttpContext.Session.GetString("RoleId"));
        ViewBag.UserType = usertype;
        ViewBag.VesselId = 0;
        if (Id == null)
        {
            using HttpClient client = new();
            var userid = Convert.ToInt32(HttpContext.Session.GetString("UserId"));

            string endpoint = apiBaseVesselUrl + "/GetVessel/" + userid; /*+ this.HttpContext.Session.GetString("UserId"); ;*/
            using var response = await client.GetAsync(endpoint);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                ViewBag.IsEdit = true;
                var user_VesselDtos = JsonConvert.DeserializeObject<List<User_VesselDto>>(response.Content.ReadAsStringAsync().Result);
                if (usertype == 1)
                {
                    ViewBag.VesselId = user_VesselDtos.Count > 0 ? user_VesselDtos.FirstOrDefault().Vessel_Id : 0;
                }

                //var fleetGrouping = user_VesselDtos.GroupBy(x => new FleetKeyModel { Id = x.Vessel.Fleet_id, Name = x.Vessel.Fleet.Name });
                //var fleetGrouping = user_VesselDtos.GroupBy(x => x.Vessel.Fleet);
                //operatorDashboardModel.FleetVessels = fleetGrouping;

                var fleetVessels = new Dictionary<string, IList<User_VesselDto>>();

                foreach (var user_VesselDto in user_VesselDtos)
                {
                    var fleetName = user_VesselDto.Vessel.Fleet.Name;
                    //var fleetKey = new FleetKeyModel { Id = user_VesselDto.Vessel.Fleet.Id, Name = user_VesselDto.Vessel.Fleet.Name };
                    //var existingFleetKey = fleetVessels.Where(x => x.Value)
                    if (fleetVessels.ContainsKey(user_VesselDto.Vessel.Fleet.Name))
                    {
                        fleetVessels[fleetName].Add(user_VesselDto);
                    }
                    else
                    {
                        fleetVessels.Add(fleetName, new List<User_VesselDto> { user_VesselDto });
                    }
                }

                operatorDashboardModel.FleetVessels = fleetVessels;

                return View(operatorDashboardModel);
            }
            else
            {
                ModelState.Clear();
                ModelState.AddModelError(string.Empty, "Invalid Data");
                return View();
            }
        }
        else
        {

            using (HttpClient client = new HttpClient())
            {

                string endpoint = apiBaseVesselUrl + "/GetVessel/" + Id;   /* "/"  + this.HttpContext.Session.GetString("UserId");;*/
               
                    using (var Response = await client.GetAsync(endpoint))
                {
                    if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                            ViewBag.IsEdit = true;
                            var user_VesselDtos = JsonConvert.DeserializeObject<List<User_VesselDto>>(Response.Content.ReadAsStringAsync().Result);
                            if (usertype == 1)
                            {
                                ViewBag.VesselId = user_VesselDtos.Count > 0 ? user_VesselDtos.FirstOrDefault().Vessel_Id : 0;
                            }

                            //var fleetGrouping = user_VesselDtos.GroupBy(x => new FleetKeyModel { Id = x.Vessel.Fleet_id, Name = x.Vessel.Fleet.Name });
                            //var fleetGrouping = user_VesselDtos.GroupBy(x => x.Vessel.Fleet);
                            //operatorDashboardModel.FleetVessels = fleetGrouping;

                            var fleetVessels = new Dictionary<string, IList<User_VesselDto>>();

                            foreach (var user_VesselDto in user_VesselDtos)
                            {
                                var fleetName = user_VesselDto.Vessel.Fleet.Name;
                                //var fleetKey = new FleetKeyModel { Id = user_VesselDto.Vessel.Fleet.Id, Name = user_VesselDto.Vessel.Fleet.Name };
                                //var existingFleetKey = fleetVessels.Where(x => x.Value)
                                if (fleetVessels.ContainsKey(user_VesselDto.Vessel.Fleet.Name))
                                {
                                    fleetVessels[fleetName].Add(user_VesselDto);
                                }
                                else
                                {
                                    fleetVessels.Add(fleetName, new List<User_VesselDto> { user_VesselDto });
                                }
                            }

                            operatorDashboardModel.FleetVessels = fleetVessels;

                            return View(operatorDashboardModel);
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


    public async Task<ActionResult> GetVesselDetails(int? Id)
    {
        var endvessel = apiBaseVessel_Url + "/GetVesselData/" + Id;

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

    public IActionResult GoToAction(int? type, int? vesselId)
    {
        if (type == 1)
        {
            TempData["vessselId"] = vesselId;
            return RedirectToAction("Index", "OngoingInspection");
        }
        return RedirectToAction("Index", "TrainingQuestion");
    }
}
}
