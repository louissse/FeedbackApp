using FeedbackApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeedbackApp.Services
{
    public class FeedbackService
    {
        public Task<List<Feedback>> GetFeedback(List<Feedback> possibleFeedback, List<Question> answeredQuestions)
        {
            List<Feedback> feedback = new List<Feedback>();
            int i = 1;
            do
            {
                var priorityFeedback = possibleFeedback.Where(f => f.Priority == i);

                foreach (var feedbackItem in priorityFeedback)
                {
                    var conditionsIsMet = true;
                    foreach (var condition in feedbackItem.Conditions)
                    {

                        var question = answeredQuestions.FirstOrDefault(q => q.Text == condition.Text);
                        if (question.Answer != condition.Answer)
                        {
                            conditionsIsMet = false;
                        }
                    }
                    if (conditionsIsMet)
                    {
                        feedback.Add(feedbackItem);
                    }
                }

                i++;
                if (i > possibleFeedback.Count() + 1)
                {
                    feedback.AddRange(possibleFeedback.Where(f => f.Priority == 0));
                }

            } while (feedback.Count() == 0 && i < possibleFeedback.Count() + 1);
            return Task.FromResult(feedback);
        }
    }
}
