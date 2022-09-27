using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Sire.Data.Dto.Master;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Sire.Data.Dto.Inspection;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace Sire.Web.Controllers
{
    [Authorize]
    public class VesselDetails : Controller
    {

        private readonly ILogger<VesselController> _logger;
        private readonly Microsoft.Extensions.Configuration.IConfiguration _iConfig;
        string apiBaseUrl = string.Empty;
        public VesselDetails(ILogger<VesselController> logger,
            Microsoft.Extensions.Configuration.IConfiguration iConfig
            )
        {
            _logger = logger;
            _iConfig = iConfig;
            apiBaseUrl = _iConfig.GetValue<string>("apiUrl:url").ToString() + "/Vessel";
        }

        public async Task<IActionResult> Index()
        {
            using (HttpClient client = new HttpClient())
            {
                string endpoint = apiBaseUrl + "/" + this.HttpContext.Session.GetString("VesselId"); ;
                using (var Response = await client.GetAsync(endpoint))
                {
                    var data = JsonConvert.DeserializeObject<VesselDto>(Response.Content.ReadAsStringAsync().Result);
                    return View(data);

                }
            }

        }

        public async Task<IActionResult> InspectionIndex()
        {

            return View();


        }






    }
}
