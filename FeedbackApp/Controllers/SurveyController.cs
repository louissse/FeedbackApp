using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FeedbackApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace FeedbackApp.Controllers
{
    public class SurveyController : Controller
    {
        public IActionResult Index()
        {
            //Testing a question and answer
            var question1 = new Question();
            question1.Text = "Hvor glad er du for katte?";
            question1.Id = 1;
            var question2 = new Question();
            question2.Text = "Hvor godt kan du lide mad?";
            question2.Id = 2;

            var questionnaireModel = new Survey();
            questionnaireModel.Questions.Add(question1);
            questionnaireModel.Questions.Add(question2);

            return View(questionnaireModel);
        }

        //Testing question answer
        [HttpPost]
        public IActionResult Index(Survey questionnaireModel)
        {
            return View(questionnaireModel);
        }
    }
}