using BookLibarySystem.Models;
using BookLibarySystem.Models.Views;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookLibarySystem.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        [AllowAnonymous]
        public ActionResult Index()
        {
            var allBooks = db.Books;
            var newBooks = allBooks.Where(b => DbFunctions.DiffDays(b.CreatedAt, DateTime.Now) <= 30).Take(4);
            var viewModel = new BooksViewModel { AllBooks = allBooks, NewBooks = newBooks };
            return View(viewModel);
        }
        [AllowAnonymous]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        [AllowAnonymous]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}