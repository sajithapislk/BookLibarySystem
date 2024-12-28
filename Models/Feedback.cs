using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookLibarySystem.Models
{
    public class Feedback
    {
        public int Id { get; set; }
        public int OrderInfoId { get; set; }
        
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; }

        // Navigation property
        public virtual OrderInfo OrderInfo { get; set; }
    }
}