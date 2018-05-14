using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FeedbackApp.Models;
using FeedbackApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace FeedbackApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateSurvey(Survey survey, [FromServices] ISurveyService surveyService)
        {
            await surveyService.AddSurvey(survey);
            return RedirectToAction("Index", "CreateSurvey", new {id = survey.Id });
        }

    }
}