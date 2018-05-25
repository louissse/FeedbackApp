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
        Task<bool> DeleteSurvey(Guid id);
        Task<Survey> AddQuestion(Guid id, string question);
        Task EditQuestion(Guid id, int questionId, string question);
        Task DeleteQuestion(Guid id, int questionId);
        Task<Survey> AddFeedback(Guid id, string feedback, List<Condition> conditions, int priority);
        Task EditFeedback(Guid id, string feedback, List<Condition> conditions, int priority, int feedbackId);
        Task DeleteFeedback(Guid id, int feedbackId);

    }
}