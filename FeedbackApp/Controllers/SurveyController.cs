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
        public IActionResult Index(Guid id)
        {
            var viewModel = new SurveyViewModel
            {
                SurveyId = id
            };
            ////Testing a question and answer
            //var question1 = new Question();
            //question1.Text = "Hvor glad er du for katte?";
            ////question1.Id = 1;
            //var question2 = new Question();
            //question2.Text = "Hvor godt kan du lide mad?";
            ////question2.Id = 2;

            //var questionnaireModel = new Survey();
            //questionnaireModel.Questions.Add(question1);
            //questionnaireModel.Questions.Add(question2);

            return View(viewModel);
        }

        //Testing question answer
        [HttpPost]
        public async Task<IActionResult> Index(Survey surveyModel)
        {
            //Tilføj validering

            var originalSurvey = await _surveyService.Get(surveyModel.Id);
            var feedback = new List<Feedback>(await _feedbackService.GetFeedback(originalSurvey.Feedback, surveyModel.Questions));
            return View("SurveyFeedback", feedback);

        }

        //public async Task<IActionResult> SurveyFeedback(Guid id, List<Question> answeredQuestions)
        //{
        //    
        //    );
        //    return View(feedback);
        //}
    }
}