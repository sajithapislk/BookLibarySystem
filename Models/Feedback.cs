using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookLibarySystem.Models
{
    public class Feedback
    {
        public int id { get; set; }
        public int BookInfoId { get; set; }
        
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; }

        // Navigation property
        public virtual OrderInfo OrderInfo { get; set; }
    }
}