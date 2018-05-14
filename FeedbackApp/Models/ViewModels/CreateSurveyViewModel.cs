using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeedbackApp.Models.ViewModels
{
    public class CreateSurveyViewModel
    {
        public Survey Survey { get; set; }
        public int? QuestionToEdit { get; set; }
    }
}
