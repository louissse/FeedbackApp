using System;
using System.Threading.Tasks;
using FeedbackApp.Models;

namespace FeedbackApp.Services
{
    public interface ISurveyService
    {
        Task<bool> AddSurvey(Survey survey);
        Task<Survey> Get(Guid id);
        Task<Survey> AddQuestion(Guid id, string question);
        Task<Survey> EditQuestion(Guid id, int questionId, string question);

    }
}