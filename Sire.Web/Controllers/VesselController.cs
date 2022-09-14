using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Sire.Common;
using Sire.Data.Dto.Master;
using Sire.Data.Dto.UserMgt;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static Sire.Common.CommonServices;

namespace Sire.Web.Controllers
{
    [Authorize]
    public class VesselController : Controller
    {
        private readonly ILogger<VesselController> _logger;
        private readonly Microsoft.Extensions.Configuration.IConfiguration _iConfig;
        string apiBaseUrl = string.Empty;
        string apiBaseOperatorUrl = string.Empty;
        string apiBaseFleetUrl = string.Empty;
        string apiBaseVesselUrl = string.Empty;
        public VesselController(ILogger<VesselController> logger,
            Microsoft.Extensions.Configuration.IConfiguration iConfig
            )
        {
            _logger = logger;
            _iConfig = iConfig;
            apiBaseUrl = _iConfig.GetValue<string>("apiUrl:url").ToString() + "/Vessel";

            apiBaseOperatorUrl = _iConfig.GetValue<string>("apiUrl:url").ToString() + "/operator";
            apiBaseFleetUrl = _iConfig.GetValue<string>("apiUrl:url").ToString() + "/Fleet";

            apiBaseVesselUrl = _iConfig.GetValue<string>("apiUrl:url").ToString() + "/Vessel";
        }

        public async Task<IActionResult> Index(string? alert)
        {

            try

            {
                using (HttpClient client = new HttpClient())
                {


                    using (var Response = await client.GetAsync(apiBaseUrl))
                    {
                        if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            var data = JsonConvert.DeserializeObject<IEnumerable<VesselDto>>(Response.Content.ReadAsStringAsync().Result);
                            ViewBag.Alert = alert;
                            return View(data);
                        }
                        else
                        {
                            ModelState.Clear();
                            ModelState.AddModelError(string.Empty, "Invalid Vessel data");
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

        public async Task<IActionResult> AddEdit(int? Id)
        {


            string endpoint = apiBaseUrl + "/" + Id;
            if (Id == null)
            {
                ViewBag.IsEdit = false;
                using (HttpClient client = new HttpClient())
                {


                    using (var Response = await client.GetAsync(endpoint))
                    {
                        if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            ViewBag.IsEdit = true;
                           
                            var enduser = apiBaseOperatorUrl + "/GetOperatorDropDown";
                            var endfleet = apiBaseFleetUrl + "/GetFleetDropDown";
                            var endvesseltype = apiBaseVesselUrl + "/GetVesselTypeDropDown";
                            using (var IUserResponse = await client.GetAsync(enduser))
                            {
                                if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                                {
                                    var UserData = JsonConvert.DeserializeObject<IEnumerable<DropDownDto>>(IUserResponse.Content.ReadAsStringAsync().Result);
                                    ViewBag.Operator = UserData;

                                }
                                else
                                {
                                    ModelState.Clear();
                                }
                            }
                            using (var IUserResponse = await client.GetAsync(endfleet))
                            {
                                if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                                {
                                    var FleetData = JsonConvert.DeserializeObject<IEnumerable<DropDownDto>>(IUserResponse.Content.ReadAsStringAsync().Result);
                                    ViewBag.Fleet = FleetData;

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

                   
                    using (var Response = await client.GetAsync(endpoint))
                    {
                        if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            ViewBag.IsEdit = true;
                            var data = JsonConvert.DeserializeObject<VesselDto>(Response.Content.ReadAsStringAsync().Result);
                            var enduser = apiBaseOperatorUrl + "/GetOperatorDropDown";
                            var endvesseltype = apiBaseFleetUrl + "/GetVesselTypeDropDown";
                            var endfleet = apiBaseFleetUrl + "/GetFleetDropDown";
                            using (var IUserResponse = await client.GetAsync(enduser))
                            {
                                if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                                {
                                    var UserData = JsonConvert.DeserializeObject<IEnumerable<DropDownDto>>(IUserResponse.Content.ReadAsStringAsync().Result);
                                    ViewBag.Operator = UserData;

                                }
                                else
                                {
                                    ModelState.Clear();
                                }
                            }
                            using (var FleetResponse = await client.GetAsync(endfleet))
                            {
                                if (FleetResponse.StatusCode == System.Net.HttpStatusCode.OK)
                                {
                                    var UserData = JsonConvert.DeserializeObject<IEnumerable<DropDownDto>>(FleetResponse.Content.ReadAsStringAsync().Result);
                                    ViewBag.Fleet = UserData;

                                }
                                else
                                {
                                    ModelState.Clear();
                                }
                            }
                           

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
        }

        public async Task<IEnumerable<VesselDto>> GetData()
        {
            using (HttpClient client = new HttpClient())
            {


                using (var Response = await client.GetAsync(apiBaseUrl))
                {
                    var data = JsonConvert.DeserializeObject<IEnumerable<VesselDto>>(Response.Content.ReadAsStringAsync().Result);
                    return data;

                }
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddEdit(VesselDto vesselDto)
        {
/*
            if (ModelState.IsValid)
            {*/
                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        StringContent content = new StringContent(JsonConvert.SerializeObject(vesselDto), Encoding.UTF8, "application/json");
                        
                    using (var Response = await client.PostAsync(apiBaseUrl, content))
                        {
                            if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                            {
                                // Get Response Here
                                ViewBag.IsEdit = false;
                               // vesselDto = new VesselDto();
                                using (var VesselData = await client.GetAsync(apiBaseUrl))
                                {
                                    var data = JsonConvert.DeserializeObject<List<VesselDto>>(VesselData.Content.ReadAsStringAsync().Result);
                                if (vesselDto.Id == 0)
                                {
                                    ViewBag.Alert = CommonServices.ShowAlert(Alerts.Success, "Record added Successfully");
                                }
                                else
                                {
                                    ViewBag.Alert = CommonServices.ShowAlert(Alerts.Success, "Record Updated Successfully");

                                }

                                return View("Index", data);
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
           // }
            return View();
        }


        public async Task<IActionResult> Delete(int Id)
        {
            string endpoint = apiBaseUrl + "/" + Id;
            using (HttpClient client = new HttpClient())
            {
                using (var VesselData = await client.DeleteAsync(endpoint))
                {
                    var data = JsonConvert.DeserializeObject<IEnumerable<VesselDto>>(VesselData.Content.ReadAsStringAsync().Result);
                    using (var Response = await client.GetAsync(apiBaseUrl))
                    {
                        if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                        {

                            var getall = JsonConvert.DeserializeObject<List<VesselDto>>(Response.Content.ReadAsStringAsync().Result);
                            ViewBag.Alert = CommonServices.ShowAlert(Alerts.Danger, "Record Deleted Successfully");

                            //return View(getall);
                            return View("Index", getall);
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
            return RedirectToAction(nameof(Index));
        }
    }
}
