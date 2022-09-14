using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Sire.Data.Dto.Master;
using Sire.Data.Dto.Training;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Sire.Web.Controllers
{
    [Authorize]
    public class TrainingVesselDetailsController : Controller
    {
        private readonly ILogger<TrainingVesselDetailsController> _logger;
        private readonly Microsoft.Extensions.Configuration.IConfiguration _iConfig;
        string apiBaseUrl = string.Empty;
        string apiBaseOperatorUrl = string.Empty;
        string apiBaseVesselUrl = string.Empty;
        string apiBaseQuestionUrl = string.Empty;
        public TrainingVesselDetailsController(ILogger<TrainingVesselDetailsController> logger,
            Microsoft.Extensions.Configuration.IConfiguration iConfig
            )
        {
            _logger = logger;
            _iConfig = iConfig;
            apiBaseUrl = _iConfig.GetValue<string>("apiUrl:url").ToString() + "/training";
            apiBaseOperatorUrl = _iConfig.GetValue<string>("apiUrl:url").ToString() + "/operator";
            apiBaseVesselUrl = _iConfig.GetValue<string>("apiUrl:url").ToString() + "/vessel";
            apiBaseQuestionUrl = _iConfig.GetValue<string>("apiUrl:url").ToString() + "/Question";
        }

        public async Task<IActionResult> Index(int? Id)
        {
            var enduser = apiBaseOperatorUrl + "/GetOperatorDropDown";

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
                                    var OperatorData = JsonConvert.DeserializeObject<IEnumerable<DropDownDto>>(IUserResponse.Content.ReadAsStringAsync().Result);
                                    ViewBag.Operator_Id = OperatorData;
                                }
                                else
                                {
                                    ModelState.Clear();
                                }
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

                            var data = JsonConvert.DeserializeObject<TrainingDto>(Response.Content.ReadAsStringAsync().Result);

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
                        }
                        else
                        {
                            ModelState.Clear();
                            ModelState.AddModelError(string.Empty, "Invalid Data");
                            return View();
                        }
                    }
                    return View();
                }

            }
        }


        public async Task<JsonResult> GetVessel(int Id)
        {
            var endvessel = apiBaseVesselUrl + "/GetVesselbyOperator/" + Id;

            using (HttpClient client = new HttpClient())
            {
                using (var Response = await client.GetAsync(endvessel))
                {

                    var data = JsonConvert.DeserializeObject<List<VesselDto>>(Response.Content.ReadAsStringAsync().Result).ToList();
                    return Json(data);

                }
            }

        }


        public async Task<JsonResult> GetVesselDetails(int Id)
        {
            var endvessel = apiBaseVesselUrl + "/getVesselDetails/" + Id;

            using (HttpClient client = new HttpClient())
            {
                using (var Response = await client.GetAsync(endvessel))
                {

                    var data = JsonConvert.DeserializeObject<VesselDto>(Response.Content.ReadAsStringAsync().Result);
                    return Json(data);

                }
            }

        }

    }
}
