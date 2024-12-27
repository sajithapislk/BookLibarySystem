using BookLibarySystem.Models;
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
        // GET: Book
        public ActionResult Show(int id)
        {
            var book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
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