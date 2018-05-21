using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FeedbackApp.Models;

namespace FeedbackApp.Services
{
    public interface ISurveyService
    {
        Task<bool> AddSurvey(Survey survey);
        Task<List<Survey>> GetAll();
        Task<Survey> Get(Guid id);
        Task<Survey> AddQuestion(Guid id, string question);
        Task<Survey> EditQuestion(Guid id, int questionId, string question);
        Task<Survey> DeleteQuestion(Guid id, int questionId);
        Task<Survey> AddFeedback(Guid id, string feedback, List<Question> conditions, int priority);
        Task<Survey> EditFeedback(Guid id, string feedback, List<Question> conditions, int priority, int feedbackId);
        Task<Survey> DeleteFeedback(Guid id, int feedbackId);

    }
}