using FeedbackApp.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeedbackApp.Components
{
    [ViewComponent(Name = "Questionnaire")]
    public class SurveyViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(Survey questionnaire)
        {
            return View(questionnaire);
        }
    }
}
