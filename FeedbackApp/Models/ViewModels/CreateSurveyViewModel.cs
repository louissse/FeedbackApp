using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FeedbackApp.Models.ViewModels
{
    public class CreateSurveyViewModel
    {
        public Guid SurveyId { get; set; }
        [Required]
        public String QuestionText { get; set; }
        public String SurveyTitle { get; set; }
        public String SurveyDescription { get; set; }
        public List<Question> SurveyQuestions { get; set; }
        public int? QuestionToEdit { get; set; }

        public CreateSurveyViewModel()
        {
            SurveyQuestions = new List<Question>();
        }
    }
}
