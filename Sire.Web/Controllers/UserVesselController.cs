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
    public class UserVesselController : Controller
    {
        private readonly ILogger<UserVesselController> _logger;
        private readonly Microsoft.Extensions.Configuration.IConfiguration _iConfig;
        string apiBaseUrl = string.Empty;
        string apiBaseUserUrl = string.Empty;
        string apiBaseVesselUrl = string.Empty;

        public UserVesselController(ILogger<UserVesselController> logger,
            Microsoft.Extensions.Configuration.IConfiguration iConfig
            )
        {
            _logger = logger;
            _iConfig = iConfig;
            apiBaseUrl = _iConfig.GetValue<string>("apiUrl:url").ToString() + "/User_Vessel";
            apiBaseUserUrl = _iConfig.GetValue<string>("apiUrl:url").ToString() + "/UserMaster";
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
                            var data = JsonConvert.DeserializeObject<List<User_VesselDto>>(Response.Content.ReadAsStringAsync().Result);
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

                            var enduser = apiBaseUserUrl + "/GetUserDropDown";
                            var endvessel = apiBaseVesselUrl + "/GetVesselDropDown";
                            using (var IUserResponse = await client.GetAsync(enduser))
                            {
                                if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                                {
                                    var UserData = JsonConvert.DeserializeObject<IEnumerable<DropDownDto>>(IUserResponse.Content.ReadAsStringAsync().Result);
                                    ViewBag.User = UserData;

                                }
                                else
                                {
                                    ModelState.Clear();
                                }
                            }
                            using (var IVesselResponse = await client.GetAsync(endvessel))
                            {
                                if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                                {
                                    var VesselData = JsonConvert.DeserializeObject<IEnumerable<DropDownDto>>(IVesselResponse.Content.ReadAsStringAsync().Result);
                                   ViewBag.Vessel = VesselData;

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

                  //  string endpoint = apiBaseUrl + "/" + Id;
                    using (var Response = await client.GetAsync(endpoint))
                    {
                        if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            ViewBag.IsEdit = true;
                            var data = JsonConvert.DeserializeObject<User_VesselDto>(Response.Content.ReadAsStringAsync().Result);
                            var enduser = apiBaseUserUrl + "/GetUserDropDown";
                            var endvessel = apiBaseVesselUrl + "/GetVesselDropDown";
                            using (var IUserResponse = await client.GetAsync(enduser))
                            {
                                if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                                {
                                    var UserData = JsonConvert.DeserializeObject<IEnumerable<DropDownDto>>(IUserResponse.Content.ReadAsStringAsync().Result);
                                    ViewBag.User = UserData;

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
                                    var VesselData = JsonConvert.DeserializeObject<IEnumerable<DropDownDto>>(IUserResponse.Content.ReadAsStringAsync().Result);
                                   ViewBag.Vessel = VesselData;

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

        public async Task<IEnumerable<User_VesselDto>> GetData()
        {
            using (HttpClient client = new HttpClient())
            {


                using (var Response = await client.GetAsync(apiBaseUrl))
                {
                    var data = JsonConvert.DeserializeObject<IEnumerable<User_VesselDto>>(Response.Content.ReadAsStringAsync().Result);
                    return data;

                }
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddEdit(User_VesselDto User_VesselDto)
        {

           // if (ModelState.IsValid)
            {
                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        StringContent content = new StringContent(JsonConvert.SerializeObject(User_VesselDto), Encoding.UTF8, "application/json");
                        using (var Response = await client.PostAsync(apiBaseUrl, content))
                        {
                            if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                            {
                                // Get Response Here
                                ViewBag.IsEdit = false;
                              //  User_VesselDto = new User_VesselDto();
                                using (var UserData = await client.GetAsync(apiBaseUrl))
                                {
                                    var data = JsonConvert.DeserializeObject<List<User_VesselDto>>(UserData.Content.ReadAsStringAsync().Result);
                                if (User_VesselDto.Id == 0)
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
            }

            //using (HttpClient client = new HttpClient())
            //{
               

            //    using (var Response = await client.GetAsync(apiBaseUrl))
            //    {
            //        if (Response.StatusCode == System.Net.HttpStatusCode.OK)
            //        {
            //            ViewBag.IsEdit = true;

            //            var enduser = apiBaseUserUrl + "/GetUserDropDown";
            //            var endvessel = apiBaseVesselUrl + "/GetVesselDropDown";
            //            using (var IUserResponse = await client.GetAsync(enduser))
            //            {
            //                if (Response.StatusCode == System.Net.HttpStatusCode.OK)
            //                {
            //                    var UserData = JsonConvert.DeserializeObject<IEnumerable<DropDownDto>>(IUserResponse.Content.ReadAsStringAsync().Result);
            //                    ViewBag.User = UserData;

            //                }
            //                else
            //                {
            //                    ModelState.Clear();
            //                }
            //            }
            //            using (var IVesselResponse = await client.GetAsync(endvessel))
            //            {
            //                if (Response.StatusCode == System.Net.HttpStatusCode.OK)
            //                {
            //                    var VesselData = JsonConvert.DeserializeObject<IEnumerable<DropDownDto>>(IVesselResponse.Content.ReadAsStringAsync().Result);
            //                    ViewBag.Vessel = VesselData;

            //                }
            //                else
            //                {
            //                    ModelState.Clear();
            //                }
            //            }

            //            // return View(data);
            //        }
            //        else
            //        {
            //            ModelState.Clear();
            //            ModelState.AddModelError(string.Empty, "Invalid Data");
            //            return View();
            //        }
            //    }
            //}
            ViewBag.IsEdit = false;
            ViewBag.Alert = "";
            return View();
        }


        public async Task<IActionResult> Delete(int Id)
        {
            string endpoint = apiBaseUrl + "/" + Id;
            using (HttpClient client = new HttpClient())
            {
                using (var UserData = await client.DeleteAsync(endpoint))
                {
                    var data = JsonConvert.DeserializeObject<IEnumerable<User_VesselDto>>(UserData.Content.ReadAsStringAsync().Result);


                    using (var Response = await client.GetAsync(apiBaseUrl))
                    {
                        if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                        {

                            var getall = JsonConvert.DeserializeObject<List<User_VesselDto>>(Response.Content.ReadAsStringAsync().Result);
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
