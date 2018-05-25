using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FeedbackApp.Models
{
    public class Question
    {
        public int QuestionId { get; set; } //for DB
        public String Text { get; set; }
        [NotMapped]
        public Answers[] PossibleAnswers { get; set; }
        public Answers Answer { get; set; }

        public Question()
        {
            PossibleAnswers = new Answers[] {Answers.BigUnsatisfied, Answers.SmallUnsatisfied, Answers.Neutral, Answers.SmallSatisfied, Answers.BigSatisfied};
        }
    }
}

