using FeedbackApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeedbackApp.Services
{
    public class FeedbackService : IFeedbackService
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
                    var conditionsIsMet = false;
                    foreach (var condition in feedbackItem.Conditions)
                    {

                        var question = answeredQuestions.FirstOrDefault(q => q.Text == condition.Text);
                        if (question.Answer == condition.Answer)
                        {
                            conditionsIsMet = true;
                        }
                    }
                    if (conditionsIsMet)
                    {
                        feedback.Add(feedbackItem);
                    }
                }

                
                if (i == 999)
                {
                    feedback.Add(possibleFeedback.Find(f => f.Priority == 0)?? new Feedback{Text = "", Priority = 0, Conditions = new List<Question>()} );
                    break;
                }
                i++;
            } while (feedback.Count() == 0);
            return Task.FromResult(feedback);
        }

        public Task<List<Question>> FindValidConditions(List<Question> conditions)
        {
            var validConditions = new List<Question>(conditions.Where(f => f.Answer != Answers.NoAnswer));                
            return Task.FromResult(validConditions);
        }
    }
}
