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
                    var conditionsIsMet = true;
                    foreach (var condition in feedbackItem.Conditions)
                    {

                        var question = answeredQuestions.FirstOrDefault(q => q.QuestionId == condition.QuestionId);
                        if (question.Answer != condition.Answer)
                        {
                            conditionsIsMet = false;
                        }
                    }
                    if (conditionsIsMet && feedbackItem.Conditions.Count() != 0)
                    {
                        feedback.Add(feedbackItem);
                    }
                }

                
                if (i == 999)
                {
                    feedback.Add(possibleFeedback.Find(f => f.Priority == 0)?? new Feedback{Text = "", Priority = 0, Conditions = new List<Condition>()} );
                    break;
                }
                i++;
            } while (feedback.Count() == 0);
            return Task.FromResult(feedback);
        }

        public Task<List<Condition>> FindValidConditions(List<Question> questions)
        {
            var validConditions = new List<Question>(questions.Where(f => f.Answer != Answers.NoAnswer));

            var conditions = new List<Condition>();
            foreach (var question in validConditions)
            {
                var condition = new Condition
                {
                    QuestionId = question.QuestionId,
                    Answer = question.Answer
                };
                conditions.Add(condition);
            }

            return Task.FromResult(conditions);
        }
    }
}
