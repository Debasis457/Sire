using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Sire.Data.Dto.Master;
using Sire.Data.Dto.Question;
using Sire.Data.Dto.Training;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;


namespace Sire.Web.Controllers
{
    [Authorize]
    public class TrainingFlowController : Controller
    {
        private readonly ILogger<TrainingFlowController> _logger;
        private readonly Microsoft.Extensions.Configuration.IConfiguration _iConfig;
        string apiBaseUrl = string.Empty;
        string apiBaseQuestionUrl = string.Empty;
        string apiBaseTrainingResponseUrl = string.Empty;

        public TrainingFlowController(ILogger<TrainingFlowController> logger,
            Microsoft.Extensions.Configuration.IConfiguration iConfig
            )
        {
            _logger = logger;
            _iConfig = iConfig;
            apiBaseUrl = _iConfig.GetValue<string>("apiUrl:url").ToString() + "/training";

            apiBaseQuestionUrl = _iConfig.GetValue<string>("apiUrl:url").ToString() + "/Question";
            apiBaseTrainingResponseUrl = _iConfig.GetValue<string>("apiUrl:url").ToString() + "/TraningResponse";
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


        public async Task<PartialViewResult> GetTasks()
        {
            var trainingNumber = Convert.ToInt32(TempData["TrainingNumber"]);
            var trainingId = Convert.ToInt32(TempData["TrainingId"]);
            var questionId = Convert.ToInt32(TempData["QuestionId"]);
            TraningResponseDto _traningResponseDto = new TraningResponseDto();
            _traningResponseDto.Question_Id = questionId;
            _traningResponseDto.Trainee_Id = Convert.ToInt32(HttpContext.Session.GetString("UserId"));
            _traningResponseDto.Training_Id = trainingNumber;
            TempData.Keep();

            var taskSubmittedDataUrl = apiBaseTrainingResponseUrl + "/GetTriningResponseByTraningByUser/" + trainingNumber + "/" + questionId + "/" + _traningResponseDto.Trainee_Id;

            using (HttpClient client = new())
            {
                using var Response = await client.GetAsync(taskSubmittedDataUrl);
                if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var result = Response.Content.ReadAsStringAsync().Result;
                    if (result.Length > 2)
                    {
                        var data = JsonConvert.DeserializeObject<IList<TraningResponseDto>>(result);
                        return PartialView("Tasks", data);
                    }
                }
                else
                {
                    return PartialView("Tasks");
                }
            }
            /* var endquestion = apiBaseQuestionUrl + "/" + Id;

             using (HttpClient client = new HttpClient())
             {
                 using (var Response = await client.GetAsync(endquestion))
                 {
                     if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                     {
                         var result = Response.Content.ReadAsStringAsync().Result;

                         var data = JsonConvert.DeserializeObject<QuestionDto>(Response.Content.ReadAsStringAsync().Result);
 */


            return PartialView("Tasks");
            /*}
            else
            {
                ModelState.Clear();
                ModelState.AddModelError(string.Empty, "Invalid Data");
                return PartialView();
            }
        }
    }*/
        }

        public async Task<PartialViewResult> GetOpContent()
        {
            return PartialView("Op_SuppliedContent");
        }

        /* [HttpPost]
         public async Task<IActionResult> FileUpload(IFormFile file)
         {
             await UploadFile(file);
             return RedirectToAction("GetDetails", "TrainingQuestion");

         }*/

        [HttpGet]
        public async Task<IActionResult> DownloadFile()
        {

            string filePath = "~/Upload/abc.pdf";
            Response.Headers.Add("Content-Disposition", "inline; filename=abc.pdf");
            return File(filePath, "application/pdf");
        }

        [HttpPost]
        public async Task<IActionResult> SaveTask()
        {
            var trainingNumber = Convert.ToInt32(TempData["TrainingNumber"]);
            var trainingId = Convert.ToInt32(TempData["TrainingId"]);
            TraningResponseDto _traningResponseDto = new()
            {
                Question_Id = Convert.ToInt32(TempData["QuestionId"]),
                Trainee_Id = Convert.ToInt32(HttpContext.Session.GetString("UserId")),
                Training_Id = trainingNumber
            };
            TempData.Keep();

            try
            {
                using HttpClient client = new();

                StringContent content = new(JsonConvert.SerializeObject(_traningResponseDto), Encoding.UTF8, "application/json");

                using var Response = await client.PostAsync(apiBaseTrainingResponseUrl, content);
                if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                {

                }
                else
                {
                    ModelState.Clear();
                    ModelState.AddModelError(string.Empty, "Invalid Data");
                    return PartialView("Tasks");
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return RedirectToAction("Index", "TrainingQuestion", new { @id = trainingId });
        }

    }
}
