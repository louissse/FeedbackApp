using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeedbackApp.Models
{
    public class Feedback
    {
        public int FeedbackId { get; set; } //For DB 
        public string Text { get; set; }
        public List<Condition> Conditions { get; set; }
        public int Priority { get; set; }

        public Guid SurveyId { get; set; }
    }
}
