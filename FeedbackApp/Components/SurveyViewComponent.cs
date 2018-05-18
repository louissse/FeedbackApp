﻿using FeedbackApp.Models;
using FeedbackApp.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeedbackApp.Components
{
    [ViewComponent(Name = "Survey")]
    public class SurveyViewComponent : ViewComponent
    {
        readonly ISurveyService _surveyService;

        public SurveyViewComponent(ISurveyService surveyService)
        {
            _surveyService = surveyService;
        }
        public async Task<IViewComponentResult> InvokeAsync(Guid id)
        {
            var survey = await _surveyService.Get(id);
            return View(survey);
        }
    }
}
