using FeedbackApp.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeedbackApp.Services
{
    public class SurveyService : ISurveyService
    {
        private static ConcurrentBag<Survey> _surveyStore;

        static SurveyService()
        {
            _surveyStore = new ConcurrentBag<Survey>();
        }


        public Task<bool> AddSurvey(Survey survey)
        {
            _surveyStore.Add(survey);
            return Task.FromResult(true);
        }

        public async Task<List<Survey>> GetAll()
        {
            return new List<Survey>(_surveyStore);
        }

        public Task<Survey> Get(Guid id)
        {
            return Task.FromResult(_surveyStore.FirstOrDefault(x => x.Id == id));
        }

        public async Task<Survey> AddQuestion(Guid id, string question)
        {
            var questionModel = new Question();
            questionModel.Text = question;

            var survey = _surveyStore.FirstOrDefault(s => s.Id == id);
            //questionModel.Id = survey.Questions.Count();
            survey.Questions.Add(questionModel);

            _surveyStore = new ConcurrentBag<Survey>(_surveyStore.Where(s => s.Id != id))
                {
                    survey
                };

            return survey;
        }

        public async Task<Survey> EditQuestion(Guid id, int questionId, string question)
        {

            var survey = _surveyStore.FirstOrDefault(s => s.Id == id);
            survey.Questions[questionId].Text = question;

            _surveyStore = new ConcurrentBag<Survey>(_surveyStore.Where(s => s.Id != id))
                {
                    survey
                };

            return survey;
        }

        public async Task<Survey> DeleteQuestion(Guid id, int questionId)
        {

            var survey = _surveyStore.FirstOrDefault(s => s.Id == id);
            survey.Questions.RemoveAt(questionId);

            _surveyStore = new ConcurrentBag<Survey>(_surveyStore.Where(s => s.Id != id))
                {
                    survey
                };

            return survey;
        }

        public async Task<Survey> AddFeedback(Guid id, string feedback, List<Question> conditions, int priority)
        {
            var feedbackModel = new Feedback();
            feedbackModel.Text = feedback;
            feedbackModel.Priority = priority;
            feedbackModel.Conditions = conditions;

            var survey = _surveyStore.FirstOrDefault(s => s.Id == id);
            survey.Feedback.Add(feedbackModel);
            survey.Feedback = new List<Feedback>(survey.Feedback.OrderBy(f => f.Priority));
            _surveyStore = new ConcurrentBag<Survey>(_surveyStore.Where(s => s.Id != id))
                {
                    survey
                };

            return survey;

        }

        public async Task<Survey> EditFeedback(Guid id, string feedback, List<Question> conditions, int priority, int feedbackId)
        {
            var feedbackModel = new Feedback();
            feedbackModel.Text = feedback;
            feedbackModel.Priority = priority;
            feedbackModel.Conditions = conditions;

            var survey = _surveyStore.FirstOrDefault(s => s.Id == id);
            survey.Feedback[feedbackId] = feedbackModel;

            survey.Feedback = new List<Feedback>(survey.Feedback.OrderBy(f => f.Priority));
            _surveyStore = new ConcurrentBag<Survey>(_surveyStore.Where(s => s.Id != id))
                {
                    survey
                };

            return survey;
        }

        public async Task<Survey> DeleteFeedback(Guid id, int feedbackId)
        {

            var survey = _surveyStore.FirstOrDefault(s => s.Id == id);
            survey.Feedback.RemoveAt(feedbackId);

            _surveyStore = new ConcurrentBag<Survey>(_surveyStore.Where(s => s.Id != id))
                {
                    survey
                };

            return survey;
        }
    }
}
