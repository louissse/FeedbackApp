using FeedbackApp.Models;
using FeedbackApp.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeedbackApp.Components
{
    [ViewComponent(Name = "SurveyTable")]

    public class SurveyTableViewComponent : ViewComponent
    {
        readonly ISurveyService _surveyService;

        public SurveyTableViewComponent(ISurveyService surveyService)
        {
            _surveyService = surveyService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
  
            var listOfSurveys = new List<Survey>(await _surveyService.GetAll());

            return View(listOfSurveys);
        }
    }
}
