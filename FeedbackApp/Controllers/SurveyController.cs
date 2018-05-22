using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FeedbackApp.Models;
using FeedbackApp.Models.ViewModels;
using FeedbackApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace FeedbackApp.Controllers
{
    public class SurveyController : Controller
    {
        readonly ISurveyService _surveyService;
        readonly IFeedbackService _feedbackService;

        public SurveyController(ISurveyService surveyService, IFeedbackService feedbackService)
        {
            _surveyService = surveyService;
            _feedbackService = feedbackService;
        }
        public async Task<IActionResult> Index(Guid id)
        {
            var survey = await _surveyService.Get(id);
            var viewModel = new SurveyViewModel
            {
                SurveyId = id,
                Questions = survey.Questions
            };
            return View(viewModel);
        }


        [HttpPost]
        public async Task<IActionResult> Index(SurveyViewModel surveyViewModel)
        {
            if (surveyViewModel.Questions.Where(q => q.Answer == Answers.NoAnswer).Count() != 0)
            {
                surveyViewModel.ErrorMessage = "You must answer all questions, thank you.";
                return View(surveyViewModel);
            }

            var originalSurvey = await _surveyService.Get(surveyViewModel.SurveyId);
            var feedback = new List<Feedback>(await _feedbackService.GetFeedback(originalSurvey.Feedback, surveyViewModel.Questions));
            return View("SurveyFeedback", feedback);

        }
    }
}