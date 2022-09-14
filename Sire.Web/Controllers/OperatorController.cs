using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Sire.Common;
using Sire.Data.Dto.Master;
using Sire.Data.Dto.Operator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static Sire.Common.CommonServices;

namespace SIREWEB.Controllers
{
    [Authorize]
    public class OperatorController : Controller
    {

        private readonly ILogger<OperatorController> _logger;
        private readonly Microsoft.Extensions.Configuration.IConfiguration _iConfig;
        string apiBaseUrl = string.Empty;
        

        public OperatorController(ILogger<OperatorController> logger,
            Microsoft.Extensions.Configuration.IConfiguration iConfig
            )
        {
            _logger = logger;
            _iConfig = iConfig;
            apiBaseUrl = _iConfig.GetValue<string>("apiUrl:url").ToString() + "/operator";
           

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
                            var data = JsonConvert.DeserializeObject<List<OperatorDto>>(Response.Content.ReadAsStringAsync().Result);
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
                            var data = JsonConvert.DeserializeObject<OperatorDto>(Response.Content.ReadAsStringAsync().Result);
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

        public async Task<IEnumerable<OperatorDto>> GetData()
        {
            using (HttpClient client = new HttpClient())
            {


                using (var Response = await client.GetAsync(apiBaseUrl))
                {
                    var data = JsonConvert.DeserializeObject<IEnumerable<OperatorDto>>(Response.Content.ReadAsStringAsync().Result);
                    return data;

                }
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddEdit(OperatorDto operatorDto)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        StringContent content = new StringContent(JsonConvert.SerializeObject(operatorDto), Encoding.UTF8, "application/json");
                        using (var Response = await client.PostAsync(apiBaseUrl, content))
                        {
                            if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                            {
                                // Get Response Here
                                ViewBag.IsEdit = false;
                              //  operatorDto = new OperatorDto();
                                using (var OperatorData = await client.GetAsync(apiBaseUrl))
                                {
                                    var data = JsonConvert.DeserializeObject<IEnumerable<OperatorDto>>(OperatorData.Content.ReadAsStringAsync().Result);
                                    if (operatorDto.Id == 0)
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
                                ViewBag.IsEdit = true;
                                ModelState.Clear();
                                ViewBag.Alert = CommonServices.ShowAlert(Alerts.Warning, "Record Already Exists");
                             //   ModelState.AddModelError(string.Empty, "Invalid Data");
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


        public async Task<IActionResult> Delete(int Id)
        {
            string endpoint = apiBaseUrl + "/" + Id;
            using (HttpClient client = new HttpClient())
            {
                using (var OperatorData = await client.DeleteAsync(endpoint))
                {
                    var data = JsonConvert.DeserializeObject<IEnumerable<OperatorDto>>(OperatorData.Content.ReadAsStringAsync().Result);

                    using (var Response = await client.GetAsync(apiBaseUrl))
                    {
                        if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                        {

                            var getall = JsonConvert.DeserializeObject<List<OperatorDto>>(Response.Content.ReadAsStringAsync().Result);
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
