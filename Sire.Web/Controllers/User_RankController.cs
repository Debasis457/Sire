using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Sire.Common;
using Sire.Data.Dto.Master;
using Sire.Data.Dto.Operator;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static Sire.Common.CommonServices;

namespace Sire.Web.Controllers
{
    [Authorize]
    public class User_RankController : Controller
    {
        private readonly ILogger<User_RankController> _logger;
        private readonly Microsoft.Extensions.Configuration.IConfiguration _iConfig;
        string apiBaseUrl = string.Empty;


        public User_RankController(ILogger<User_RankController> logger,
            Microsoft.Extensions.Configuration.IConfiguration iConfig
            )
        {
            _logger = logger;
            _iConfig = iConfig;
            apiBaseUrl = _iConfig.GetValue<string>("apiUrl:url").ToString() + "/User_Rank";


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
                            var data = JsonConvert.DeserializeObject<List<User_RankDto>>(Response.Content.ReadAsStringAsync().Result);
                            ViewBag.Alert=alert;
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
            if (Id == null)
            {
                ViewBag.IsEdit = false;

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
                            var data = JsonConvert.DeserializeObject<User_RankDto>(Response.Content.ReadAsStringAsync().Result);
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

        public async Task<IEnumerable<User_RankDto>> GetData()
        {
            using (HttpClient client = new HttpClient())
            {
                using (var Response = await client.GetAsync(apiBaseUrl))
                {
                    var data = JsonConvert.DeserializeObject<IEnumerable<User_RankDto>>(Response.Content.ReadAsStringAsync().Result);
                    return data;

                }
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddEdit(User_RankDto user_RankDto)
        {
           
            if (ModelState.IsValid)
            {
                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        StringContent content = new StringContent(JsonConvert.SerializeObject(user_RankDto), Encoding.UTF8, "application/json");
                        using (var Response = await client.PostAsync(apiBaseUrl, content))
                        {
                            if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                            {
                                // Get Response Here
                                ViewBag.IsEdit = false;
                                //user_RankDto = new User_RankDto();
                                using (var UserRankData = await client.GetAsync(apiBaseUrl))
                                {
                                    var data = JsonConvert.DeserializeObject<IEnumerable<User_RankDto>>(UserRankData.Content.ReadAsStringAsync().Result);
                                    
                                    if(user_RankDto.Id == 0) { 
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
                                ViewBag.IsEdit = true;
                                ModelState.Clear();
                                ViewBag.Alert = CommonServices.ShowAlert(Alerts.Warning, "User Rank Already Exists");
                                // ModelState.AddModelError(string.Empty, "Invalid Data");
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
            ViewBag.IsEdit = false;
            ViewBag.Alert = "";

            return View();
        }


        public async Task<IActionResult> Delete(int Id)
        {
            string endpoint = apiBaseUrl + "/" + Id;
            using (HttpClient client = new HttpClient())
            {
                using (var UserRankData = await client.DeleteAsync(endpoint))
                {
                    var data = JsonConvert.DeserializeObject<IEnumerable<User_RankDto>>(UserRankData.Content.ReadAsStringAsync().Result);

                    using (var Response = await client.GetAsync(apiBaseUrl))
                    {
                        if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                        {

                            var getall = JsonConvert.DeserializeObject<List<User_RankDto>>(Response.Content.ReadAsStringAsync().Result);
                            ViewBag.Alert = CommonServices.ShowAlert(Alerts.Danger, "Record Deleted Successfully");
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
