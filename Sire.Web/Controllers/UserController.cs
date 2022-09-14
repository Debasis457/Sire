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
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly Microsoft.Extensions.Configuration.IConfiguration _iConfig;
        string apiBaseUrl = string.Empty;
        string apiBaseOperatorUrl = string.Empty;
        string apiBaseUserRankUrl = string.Empty;
        string apiBaseRankGroupUrl = string.Empty;
        public UserController(ILogger<UserController> logger,
            Microsoft.Extensions.Configuration.IConfiguration iConfig
            )
        {
            _logger = logger;
            _iConfig = iConfig;
            apiBaseUrl = _iConfig.GetValue<string>("apiUrl:url").ToString() + "/UserMaster";
            apiBaseOperatorUrl = _iConfig.GetValue<string>("apiUrl:url").ToString() + "/operator";
            apiBaseUserRankUrl = _iConfig.GetValue<string>("apiUrl:url").ToString() + "/User_Rank";
            apiBaseRankGroupUrl = _iConfig.GetValue<string>("apiUrl:url").ToString() + "/RankGroup";

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
                            var data = JsonConvert.DeserializeObject<List<UserDto>>(Response.Content.ReadAsStringAsync().Result);
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
            var enduser = apiBaseOperatorUrl + "/GetOperatorDropDown";
            var endUserRank = apiBaseUserRankUrl + "/GetUser_RankDropDown";
            var endRankGroup = apiBaseRankGroupUrl + "/GetRankGroupDropDown";
            if (Id == null)
            { ViewBag.IsEdit = false;
               
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

                            using (var IUserResponse = await client.GetAsync(endUserRank))
                            {
                                if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                                {
                                    var RankData = JsonConvert.DeserializeObject<IEnumerable<DropDownDto>>(IUserResponse.Content.ReadAsStringAsync().Result);
                                    ViewBag.Rank_Id = RankData;
                                }
                                else
                                {
                                    ModelState.Clear();
                                }
                            }

                            using (var IUserResponse = await client.GetAsync(endRankGroup))
                            {
                                if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                                {
                                    var RankGroupData = JsonConvert.DeserializeObject<IEnumerable<DropDownDto>>(IUserResponse.Content.ReadAsStringAsync().Result);
                                    ViewBag.Rank_Group = RankGroupData;
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

                            var data = JsonConvert.DeserializeObject<UserDto>(Response.Content.ReadAsStringAsync().Result);

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

                            using (var IUserResponse = await client.GetAsync(endUserRank))
                            {
                                if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                                {
                                    var RankData = JsonConvert.DeserializeObject<IEnumerable<DropDownDto>>(IUserResponse.Content.ReadAsStringAsync().Result);
                                    ViewBag.Rank_Id = RankData;
                                }
                                else
                                {
                                    ModelState.Clear();
                                }
                            }

                            using (var IUserResponse = await client.GetAsync(endRankGroup))
                            {
                                if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                                {
                                    var RankGroupData = JsonConvert.DeserializeObject<IEnumerable<DropDownDto>>(IUserResponse.Content.ReadAsStringAsync().Result);
                                    ViewBag.Rank_Group = RankGroupData;
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

        public async Task<IEnumerable<UserDto>> GetData()
        {
            using (HttpClient client = new HttpClient())
            {


                using (var Response = await client.GetAsync(apiBaseUrl))
                {
                    var data = JsonConvert.DeserializeObject<IEnumerable<UserDto>>(Response.Content.ReadAsStringAsync().Result);
                    return data;

                }
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddEdit(UserDto UserDto)
        {

            /*if (ModelState.IsValid)
            {*/
                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        
                        StringContent content = new StringContent(JsonConvert.SerializeObject(UserDto), Encoding.UTF8, "application/json");
                       
                        using (var Response = await client.PostAsync(apiBaseUrl, content))
                        {
                            if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                            {
                                // Get Response Here
                                ViewBag.IsEdit = false;
                                //UserDto = new UserDto();
                                using (var UserData = await client.GetAsync(apiBaseUrl))
                                {
                                    var data = JsonConvert.DeserializeObject<List<UserDto>>(UserData.Content.ReadAsStringAsync().Result);

                                if (UserDto.Id == 0)
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
            //}
            
            return View();
        }


        public async Task<IActionResult> Delete(int Id)
        {
            string endpoint = apiBaseUrl + "/" + Id;
            using (HttpClient client = new HttpClient())
            {
                using (var UserData = await client.DeleteAsync(endpoint))
                {
                    var data = JsonConvert.DeserializeObject<IEnumerable<UserDto>>(UserData.Content.ReadAsStringAsync().Result);
                    using (var Response = await client.GetAsync(apiBaseUrl))
                    {
                        if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                        {

                            var getall = JsonConvert.DeserializeObject<List<UserDto>>(Response.Content.ReadAsStringAsync().Result);
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
