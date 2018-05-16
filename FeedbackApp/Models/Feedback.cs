using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeedbackApp.Models
{
    public class Feedback
    {
        public string Text { get; set; }
        public List<Question> Conditions { get; set; }
        public int Priority { get; set; }
    }
}
