using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FeedbackApp.Models;
using FeedbackApp.Models.ViewModels;
using FeedbackApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FeedbackApp.Controllers
{
    public class CreateSurveyController : Controller
    {
        readonly ISurveyService _surveyService;
        readonly IFeedbackService _feedbackService;


        public CreateSurveyController(ISurveyService surveyService, IFeedbackService feedbackService)
        {
            _surveyService = surveyService;
            _feedbackService = feedbackService;

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
                var survey = await _surveyService.EditQuestion(id, questionId ?? 0, question);
                return RedirectToAction("Index", new { id = survey.Id });
            }
            else
            {
                var survey = await _surveyService.AddQuestion(id, question);
                return RedirectToAction("Index", new { id = survey.Id });
            }

        }

        public async Task<IActionResult> Feedback(Guid id, int? feedbackId)
        {
            var survey = await _surveyService.Get(id);
            var feedbackViewModel = new FeedbackViewModel();
            feedbackViewModel.Survey = survey;
            feedbackViewModel.Conditions = new List<Question>(survey.Questions);
            
            if (feedbackId != null)
            {
                int feedbackIdInt = feedbackId ?? default(int);
                feedbackViewModel.FeedbackText = survey.Feedback[feedbackIdInt].Text;
                feedbackViewModel.FeedbackToEdit = feedbackIdInt;
                feedbackViewModel.ChosenPriority = survey.Feedback[feedbackIdInt].Priority;
                feedbackViewModel.Conditions = FilterConditions(feedbackViewModel.Conditions, survey.Feedback[feedbackIdInt].Conditions);

            } else
            {
                feedbackViewModel.FeedbackText = "";
            }

            return View(feedbackViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddOrEditFeedback(FeedbackViewModel feedbackViewModel, Guid id, int? feedbackId)
        {
            if (!ModelState.IsValid)
            {
                feedbackViewModel.Survey = await _surveyService.Get(id);
                return View("Feedback", feedbackViewModel);
            }

            var conditions = await _feedbackService.FindValidConditions(feedbackViewModel.Conditions);

            if (feedbackId != null)
            {
                var survey = await _surveyService.EditFeedback(id, feedbackViewModel.FeedbackText, conditions, feedbackViewModel.ChosenPriority, feedbackId ?? 0);
                return RedirectToAction("Feedback", new { id = id });
            }
            else
            {
                var survey = await _surveyService.AddFeedback(id, feedbackViewModel.FeedbackText, conditions, feedbackViewModel.ChosenPriority); //TODO
                return RedirectToAction("Feedback", new { id = id});
            }

        }

        private List<Question> FilterConditions(List<Question> conditionList, List<Question> filterList)
        {
            var filteredConditions = new List<Question>(conditionList);
           
            for (var i = 0 ; i < conditionList.Count(); i++)
            {
                foreach (var filter in filterList)
                {
                    if (filter.Text == conditionList[i].Text)
                    {
                        filteredConditions[i] = filter;
                    }
                }
            }
            return filteredConditions;
        } 
        //private List<SelectListItem> CreatePriorityList(int numberOfQuestions)
        //{
        //    var priorityList = new List<SelectListItem>();

        //    for (int i = 0; i <= numberOfQuestions; i++)
        //    {
        //        var priority = new SelectListItem { Value = i.ToString(), Text = i.ToString() };
        //        priorityList.Add(priority);

        //    }
        //    return priorityList;
        //} Obsolete

    }
}