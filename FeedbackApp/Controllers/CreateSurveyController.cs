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
            //Get the relevant survey
            var survey = await _surveyService.Get(id);

            //Decorate the ViewModel
            var createSurveyViewModel = new CreateSurveyViewModel
            {
                SurveyId = survey.Id,
                QuestionToEdit = questionId,
                QuestionText = questionId == null ? "" : survey.Questions.FirstOrDefault(q => q.QuestionId == questionId).Text,
                SurveyTitle = survey.Title,
                SurveyDescription = survey.Description,
                SurveyQuestions = survey.Questions
            };
            //return the view
            return View(createSurveyViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddOrEditQuestion(CreateSurveyViewModel createSurveyViewModel, Guid id, int? questionId)
        {
            if (questionId != null && ModelState.IsValid)
            {
                await _surveyService.EditQuestion(id, questionId ?? 0, createSurveyViewModel.QuestionText);
                return RedirectToAction("Index", new { id = id });
            }
            else if (ModelState.IsValid)
            {
                var survey = await _surveyService.AddQuestion(id, createSurveyViewModel.QuestionText);
                return RedirectToAction("Index", new { id = survey.Id });
            }
            else
            {
                //Get the survey
                var survey = await _surveyService.Get(id);
                //Decorate the ViewModel
                createSurveyViewModel.SurveyId = survey.Id;
                createSurveyViewModel.QuestionToEdit = questionId;
                createSurveyViewModel.QuestionText = questionId == null ? "" : survey.Questions.FirstOrDefault(q => q.QuestionId == questionId).Text;
                createSurveyViewModel.SurveyTitle = survey.Title;
                createSurveyViewModel.SurveyDescription = survey.Description;
                createSurveyViewModel.SurveyQuestions = survey.Questions;
                //Return the Index view
                return View("Index", createSurveyViewModel);
            }

        }

        public async Task<IActionResult> DeleteQuestion(Guid id, int questionId)
        {
            await _surveyService.DeleteQuestion(id, questionId);
            return RedirectToAction("Index", new { id = id });

        }

        public async Task<IActionResult> Feedback(Guid id, int? feedbackId)
        {
            var survey = await _surveyService.Get(id);
            var feedbackViewModel = new FeedbackViewModel
            {
                SurveyId = survey.Id,
                SurveyFeedback = survey.Feedback,
                FeedbackText = "",
                SurveyTitle = survey.Title,
                SurveyDescription = survey.Description,
                PossibleConditions = new List<Question>(survey.Questions) 
            };

            //When we want to edit a feedback
            if (feedbackId != null)
            {
                var feedback = survey.Feedback.FirstOrDefault(f => f.FeedbackId == feedbackId);
                feedbackViewModel.FeedbackText = feedback.Text;
                feedbackViewModel.FeedbackToEdit = feedbackId;
                feedbackViewModel.ChosenPriority = feedback.Priority;
                feedbackViewModel.PossibleConditions = FilterConditions(survey.Questions, feedback.Conditions);
            }
            return View(feedbackViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddOrEditFeedback(FeedbackViewModel feedbackViewModel, Guid id, int? feedbackId)
        {
            if (!ModelState.IsValid)
            {
                var survey = await _surveyService.Get(id);
                
                //The SurveyFeedback is not bound to the model in the View as it is a complex object
                feedbackViewModel.SurveyFeedback = survey.Feedback;
                return View("Feedback", feedbackViewModel);
            }

            //Get a list of Conditions from the possible (Question)conditions
            var conditions = await _feedbackService.FindValidConditions(feedbackViewModel.PossibleConditions);
            if (feedbackId != null)
            {
                await _surveyService.EditFeedback(id, feedbackViewModel.FeedbackText, conditions, feedbackViewModel.ChosenPriority, feedbackId ?? 0);
                return RedirectToAction("Feedback", new { id = id });
            }
            else
            {
                var survey = await _surveyService.AddFeedback(id, feedbackViewModel.FeedbackText, conditions, feedbackViewModel.ChosenPriority); //TODO
                return RedirectToAction("Feedback", new { id = id });
            }

        }

        public async Task<IActionResult> DeleteFeedback(Guid id, int feedbackId)
        {
            await _surveyService.DeleteFeedback(id, feedbackId);
            return RedirectToAction("Feedback", new { id = id });

        }

        private List<Question> FilterConditions(List<Question> conditionList, List<Condition> filterList)
        {
            var filteredConditions = new List<Question>(conditionList);

            for (var i = 0; i < conditionList.Count(); i++)
            {
                foreach (var filter in filterList)
                {
                    if (filter.QuestionId == conditionList[i].QuestionId)
                    {
                        filteredConditions[i].Answer = filter.Answer;
                    }
                }
            }
            return filteredConditions;
        }
    }
}