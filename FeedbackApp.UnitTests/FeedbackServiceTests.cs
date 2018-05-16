using FeedbackApp.Models;
using FeedbackApp.Services;
using System;
using System.Collections.Generic;
using Xunit;

namespace FeedbackApp.UnitTests
{
    public class FeedbackServiceTests
    {
        [Fact]
        public void GetFeedbackTest_True()
        {
            var feedbackService = new FeedbackService();
            var question1 = new Question
            {
                Text = "Test",
                Answer = Answers.BigSatisfied
            };
            var question2 = new Question
            {
                Text = "Test2",
                Answer = Answers.BigSatisfied
            };
            List<Question> answeredQuestions = new List<Question>();
            answeredQuestions.Add(question1);
            answeredQuestions.Add(question2);

            var conditions = new List<Question>();
            conditions.Add(question1);
            conditions.Add(question2); // Med to conditions

            var feedback1 = new Feedback
            {
                Priority = 1,
                Text = "Feedback prioritet 1",
                Conditions = conditions
            };
            var conditions2 = new List<Question>();
            conditions2.Add(question2);
            var feedback2 = new Feedback
            {
                Priority = 2,
                Text = "Feedback prioritet 2",
                Conditions = conditions2
            };
            var feedbackList = new List<Feedback>();
            feedbackList.Add(feedback1);
            feedbackList.Add(feedback2);

            var realFeedback = feedbackService.GetFeedback(feedbackList, answeredQuestions);
            Assert.True(realFeedback.Result[0] == feedbackList[0]);
        }

        [Fact]
        public void GetFeedbackTest_TwoPriority1_True()
        {
            var feedbackService = new FeedbackService();
            var question1 = new Question
            {
                Text = "Test",
                Answer = Answers.BigSatisfied
            };
            var question2 = new Question
            {
                Text = "Test2",
                Answer = Answers.BigSatisfied
            };
            List<Question> answeredQuestions = new List<Question>();
            answeredQuestions.Add(question1);
            answeredQuestions.Add(question2);

            var conditions = new List<Question>();
            conditions.Add(question1);
            conditions.Add(question2); // Med to conditions

            var feedback1 = new Feedback
            {
                Priority = 1,
                Text = "Feedback prioritet 1",
                Conditions = conditions
            };
            var conditions2 = new List<Question>();
            conditions2.Add(question2);
            var feedback2 = new Feedback
            {
                Priority = 1,
                Text = "Feedback prioritet 2",
                Conditions = conditions2
            };
            var feedbackList = new List<Feedback>();
            feedbackList.Add(feedback1);
            feedbackList.Add(feedback2);

            var realFeedback = feedbackService.GetFeedback(feedbackList, answeredQuestions);
            Assert.True(realFeedback.Result[0] == feedbackList[0] && realFeedback.Result[1] == feedbackList[1]);
        }

        [Fact]
        public void GetFeedbackTest_PriorityOneIsNotMet_True()
        {
            var feedbackService = new FeedbackService();
            var question1 = new Question
            {
                Text = "Test",
                Answer = Answers.BigSatisfied
            };
            var question2 = new Question
            {
                Text = "Test2",
                Answer = Answers.BigSatisfied
            };

            var condition1 = new Question
            {
                Text = "Test",
                Answer = Answers.BigUnsatisfied
            };
            var condition2 = new Question
            {
                Text = "Test2",
                Answer = Answers.BigUnsatisfied
            };

            List<Question> answeredQuestions = new List<Question>();
            answeredQuestions.Add(question1);
            answeredQuestions.Add(question2);

            var conditions = new List<Question>();
            conditions.Add(condition1);
            //conditions.Add(condition2); // Med to conditions

            var feedback1 = new Feedback
            {
                Priority = 1,
                Text = "Feedback prioritet 1",
                Conditions = conditions
            };


            var conditions2 = new List<Question>();
            conditions2.Add(question2);
            var feedback2 = new Feedback
            {
                Priority = 2,
                Text = "Feedback prioritet 2",
                Conditions = conditions2
            };
            var feedbackList = new List<Feedback>();
            feedbackList.Add(feedback1);
            feedbackList.Add(feedback2);

            var realFeedback = feedbackService.GetFeedback(feedbackList, answeredQuestions);
            Assert.True(realFeedback.Result[0] == feedbackList[1]);
        }
    }
}
