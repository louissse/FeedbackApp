using System.Collections.Generic;
using System.Threading.Tasks;
using FeedbackApp.Models;

namespace FeedbackApp.Services
{
    public interface IFeedbackService
    {
        Task<List<Condition>> FindValidConditions(List<Question> questions);
        Task<List<Feedback>> GetFeedback(List<Feedback> possibleFeedback, List<Question> answeredQuestions);
    }
}