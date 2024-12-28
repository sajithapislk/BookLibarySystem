using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookLibarySystem.Models.Views
{
    public class BookDetailsViewModel
    {
        public Book Book { get; set; }
        public IEnumerable<Feedback> Feedbacks { get; set; }
    }
}