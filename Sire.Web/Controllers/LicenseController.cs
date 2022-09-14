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
    public class LicenseController : Controller
    {

        private readonly ILogger<LicenseController> _logger;
        private readonly Microsoft.Extensions.Configuration.IConfiguration _iConfig;
        string apiBaseUrl = string.Empty;
        string apiBaseUserUrl = string.Empty;
        string apiBaseVesselUrl = string.Empty;
        public LicenseController(ILogger<LicenseController> logger,
            Microsoft.Extensions.Configuration.IConfiguration iConfig
            )
        {
            _logger = logger;
            _iConfig = iConfig;
            apiBaseUserUrl = _iConfig.GetValue<string>("apiUrl:url").ToString() + "/UserMaster";
            apiBaseUrl = _iConfig.GetValue<string>("apiUrl:url").ToString() + "/license";
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

                            var data = JsonConvert.DeserializeObject<List<LicenseDto>>(Response.Content.ReadAsStringAsync().Result);
                            ViewBag.Alert = alert;

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
            return View();
        }

        public async Task<IActionResult> AddEdit(int? Id)
        {
            var enduser = apiBaseUserUrl + "/GetUserDropDown";
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
                                    var UserData = JsonConvert.DeserializeObject<IEnumerable<DropDownDto>>(IUserResponse.Content.ReadAsStringAsync().Result);
                                    ViewBag.Fleet_Head_Id = UserData;
                                }
                                else
                                {
                                    ModelState.Clear();
                                }
                            }

                            using (var IUserResponse = await client.GetAsync(endvessel))
                            {
                                if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                                {
                                    var UserData = JsonConvert.DeserializeObject<IEnumerable<DropDownDto>>(IUserResponse.Content.ReadAsStringAsync().Result);
                                    ViewBag.Vessel = UserData;

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

                            var data = JsonConvert.DeserializeObject<LicenseDto>(Response.Content.ReadAsStringAsync().Result);

                            using (var IUserResponse = await client.GetAsync(enduser))
                            {
                                if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                                {
                                    var UserData = JsonConvert.DeserializeObject<IEnumerable<DropDownDto>>(IUserResponse.Content.ReadAsStringAsync().Result);
                                    ViewBag.Fleet_Head_Id = UserData;
                                }
                                else
                                {
                                    ModelState.Clear();
                                }
                            }
                            using (var IUserResponse = await client.GetAsync(endvessel))
                            {
                                if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                                {
                                    var UserData = JsonConvert.DeserializeObject<IEnumerable<DropDownDto>>(IUserResponse.Content.ReadAsStringAsync().Result);
                                    ViewBag.Vessel = UserData;

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

                    //for User dropdown

                }



            }
        }

        public async Task<IEnumerable<LicenseDto>> GetData()
        {
            using (HttpClient client = new HttpClient())
            {


                using (var Response = await client.GetAsync(apiBaseUrl))
                {
                    var data = JsonConvert.DeserializeObject<IEnumerable<LicenseDto>>(Response.Content.ReadAsStringAsync().Result);


                    return data;

                }
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddEdit(LicenseDto licenseDto)
        {

            //   if (ModelState.IsValid)
            //{
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(licenseDto), Encoding.UTF8, "application/json");
                    using (var Response = await client.PostAsync(apiBaseUrl, content))
                    {
                        if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            // Get Response Here
                            ViewBag.IsEdit = false;
                            //licenseDto = new LicenseDto();
                            using (var LicenseData = await client.GetAsync(apiBaseUrl))
                            {
                                var data = JsonConvert.DeserializeObject<List<LicenseDto>>(LicenseData.Content.ReadAsStringAsync().Result);
                                if (licenseDto.Id == 0)
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
            //  }
            return View();
        }


        public async Task<IActionResult> Delete(int Id)
        {
            string endpoint = apiBaseUrl + "/" + Id;
            using (HttpClient client = new HttpClient())
            {
                using (var FleetData = await client.DeleteAsync(endpoint))
                {
                    var data = JsonConvert.DeserializeObject<List<LicenseDto>>(FleetData.Content.ReadAsStringAsync().Result);
                    using (var Response = await client.GetAsync(apiBaseUrl))
                    {
                        if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                        {

                            var getall = JsonConvert.DeserializeObject<List<LicenseDto>>(Response.Content.ReadAsStringAsync().Result);
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
                    // return View("Index", data);
                }
            }
            return RedirectToAction(nameof(Index));

        }



    }
}



