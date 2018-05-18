using System.Collections.Generic;
using System.Threading.Tasks;
using FeedbackApp.Models;

namespace FeedbackApp.Services
{
    public interface IFeedbackService
    {
        Task<List<Question>> FindValidConditions(List<Question> conditions);
        Task<List<Feedback>> GetFeedback(List<Feedback> possibleFeedback, List<Question> answeredQuestions);
    }
}