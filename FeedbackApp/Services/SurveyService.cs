using FeedbackApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FeedbackApp.Data;

namespace FeedbackApp.Services
{
    public class SurveyService : ISurveyService
    {
        private static ConcurrentBag<Survey> _surveyStore;
        private DbContextOptions<FeedbackContext> _dbContextOptions;

        static SurveyService()
        {
            _surveyStore = new ConcurrentBag<Survey>();
        }

        public SurveyService(DbContextOptions<FeedbackContext> dbContextOptions)
        {
            _dbContextOptions = dbContextOptions;
        }


        public async Task<bool> AddSurvey(Survey survey)
        {

            using (var db = new FeedbackContext(_dbContextOptions))
            {
                db.survey.Add(survey);
                await db.SaveChangesAsync();
                return true;
            }
            
        }

        public async Task<bool> DeleteSurvey(Guid id)
        {

            using (var db = new FeedbackContext(_dbContextOptions))
            {
                var survey = await db.survey.Include(s => s.Questions).Include(s => s.Feedback)
                        .ThenInclude(f => f.Conditions).FirstOrDefaultAsync(
                    x => x.Id == id);
                db.survey.Remove(survey);
                await db.SaveChangesAsync();
                return true;
            }

        }

        public async Task<List<Survey>> GetAll()
        {
            using (var db = new FeedbackContext(_dbContextOptions))
            {
                return await db.survey.ToListAsync();
            }
            
        }

        public async Task<Survey> Get(Guid id)
        {
            using (var db = new FeedbackContext(_dbContextOptions))
            {
                var survey = await db.survey.Include(s => s.Questions).FirstOrDefaultAsync(
                x => x.Id == id);
                survey.Feedback = new List<Feedback>(db.feedback.Include(f => f.Conditions).Where(f => f.SurveyId == survey.Id));
                return survey;
            }
        }

        public async Task<Survey> AddQuestion(Guid id, string question)
        {
            using (var db = new FeedbackContext(_dbContextOptions))
            {
                var survey = db.survey.FirstOrDefault<Survey>(x => x.Id == id);
                survey.Questions.Add(new Question { Text = question });
                await db.SaveChangesAsync();
                return survey;
            }

            
        }

        public async Task EditQuestion(Guid id, int questionId, string questionText)
        {
            using (var db = new FeedbackContext(_dbContextOptions))
            {
                var question = db.question.FirstOrDefault(q => q.QuestionId == questionId);
                question.Text = questionText;
                await db.SaveChangesAsync();
            }
        }

        public async Task DeleteQuestion(Guid id, int questionId)
        {
            try
            {
                using (var db = new FeedbackContext(_dbContextOptions))
                {
                    var question = db.question.FirstOrDefault(q => q.QuestionId == questionId);
                    db.question.Remove(question);
                    await db.SaveChangesAsync();
                }
            }
            catch (Exception)
            {

                throw;
            }

        }

        public async Task<Survey> AddFeedback(Guid id, string feedback, List<Condition> conditions, int priority)
        {
            var feedbackModel = new Feedback();
            feedbackModel.Text = feedback;
            feedbackModel.Priority = priority;
            feedbackModel.Conditions = conditions;

            using (var db = new FeedbackContext(_dbContextOptions))
            {
                var survey = db.survey.FirstOrDefault<Survey>(x => x.Id == id);
                survey.Feedback.Add(feedbackModel);
                await db.SaveChangesAsync();
                return survey;
            }

        }

        public async Task EditFeedback(Guid id, string feedbackText, List<Condition> conditions, int priority, int feedbackId)
        {

            using (var db = new FeedbackContext(_dbContextOptions))
            {
                var feedback = db.feedback.Include(f => f.Conditions).FirstOrDefault(f => f.FeedbackId == feedbackId);
                db.feedback.Remove(feedback);

                var feedbackModel = new Feedback();
                feedbackModel.Text = feedbackText;
                feedbackModel.Priority = priority;
                feedbackModel.Conditions = conditions;

                var survey = db.survey.FirstOrDefault<Survey>(x => x.Id == id);
                survey.Feedback.Add(feedbackModel);
                await db.SaveChangesAsync();

            }
        }

        public async Task DeleteFeedback(Guid id, int feedbackId)
        {
            using (var db = new FeedbackContext(_dbContextOptions))
            {
                var feedback = db.feedback.Include(f => f.Conditions).FirstOrDefault(f => f.FeedbackId == feedbackId);
                db.feedback.Remove(feedback);

                await db.SaveChangesAsync();

            }

        }
    }
}
