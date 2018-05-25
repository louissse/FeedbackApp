using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeedbackApp.Models
{
    public class Condition
    {
        public int ConditionId { get; set; }
        public int QuestionId { get; set; }
        public Answers Answer { get; set; }
    }
}
