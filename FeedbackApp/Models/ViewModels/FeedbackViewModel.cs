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
        public Survey Survey { get; set; }
        public int? FeedbackToEdit { get; set; }
        [Required]
        [Range(0,1000)]
        public int ChosenPriority { get; set; }
        [Required]
        public string FeedbackText { get; set; }
        public List<Question> Conditions { get; set; }

        public FeedbackViewModel()
        {
            Conditions = new List<Question>();
        }
    }
}
