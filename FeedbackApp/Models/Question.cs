using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeedbackApp.Models
{
    public class Question
    {
       // public int Id { get; set; }
        public String Text { get; set; }
        public Answers[] PossibleAnswers { get; set; }
        public Answers Answer { get; set; }

        public Question()
        {
            PossibleAnswers = new Answers[] {Answers.BigUnsatisfied, Answers.SmallUnsatisfied, Answers.Neutral, Answers.SmallSatisfied, Answers.BigSatisfied};
        }
    }
}

