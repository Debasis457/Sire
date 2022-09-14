﻿using AutoMapper.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Sire.Data.Dto.Master;
using Sire.Data.Dto.UserMgt;
using Sire.Data.Entities.Master;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Sire.Web.Controllers
{
    public class LoginController : Controller
    {

        private readonly ILogger<LoginController> _logger;
        private readonly Microsoft.Extensions.Configuration.IConfiguration _iConfig;

        public LoginController(ILogger<LoginController> logger,
            Microsoft.Extensions.Configuration.IConfiguration iConfig
            )
        {
            _logger = logger;
            _iConfig = iConfig;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginDto user)
        {
            // Change URL If you are changeing port
            string apiBaseUrl = _iConfig.GetValue<string>("apiUrl:url").ToString();
            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
                string endpoint = apiBaseUrl + "/login";
                using (var Response = await client.PostAsync(endpoint, content))
                {
                    if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                       

                        // Get Response Here
                        var data = JsonConvert.DeserializeObject<LoginResponseDto>(Response.Content.ReadAsStringAsync().Result);
                       

                        TempData["Dashboard"] = JsonConvert.SerializeObject(user);
                        HttpContext.Session.SetString("UserName", Convert.ToString(data.Full_Name));
                        HttpContext.Session.SetString("Email", Convert.ToString(data.EmailId));
                        HttpContext.Session.SetString("Token", Convert.ToString(data.Token));
                        HttpContext.Session.SetString("UserId", Convert.ToString(data.UserId));
                        HttpContext.Session.SetString("VesselId", Convert.ToString(data.VesselId));
                        return RedirectToAction("Index", "Dashboard");
                    }
                    else
                    {
                        ModelState.Clear();
                        /*ModelState.AddModelError(string.Empty, "Invalid Data");*/
                        ViewBag.errormessage = "Username or Password is Incorrect";


                        return View();
                    }
                }
            }
        }
        [HttpGet]
        public ActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Login");
        }

    }
}
