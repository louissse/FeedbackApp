using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FeedbackApp.Models.ViewModels
{
    public class FeedbackViewModel
    {
        public Guid SurveyId { get; set; }
        public String SurveyTitle { get; set; }
        public String SurveyDescription { get; set; }
        public List<Feedback> SurveyFeedback { get; set; }
        public int? FeedbackToEdit { get; set; }
        [Required]
        [Range(0,998)]
        public int ChosenPriority { get; set; }
        [Required]
        public string FeedbackText { get; set; }
        public List<Question> Conditions { get; set; } //These are conditions for the feedback to be shown


        public FeedbackViewModel()
        {
            Conditions = new List<Question>();


        }
    }
}
