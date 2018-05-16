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

    }
}
