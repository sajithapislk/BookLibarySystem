using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BookLibarySystem.Models;

namespace BookLibarySystem.Controllers
{
    public class FeedbacksController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [HttpPost]
        public ActionResult Create(int Id, string FeedbackText)
        {
            var orderInfo = db.OrderInfos.Find(Id);
            if (orderInfo != null)
            {
                var feedback = new Feedback
                {
                    OrderInfoId = Id,
                    Text = FeedbackText,
                    CreatedAt = DateTime.Now
                };
                db.Feedbacks.Add(feedback);
                db.SaveChanges();
            }
            return RedirectToAction("Index", "Order");
        }

    }
}
