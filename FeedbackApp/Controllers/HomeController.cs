﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FeedbackApp.Models;
using FeedbackApp.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
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
            if (ModelState.IsValid)
            {
                await surveyService.AddSurvey(survey);
                return RedirectToAction("Index", "CreateSurvey", new {id = survey.Id });
            }
            else
            {
                return View("Index", survey);
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