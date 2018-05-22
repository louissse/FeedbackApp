using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FeedbackApp.Models.ViewModels
{
    public class HomeViewModel
    {
        [Required(ErrorMessage = "SurveyTitleErrorMessage")]
        [StringLength(50, MinimumLength = 3)]
        [Display(Name = "SurveyTitle")]
        public string SurveyTitle { get; set; }
        [Required(ErrorMessage = "SurveyDescriptionErrorMessage")]
        [StringLength(500, MinimumLength = 3)]
        [Display(Name = "SurveyDescription")]
        public string SurveyDescription { get; set; }
    }
}
