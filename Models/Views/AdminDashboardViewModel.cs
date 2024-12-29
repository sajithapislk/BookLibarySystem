using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookLibarySystem.Models.Views
{
    public class AdminDashboardViewModel
    {
        public decimal TotalEarnings { get; set; }
        public int TodayOrders { get; set; }
        public int Customers { get; set; }
        public int FeedbackCount { get; set; }
        public List<Order> Orders { get; set; }
    }

}