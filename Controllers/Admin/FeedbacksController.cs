using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BookLibarySystem.Models;

namespace BookLibarySystem.Controllers.Admin
{
    [Authorize(Roles = "Admin")]
    public class FeedbacksController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Feedbacks
        public ActionResult Index()
        {
            return View("~/Views/Admin/Feedbacks/Index.cshtml",db.Feedbacks.ToList());
        }

        // GET: Feedbacks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Feedback feedback = db.Feedbacks.Find(id);
            if (feedback == null)
            {
                return HttpNotFound();
            }
            return View("~/Views/Admin/Feedbacks/Details.cshtml", feedback);
        }
    }
}
