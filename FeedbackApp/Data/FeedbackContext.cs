using FeedbackApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeedbackApp.Data
{
    public class FeedbackContext : DbContext
    {
        public FeedbackContext(DbContextOptions<FeedbackContext> options) : base(options)
        { }

        public DbSet<Survey> survey { get; set; }
        public DbSet<Question> question { get; set; }
        public DbSet<Feedback> feedback { get; set; }
        public DbSet<Condition> condition { get; set; }
    }
}
