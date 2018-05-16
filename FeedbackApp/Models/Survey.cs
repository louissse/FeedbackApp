using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FeedbackApp.Models
{
    public class Survey
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Du skal give din survey en titel")]
        [StringLength(50, MinimumLength = 3)]
        public string Title { get; set; }
        [Required(ErrorMessage = "Du skal give din survey en beskrivelse")]
        [StringLength(500, MinimumLength = 3)]
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
