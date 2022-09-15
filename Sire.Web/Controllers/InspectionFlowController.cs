using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Amqp.Framing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Sire.Data.Dto.Inspection;
using Sire.Data.Dto.Question;
using Sire.Web.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Sire.Web.Controllers
{
    [Authorize]
    public class InspectionFlowController : Controller
    {

        private readonly ILogger<InspectionController> _logger;
        private readonly Microsoft.Extensions.Configuration.IConfiguration _iConfig;
        string apiBaseUrl = string.Empty;
        string apiBaseQuestionUrl = string.Empty;
        string apiBaseResponseUrl = string.Empty;
        string apiBaseQuestionResponseUrl = string.Empty;

        public InspectionFlowController(ILogger<InspectionController> logger,
            Microsoft.Extensions.Configuration.IConfiguration iConfig
            )
        {
            _logger = logger;
            _iConfig = iConfig;
           
            apiBaseUrl = _iConfig.GetValue<string>("apiUrl:url").ToString() + "/training";
            apiBaseQuestionUrl = _iConfig.GetValue<string>("apiUrl:url").ToString() + "/Question";
            apiBaseResponseUrl = _iConfig.GetValue<string>("apiUrl:url").ToString() + "/InspectionResponse";
            apiBaseQuestionResponseUrl = _iConfig.GetValue<string>("apiUrl:url").ToString() + "/QuestionResponse";
        }

        public async Task<IActionResult> Index()
        {
           
                return View();

        }
        

        public async Task<PartialViewResult> GetQueCheckList(int? id)
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



                        return PartialView("CheckList", data);
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
        public async Task<PartialViewResult> GetQuestionDetails(int? Id)
        {

            var endquestion = apiBaseQuestionUrl + "/" + Id;

            using (HttpClient client = new HttpClient())
            {
                using (var Response = await client.GetAsync(endquestion))
                {
                    if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var result = Response.Content.ReadAsStringAsync().Result;

                        var data = JsonConvert.DeserializeObject<QuestionDto>(Response.Content.ReadAsStringAsync().Result);



                        return PartialView("Guidance", data);
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

        public async Task<PartialViewResult> GetQuestionResponse(int? Id)
        {

            var endquestion = apiBaseQuestionUrl + "/" + Id;

            var questionresponse = apiBaseQuestionResponseUrl + "/GetResponse";

            using (HttpClient client = new HttpClient())
            {
               
                   
             
                using (var Response = await client.GetAsync(endquestion))
                {

                    if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var result = Response.Content.ReadAsStringAsync().Result;

                        var data = JsonConvert.DeserializeObject<QuestionDto>(Response.Content.ReadAsStringAsync().Result);

                        QuestionResponseModel questionResponseModel = new QuestionResponseModel();
                        questionResponseModel.questionDto = data;
                        using (var QResponse = await client.GetAsync(questionresponse))
                        {
                            if (QResponse.StatusCode == System.Net.HttpStatusCode.OK)
                            {
                                questionResponseModel.questionResponseDtos = JsonConvert.DeserializeObject<IList<QuestionResponseDto>>(QResponse.Content.ReadAsStringAsync().Result).ToList();
                            }
                        }

                        return PartialView("Response", questionResponseModel);
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

        public async Task<JsonResult> PassQuestionResponse(int Id)
        {

            var endquestion = apiBaseResponseUrl + "/" + Id ;

            using (HttpClient client = new HttpClient())
            {
                using (var Response = await client.GetAsync(endquestion))
                {
                    if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var result = Response.Content.ReadAsStringAsync().Result;

                        var data = JsonConvert.DeserializeObject<QuestionDto>(Response.Content.ReadAsStringAsync().Result);
                         
                        return Json(data);
                    }
                    else
                    {
                        return Json(0);
                    }
                }
            }

        }

        [HttpPost]
        public async Task<ActionResult> SaveResponse([FromBody] List<InspectionResponseDto> data)

        {
            //return Json(null);
            var Id = data[0].Inspection_Question_id;

            if (ModelState.IsValid)
            {
                var endquestion = apiBaseResponseUrl + "/" + Id;
                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        StringContent content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
                        using (var Response = await client.PostAsync(apiBaseResponseUrl, content))
                        {
                            if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                            {

                                // Get Response Here

                                using (var FleetData = await client.GetAsync(endquestion))
                                {
                                    var result = Response.Content.ReadAsStringAsync().Result;

                                    
                                    var data1 = JsonConvert.DeserializeObject<QuestionDto>(FleetData.Content.ReadAsStringAsync().Result);
                                    return PartialView("Response", data1);
                                }

                            }
                            else
                            {
                                ModelState.Clear();
                                ModelState.AddModelError(string.Empty, "Invalid data");
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
            return PartialView();
        }

        [HttpPost]
        public async Task<JsonResult> SaveQuestionResponse(List<InspectionResponseDto> data)

        {
            //return Json(null);
            var Id = data[0].Inspection_Question_id;

            if (ModelState.IsValid)
            {
                var endquestion = apiBaseResponseUrl + "/" + Id;
                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        StringContent content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
                        using (var Response = await client.PostAsync(apiBaseResponseUrl, content))
                        {
                            if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                            {
                                return Json("Data Saved");
                            }
                            else
                            {
                                ModelState.Clear();
                                ModelState.AddModelError(string.Empty, "Invalid data");
                                return Json(ModelState);
                            }
                        }
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
            }
            return Json("Data Saved");
        }

        public async Task<PartialViewResult> GetOpContent()
        {
            return PartialView("Op_SuppliedContent");
        }

        [HttpPost]
        public async Task<IActionResult> FileUpload(IFormFile file)
        {
            await UploadFile(file);
            QuestionDto questionDto = new QuestionDto();


            return View("Index", questionDto);

        }

        public async Task<bool> UploadFile(IFormFile file)
        {

            string path = "";
            bool isCopied = false;
            try
            {
                if (file.Length > 0)
                {
                    string filename = Guid.NewGuid() + Path.GetExtension(file.FileName);
                    path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "Upload"));
                    using (var filestream = new FileStream(Path.Combine(path, filename), FileMode.Create))
                    {
                        await file.CopyToAsync(filestream);
                    }
                    isCopied = true;
                }
                else
                {
                    isCopied = false;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return isCopied;
        }
    }
}
