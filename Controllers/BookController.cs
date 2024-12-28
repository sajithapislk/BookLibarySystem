using BookLibarySystem.Models;
using BookLibarySystem.Models.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookLibarySystem.Controllers
{
    public class BookController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Show(int id)
        {
            var book = db.Books.Find(id);

            if (book == null)
            {
                return HttpNotFound();
            }

            // Explicitly load OrderInfos and Feedbacks
            db.Entry(book).Collection(b => b.OrderInfos).Load();
            foreach (var orderInfo in book.OrderInfos)
            {
                db.Entry(orderInfo).Collection(oi => oi.Feedbacks).Load();
            }

            var feedbacks = book.OrderInfos.SelectMany(oi => oi.Feedbacks).ToList();

            var viewModel = new BookDetailsViewModel
            {
                Book = book,
                Feedbacks = feedbacks
            };

            return View(viewModel);
        }



        public ActionResult Search(string query)
        {
            var books = string.IsNullOrEmpty(query)
                ? db.Books.ToList()
                : db.Books.Where(b => b.Title.Contains(query) || b.Author.FirstName.Contains(query)).ToList();

            return View(books);
        }
    }
}