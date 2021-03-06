﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FeedbackApp.Models
{
    public class Survey
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<Question> Questions { get; set; }
        public List<Feedback> Feedback { get; set; }

        public Survey()
        {
            Id = Guid.NewGuid();
            Questions = new List<Question>();
            Feedback = new List<Feedback>();
        }
    }
}
