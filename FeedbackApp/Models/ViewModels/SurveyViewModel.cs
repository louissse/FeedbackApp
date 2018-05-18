using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeedbackApp.Models.ViewModels
{
    public class SurveyViewModel
    {
        public Guid SurveyId { get; set; }
        public List<Question> Questions { get; set; }

        public SurveyViewModel()
        {
            Questions = new List<Question>();
        }
    }

}
