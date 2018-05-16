using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FeedbackApp.Models.ViewModels;
using FeedbackApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace FeedbackApp.Controllers
{
    public class CreateSurveyController : Controller
    {
        readonly ISurveyService _surveyService;

        public CreateSurveyController(ISurveyService surveyService)
        {
            _surveyService = surveyService;
        }

        public async Task<IActionResult> Index(Guid id, int? questionId)
        {
            var survey = await _surveyService.Get(id);
            var surveyViewModel = new CreateSurveyViewModel();
            surveyViewModel.Survey = survey;
            surveyViewModel.QuestionToEdit = questionId;
            return View(surveyViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddOrEditQuestion(string question, Guid id, int? questionId)
        {
            if (questionId != null)
            {
                var survey = await _surveyService.EditQuestion(id, questionId?? 0, question);
                return RedirectToAction("Index", new { id = survey.Id });
            } else
            {
                var survey = await _surveyService.AddQuestion(id, question);
                return RedirectToAction("Index", new { id = survey.Id });
            }
           
        }

        public IActionResult Feedback(Guid id)
        {
            return View();
        }
    }
}