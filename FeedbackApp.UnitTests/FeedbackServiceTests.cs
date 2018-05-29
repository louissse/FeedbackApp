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
                Answer = Answers.BigSatisfied,
                QuestionId = 1
                
            };
            var question2 = new Question
            {
                Text = "Test2",
                Answer = Answers.BigSatisfied,
                QuestionId = 2

            };
            List<Question> answeredQuestions = new List<Question>();
            answeredQuestions.Add(question1);
            answeredQuestions.Add(question2);

            var conditions = new List<Condition>();
            conditions.Add(new Condition { Answer = question1.Answer, QuestionId = question1.QuestionId });
            conditions.Add(new Condition { Answer = question2.Answer, QuestionId = question2.QuestionId }); 

            var feedback1 = new Feedback
            {
                Priority = 1,
                Text = "Feedback prioritet 1",
                Conditions = conditions
            };
            var conditions2 = new List<Condition>();
            conditions2.Add(new Condition { Answer = question2.Answer, QuestionId = question2.QuestionId });
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
            Assert.True(realFeedback.Result[0] == feedbackList[0] && realFeedback.Result.Count == 1);
        }



        [Fact]
        public void GetFeedbackTest_TwoPriority1_True()
        {
            var feedbackService = new FeedbackService();
            var question1 = new Question
            {
                Text = "Test",
                Answer = Answers.BigSatisfied,
                QuestionId = 1
            };
            var question2 = new Question
            {
                Text = "Test2",
                Answer = Answers.BigSatisfied,
                QuestionId = 2
            };
            List<Question> answeredQuestions = new List<Question>();
            answeredQuestions.Add(question1);
            answeredQuestions.Add(question2);

            var conditions = new List<Condition>();
            conditions.Add(new Condition { Answer = question1.Answer, QuestionId = question1.QuestionId });
            conditions.Add(new Condition { Answer = question2.Answer, QuestionId = question2.QuestionId });

            var feedback1 = new Feedback
            {
                Priority = 1,
                Text = "Feedback prioritet 1",
                Conditions = conditions
            };
            var conditions2 = new List<Condition>();
            conditions2.Add(new Condition { Answer = question2.Answer, QuestionId = question2.QuestionId });
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
                Answer = Answers.BigSatisfied,
                QuestionId = 1
            };
            var question2 = new Question
            {
                Text = "Test2",
                Answer = Answers.BigSatisfied,
                QuestionId = 2
            };

            var condition1 = new Condition
            {
                QuestionId = 1,
                Answer = Answers.BigUnsatisfied
            };
            var condition2 = new Condition
            {
                QuestionId = 2,
                Answer = Answers.BigUnsatisfied
            };

            List<Question> answeredQuestions = new List<Question>();
            answeredQuestions.Add(question1);
            answeredQuestions.Add(question2);

            var conditions = new List<Condition>();
            conditions.Add(condition1);
            //conditions.Add(condition2); // Med to conditions

            var feedback1 = new Feedback
            {
                Priority = 1,
                Text = "Feedback prioritet 1",
                Conditions = conditions
            };


            var conditions2 = new List<Condition>();
            conditions2.Add(new Condition { Answer = question2.Answer, QuestionId = question2.QuestionId });
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


        [Theory]
        [MemberData(nameof(GetData))]
        public async void GetFeedBackTest(List<Feedback> feedbackList, List<Question> answeredQuestions, List<Feedback> expectedFeedback)
        {
            var feedbackService = new FeedbackService();
            var realFeedback = new List<Feedback>(await feedbackService.GetFeedback(feedbackList, answeredQuestions));
            Assert.True(realFeedback.Count == expectedFeedback.Count);
            foreach (var ef in expectedFeedback)
            {
                var feedBackObject = realFeedback.Find(rf => rf.Text == ef.Text);
                Assert.True(feedBackObject != null);
            }
        }

        public static IEnumerable<object[]> GetData()
        {
            var allData = new List<object[]>
                    {
                        //Test if we get priority 1
                        new object[]
                        {
                            new List<Feedback>()
                            {
                                new Feedback{ Text = "Priority 1", Priority = 1, Conditions = new List<Condition>(){ new Condition {QuestionId = 1, Answer = Answers.BigSatisfied } } },
                                new Feedback{ Text = "Priority 2", Priority = 2, Conditions = new List<Condition>(){ new Condition {QuestionId = 2, Answer = Answers.BigSatisfied } } },
                                new Feedback{ Text = "Priority 0", Priority = 0, Conditions = new List<Condition>() }
                            },

                            new List<Question>()
                            {
                                new Question(){QuestionId = 1, Answer = Answers.BigSatisfied}
                            },
                            new List<Feedback>()
                            {
                                new Feedback{ Text = "Priority 1", Priority = 1, Conditions = new List<Condition>(){ new Condition {QuestionId = 1, Answer = Answers.BigSatisfied } } },
                            },
                        },
                        //Test if we get priority 2
                        new object[]
                        {
                            new List<Feedback>()
                            {
                                new Feedback{ Text = "Priority 1", Priority = 1, Conditions = new List<Condition>(){ new Condition {QuestionId = 1, Answer = Answers.BigSatisfied } } },
                                new Feedback{ Text = "Priority 2", Priority = 2, Conditions = new List<Condition>(){ new Condition {QuestionId = 2, Answer = Answers.BigSatisfied } } },
                                new Feedback{ Text = "Priority 0", Priority = 0, Conditions = new List<Condition>() }
                            },

                            new List<Question>()
                            {
                                new Question(){QuestionId = 1, Answer = Answers.BigUnsatisfied},
                                new Question(){QuestionId = 2, Answer = Answers.BigSatisfied},

                            },
                            new List<Feedback>()
                            {
                                new Feedback{ Text = "Priority 2", Priority = 2, Conditions = new List<Condition>(){ new Condition {QuestionId = 2, Answer = Answers.BigSatisfied } } },
                            }
                        },
                        //Test if we get priority 0
                        new object[]
                        {
                            new List<Feedback>()
                            {
                                new Feedback{ Text = "Priority 1", Priority = 1, Conditions = new List<Condition>(){ new Condition {QuestionId = 1, Answer = Answers.BigSatisfied } } },
                                new Feedback{ Text = "Priority 2", Priority = 2, Conditions = new List<Condition>(){ new Condition {QuestionId = 2, Answer = Answers.BigSatisfied } } },
                                new Feedback{ Text = "Priority 0", Priority = 0, Conditions = new List<Condition>() }
                            },

                            new List<Question>()
                            {
                                new Question(){QuestionId = 1, Answer = Answers.BigUnsatisfied},
                                new Question(){QuestionId = 2, Answer = Answers.BigUnsatisfied},

                            },
                            new List<Feedback>()
                            {
                                new Feedback{ Text = "Priority 0", Priority = 0, Conditions = new List<Condition>() }
                            }
                        },
                        //Test the case where priority 0 is not set but no conditions are met
                        new object[]
                        {
                            new List<Feedback>()
                            {
                                new Feedback{ Text = "Priority 1", Priority = 1, Conditions = new List<Condition>(){ new Condition {QuestionId = 1, Answer = Answers.BigSatisfied } } },
                                new Feedback{ Text = "Priority 2", Priority = 2, Conditions = new List<Condition>(){ new Condition {QuestionId = 2, Answer = Answers.BigSatisfied } } },
                            },

                            new List<Question>()
                            {
                                new Question(){QuestionId = 1, Answer = Answers.BigUnsatisfied},
                                new Question(){QuestionId = 2, Answer = Answers.BigUnsatisfied},

                            },
                            new List<Feedback>()
                            {
                                new Feedback{ Text = "", Priority = 0, Conditions = new List<Condition>()}
                            }
                        },
                        //Test where two priority 1 and both are met
                         new object[]
                        {
                            new List<Feedback>()
                            {
                                new Feedback{ Text = "Priority 1", Priority = 1, Conditions = new List<Condition>(){ new Condition {QuestionId = 1, Answer = Answers.BigSatisfied } } },
                                new Feedback{ Text = "Priority 1 also", Priority = 1, Conditions = new List<Condition>(){ new Condition {QuestionId = 2, Answer = Answers.BigSatisfied } } },
                            },

                            new List<Question>()
                            {
                                new Question(){QuestionId = 1, Answer = Answers.BigSatisfied},
                                new Question(){QuestionId = 2, Answer = Answers.BigSatisfied},

                            },
                            new List<Feedback>()
                            {
                                new Feedback{ Text = "Priority 1", Priority = 1, Conditions = new List<Condition>(){ new Condition {QuestionId = 1, Answer = Answers.BigSatisfied } } },
                                new Feedback{ Text = "Priority 1 also", Priority = 1, Conditions = new List<Condition>(){ new Condition {QuestionId = 2, Answer = Answers.BigSatisfied } } },
                            },
                        },
                        //Test where two priority 1 and only one met
                         new object[]
                        {
                            new List<Feedback>()
                            {
                                new Feedback{ Text = "Priority 1", Priority = 1, Conditions = new List<Condition>(){ new Condition {QuestionId = 1, Answer = Answers.BigSatisfied } } },
                                new Feedback{ Text = "Priority 1 also", Priority = 1, Conditions = new List<Condition>(){ new Condition {QuestionId = 2, Answer = Answers.BigSatisfied } } },
                            },

                            new List<Question>()
                            {
                                new Question(){QuestionId = 1, Answer = Answers.BigUnsatisfied},
                                new Question(){QuestionId = 2, Answer = Answers.BigSatisfied }

                            },
                            new List<Feedback>()
                            {
                                new Feedback{ Text = "Priority 1 also", Priority = 1, Conditions = new List<Condition>(){ new Condition {QuestionId = 2, Answer = Answers.BigSatisfied } } },
                            }
                        },
                    };
            return allData;
        }
    }
}
