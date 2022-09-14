using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Sire.Data.Dto.Master;
using Sire.Data.Dto.Question;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Sire.Web.Controllers
{
    [Authorize]
    public class InspectionQuestionController : Controller
    {

        private readonly ILogger<FleetController> _logger;
        private readonly Microsoft.Extensions.Configuration.IConfiguration _iConfig;
        string apiBaseUrl = string.Empty;
        string apiBaseUserUrl = string.Empty;
        string apiBaseQuestionUrl = string.Empty;
        
        public InspectionQuestionController(ILogger<FleetController> logger,
            Microsoft.Extensions.Configuration.IConfiguration iConfig
            )
        {
            _logger = logger;
            _iConfig = iConfig;
            apiBaseUrl = _iConfig.GetValue<string>("apiUrl:url").ToString() + "/AssesorReviewer";
            apiBaseQuestionUrl = _iConfig.GetValue<string>("apiUrl:url").ToString() + "/Question";
           
        }
        public async Task<IActionResult> Index()
        {
            try

            {
                using (HttpClient client = new HttpClient())
                {


                    using (var Response = await client.GetAsync(apiBaseUrl))
                    {
                        if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                        {

                            var data = JsonConvert.DeserializeObject<List<QuetionSectionDto>>(Response.Content.ReadAsStringAsync().Result);
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


            var endquestion = apiBaseQuestionUrl + "/" + Id;
            try

            {
                using (HttpClient client = new HttpClient())
                {


                    using (var Response = await client.GetAsync(endquestion))
                    {
                        if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                        {

                            var data = JsonConvert.DeserializeObject<QuestionDto>(Response.Content.ReadAsStringAsync().Result);
                            //  return View(data);
                            ViewBag.Question=data;
                            return View("~/Views/InspectionFlow/Index.cshtml", data);
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


        public async Task<IActionResult> GetDetails(int? id)
        {
            var endquestion = apiBaseQuestionUrl + "/" + id;

            using (HttpClient client = new HttpClient())
            {
                using (var Response = await client.GetAsync(endquestion))
                {
                    if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var result = Response.Content.ReadAsStringAsync().Result;

                        var data = JsonConvert.DeserializeObject<QuestionDto>(Response.Content.ReadAsStringAsync().Result);

                        return View("~/Views/InspectionFlow/Index.cshtml", data);
                    }
                    else
                    {
                        ModelState.Clear();
                        ModelState.AddModelError(string.Empty, "Invalid Data");
                        return PartialView();
                    }
                }
            }
        }
    }
}
