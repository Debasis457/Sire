using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Sire.Data.Dto.ShipManagement;
using Sire.Data.Entities.ShipManagement;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Sire.Web.Controllers
{
    [Authorize]
    public class Vessel_Response_Piq_HvpqController : Controller
    {
        private readonly ILogger<Vessel_Response_Piq_Hvpq> _logger;
        private readonly Microsoft.Extensions.Configuration.IConfiguration _iConfig;
        string apiBaseUrl = string.Empty;
        string apiBaseResponseUrl = string.Empty;
        public Vessel_Response_Piq_HvpqController(ILogger<Vessel_Response_Piq_Hvpq> logger,
            Microsoft.Extensions.Configuration.IConfiguration iConfig
            )
        {
            _logger = logger;
            _iConfig = iConfig;
            
            apiBaseResponseUrl = _iConfig.GetValue<string>("apiUrl:url").ToString() + "/Vessel_Response_Piq_Hvpq";

        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> PIQ(Vessel_Response_Piq_HvpqDto Vessel_Response_Piq_HvpqDto)
        {

            //   if (ModelState.IsValid)
            //{
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(Vessel_Response_Piq_HvpqDto), Encoding.UTF8, "application/json");
                    using (var Response = await client.PostAsync(apiBaseResponseUrl, content))
                    {
                        if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            // Get Response Here
                            ViewBag.IsEdit = false;
                            Vessel_Response_Piq_HvpqDto = new Vessel_Response_Piq_HvpqDto();
                            using (var FleetData = await client.GetAsync(apiBaseResponseUrl))
                            {
                                var data = JsonConvert.DeserializeObject<IEnumerable<Vessel_Response_Piq_HvpqDto>>(FleetData.Content.ReadAsStringAsync().Result);
                               return View("Index" ,data);
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
    }
}
