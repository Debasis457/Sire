using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Sire.Common;
using Sire.Data.Dto.Master;
using Sire.Data.Dto.ShipManagement;
using Sire.Data.Entities.ShipManagement;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static Sire.Common.CommonServices;

namespace Sire.Web.Controllers
{
    [Authorize]
    public class PIQHVPQController : Controller
    {

        private readonly ILogger<PIQHVPQController> _logger;
        private readonly Microsoft.Extensions.Configuration.IConfiguration _iConfig;
        string apiBaseUrl = string.Empty;
        string apiBaseVesselUrl = string.Empty;
        string apiBaseResponseUrl = string.Empty;

        public PIQHVPQController(ILogger<PIQHVPQController> logger,
            Microsoft.Extensions.Configuration.IConfiguration iConfig
            )
        {
            _logger = logger;
            _iConfig = iConfig;
            apiBaseUrl = _iConfig.GetValue<string>("apiUrl:url").ToString() + "/Piq_Hvpq";
            apiBaseVesselUrl = _iConfig.GetValue<string>("apiUrl:url").ToString() + "/Vessel";
            apiBaseResponseUrl = _iConfig.GetValue<string>("apiUrl:url").ToString() + "/Vessel_Response_Piq_Hvpq";
        }



        public async Task<IActionResult> Index()
        {

            var enduser = apiBaseVesselUrl + "/GetVesselDropDown";
            using (HttpClient client = new HttpClient())
            {
                // string endpoint = apiBaseUrl + "/" + Id;
                using (var Response = await client.GetAsync(enduser))
                {
                    if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        ViewBag.IsEdit = true;


                        using (var IUserResponse = await client.GetAsync(enduser))
                        {
                            if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                            {
                                var UserData = JsonConvert.DeserializeObject<IEnumerable<DropDownDto>>(IUserResponse.Content.ReadAsStringAsync().Result);
                                ViewBag.VesselList = UserData;
                            }
                            else
                            {
                                ModelState.Clear();
                            }
                        }
                        //   return View;
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

        public async Task<PartialViewResult> GetPIQLoad()
        {


            using (HttpClient client = new HttpClient())
            {


                using (var Response = await client.GetAsync(apiBaseUrl))
                {
                    if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var result = Response.Content.ReadAsStringAsync().Result;
                        var data = JsonConvert.DeserializeObject<PIQ_HVPQWrapper>(Response.Content.ReadAsStringAsync().Result);
                        var PIQdata = data.PIQ;
                        ViewBag.PIQList = data.PIQ;
                        return PartialView("PIQ", PIQdata);
                    }
                    else
                    {
                        ModelState.Clear();
                        ModelState.AddModelError(string.Empty, "Invalid Data");
                        return PartialView("PIQ");
                    }



                    //  return PartialView("data");

                }
            }

        }

        public async Task<PartialViewResult> GetHVPQLoad()
        {


            using (HttpClient client = new HttpClient())
            {


                using (var Response = await client.GetAsync(apiBaseUrl))
                {
                    if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {

                        var data = JsonConvert.DeserializeObject<PIQ_HVPQWrapper>(Response.Content.ReadAsStringAsync().Result);

                        ViewBag.HVPQList = data.HVPQ;
                        return PartialView("HVPQ", data);
                    }
                    else
                    {
                        ModelState.Clear();
                        ModelState.AddModelError(string.Empty, "Invalid Data");
                        return PartialView("HVPQ");
                    }



                    //  return PartialView("data");

                }
            }

        }

        [HttpPost]

        public async Task<IActionResult> PIQ(List<Piq_HvpqDto> Piq_HvpqDto)
        {


            if (ModelState.IsValid)
            {
                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        StringContent content = new StringContent(JsonConvert.SerializeObject(Piq_HvpqDto), Encoding.UTF8, "application/json");
                        using (var Response = await client.PostAsync(apiBaseResponseUrl, content))
                        {
                            if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                            {
                                // Get Response Here
                                ViewBag.IsEdit = false;
                                Piq_HvpqDto = new List<Piq_HvpqDto>();
                                using (var FleetData = await client.GetAsync(apiBaseResponseUrl))
                                {
                                    var data = JsonConvert.DeserializeObject<IEnumerable<List<Piq_HvpqDto>>>(FleetData.Content.ReadAsStringAsync().Result);
                                    return View("Index", data);
                                }

                            }
                            else
                            {
                                ModelState.Clear();
                                ModelState.AddModelError(string.Empty, "Invalid data");
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
            return View();
        }

        //[HttpPost]
        //public ActionResult SavePIQ(string json)
        //{
        //    var myModel = JsonConvert.DeserializeObject(json, typeof(Vessel_Response_Piq_HvpqDto));
        //    return Json(null);
        //}

        [HttpPost]
        public async Task<IActionResult> SavePIQ([FromBody] Vessel_Response_Piq_HvpqDto1[] data)
        {
            //return Json(null);
            var vesselid = Convert.ToInt32(HttpContext.Session.GetString("VesselId"));
           for(int i = 0; i < data.Length; i++)
            {
            data[i].vessel_Id = vesselid;
            }
            
            if (ModelState.IsValid)
            {
                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        StringContent content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
                        using (var Response = await client.PostAsync(apiBaseResponseUrl, content))
                        {
                            if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                            {
                                // Get Response Here
                                ViewBag.IsEdit = false;
                               // data = new List<data>();
                                using (var FleetData = await client.GetAsync(apiBaseResponseUrl))
                                {
                                    var data1 = JsonConvert.DeserializeObject<IEnumerable<List<Vessel_Response_Piq_HvpqDto>>>(FleetData.Content.ReadAsStringAsync().Result);
                                   
                                        ViewBag.Alert = CommonServices.ShowAlert(Alerts.Success, "Record added Successfully");
                                  

                                    return View("Index", data1);
                                }

                            }
                            else
                            {
                                ModelState.Clear();
                                ModelState.AddModelError(string.Empty, "Invalid data");
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
            return View();
        }



        [HttpPost]
        public async Task<IActionResult> SaveHVPQ([FromBody] Vessel_Response_Piq_HvpqDto1[] data)
        {
            //return Json(null);
            var vesselid = Convert.ToInt32(HttpContext.Session.GetString("VesselId"));
            for (int i = 0; i < data.Length; i++)
            {
                data[i].vessel_Id = vesselid;
            }
            if (ModelState.IsValid)
            {
                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        StringContent content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
                        using (var Response = await client.PostAsync(apiBaseResponseUrl, content))
                        {
                            if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                            {
                                // Get Response Here
                                ViewBag.IsEdit = false;
                                // data = new List<data>();
                                using (var FleetData = await client.GetAsync(apiBaseResponseUrl))
                                {
                                    var data1 = JsonConvert.DeserializeObject<IEnumerable<List<Vessel_Response_Piq_HvpqDto>>>(FleetData.Content.ReadAsStringAsync().Result);
                                    return View("Index", data1);
                                }

                            }
                            else
                            {
                                ModelState.Clear();
                                ModelState.AddModelError(string.Empty, "Invalid data");
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
            return View();
        }
    }
}
