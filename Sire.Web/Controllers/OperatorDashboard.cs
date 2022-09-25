using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Sire.Data.Dto.Master;
using Sire.Data.Dto.Operator;
using System;
using System.Collections.Generic;
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
        public OperatorDashboard(ILogger<OperatorDashboard> logger,
            Microsoft.Extensions.Configuration.IConfiguration iConfig
            )
        {
            _logger = logger;
            _iConfig = iConfig;
           
            apiBaseVesselUrl = _iConfig.GetValue<string>("apiUrl:url").ToString() + "/User_Vessel";
  
        }
        public async Task<IActionResult> Index(int? Id)
        {


            if (Id == null)
            {
                using (HttpClient client = new HttpClient())
                {
                    var userid = Convert.ToInt32(HttpContext.Session.GetString("UserId"));
                    string endpoint = apiBaseVesselUrl + "/GetVessel/" + userid; /*+ this.HttpContext.Session.GetString("UserId"); ;*/
                    using (var Response = await client.GetAsync(endpoint))
                    {
                        if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            ViewBag.IsEdit = true;
                            var data = JsonConvert.DeserializeObject<List<User_VesselDto>>(Response.Content.ReadAsStringAsync().Result);
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
                            var data = JsonConvert.DeserializeObject<List<User_VesselDto>>(Response.Content.ReadAsStringAsync().Result);
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

    }
}
