using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BookLibarySystem.Models;

namespace BookLibarySystem.Controllers.Admin
{
    [Authorize(Roles = "Admin")]
    public class BooksController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Books
        public ActionResult Index()
        {
            var books = db.Books.Include(b => b.Author);
            return View("~/Views/Admin/Books/Index.cshtml",books.ToList());
        }

        // GET: Books/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View("~/Views/Admin/Books/Details.cshtml", book);
        }

        // GET: Books/Create
        public ActionResult Create()
        {
            ViewBag.Authors = db.Authors.ToList();
            return View("~/Views/Admin/Books/Create.cshtml");
        }

        // POST: Books/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BookId,AuthorId,Title,ISBN,Price,Description,CreatedAt,Category")] Book book, HttpPostedFileBase Image1, HttpPostedFileBase Image2)
        {
            if (ModelState.IsValid)
            {
                // Process Image1
                if (Image1 != null && Image1.ContentLength > 0)
                {
                    var fileName1 = DateTime.Now.ToString("yyyyMMddHHmmss") + "_1" + Path.GetExtension(Image1.FileName);
                    var directoryPath1 = Server.MapPath("~/Content/images/books/");
                    var filePath1 = Path.Combine(directoryPath1, fileName1);

                    if (!Directory.Exists(directoryPath1))
                    {
                        Directory.CreateDirectory(directoryPath1);
                    }

                    Image1.SaveAs(filePath1);
                    book.Image1 = fileName1; // Store only the file name
                }

                // Process Image2
                if (Image2 != null && Image2.ContentLength > 0)
                {
                    var fileName2 = DateTime.Now.ToString("yyyyMMddHHmmss") + "_2" + Path.GetExtension(Image2.FileName);
                    var directoryPath2 = Server.MapPath("~/Content/images/books/");
                    var filePath2 = Path.Combine(directoryPath2, fileName2);

                    if (!Directory.Exists(directoryPath2))
                    {
                        Directory.CreateDirectory(directoryPath2);
                    }

                    Image2.SaveAs(filePath2);
                    book.Image2 = fileName2; // Store only the file name
                }

                db.Books.Add(book);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AuthorId = new SelectList(db.Authors, "AuthorId", "FirstName", book.AuthorId);
            return View("~/Views/Admin/Books/Create.cshtml", book);
        }



        // GET: Books/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            ViewBag.AuthorId = new SelectList(db.Authors, "AuthorId", "FirstName", book.AuthorId);
            return View("~/Views/Admin/Books/Edit.cshtml", book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BookId,AuthorId,Title,Image1,Image2,ISBN,Price,Description,CreatedAt,Category")] Book book)
        {
            if (ModelState.IsValid)
            {
                db.Entry(book).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AuthorId = new SelectList(db.Authors, "AuthorId", "FirstName", book.AuthorId);
            return View("~/Views/Admin/Books/Edit.cshtml", book);
        }

        // GET: Books/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View("~/Views/Admin/Books/Delete.cshtml", book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Book book = db.Books.Find(id);
            db.Books.Remove(book);
            db.SaveChanges();
            return RedirectToAction("~/Views/Admin/Books/Index.cshtml");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult Report()
        {
            var books = db.Books.Include(b => b.Author);
            return new Rotativa.ViewAsPdf("~/Views/Admin/Books/Report.cshtml", books.ToList())
            {
                FileName = "All Books.pdf"
            };
        }
    }
}
