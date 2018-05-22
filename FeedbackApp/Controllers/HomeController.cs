using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FeedbackApp.Models;
using FeedbackApp.Models.ViewModels;
using FeedbackApp.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FeedbackApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            _logger.LogInformation("Index page says hello");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddANewSurvey(HomeViewModel homeViewModel, [FromServices] ISurveyService surveyService)
        {
            if (ModelState.IsValid)
            {
                var survey = new Survey
                {
                    Title = homeViewModel.SurveyTitle,
                    Description = homeViewModel.SurveyDescription
                };
                await surveyService.AddSurvey(survey);
                return RedirectToAction("Index", "CreateSurvey", new {id = survey.Id });
            }
            else
            {
                return View("Index", homeViewModel);
            }
        }

        [HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            return LocalRedirect(returnUrl);
        }

    }
}