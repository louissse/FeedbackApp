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
        public async Task<IActionResult> Index([FromServices] ISurveyService surveyService, Guid id, int? questionId)
        {
            var survey = await surveyService.Get(id);
            var surveyViewModel = new CreateSurveyViewModel();
            surveyViewModel.Survey = survey;
            surveyViewModel.QuestionToEdit = questionId;
            return View(surveyViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddOrEditQuestion([FromServices] ISurveyService surveyService, string question, Guid id, int? questionId)
        {
            if (questionId != null)
            {
                var survey = await surveyService.EditQuestion(id, questionId?? 0, question);
                return RedirectToAction("Index", new { id = survey.Id });
            } else
            {
                var survey = await surveyService.AddQuestion(id, question);
                return RedirectToAction("Index", new { id = survey.Id });
            }
           
        }

        public async Task<IActionResult> EditQuestion([FromServices] ISurveyService surveyService, Guid id, int questionId)
        {
            var survey = await surveyService.Get(id);
            var surveyViewModel = new CreateSurveyViewModel();
            surveyViewModel.Survey = survey;
            surveyViewModel.QuestionToEdit = questionId;
            return RedirectToAction("Index", surveyViewModel);
        }
    }
}