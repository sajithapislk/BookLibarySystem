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
    public class AuthorsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Authors
        public ActionResult Index()
        {
            return View("~/Views/Admin/Authors/Index.cshtml",db.Authors.ToList());
        }

        // GET: Authors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Author author = db.Authors.Find(id);
            if (author == null)
            {
                return HttpNotFound();
            }
            return View("~/Views/Admin/Authors/Details.cshtml", author);
        }

        // GET: Authors/Create
        public ActionResult Create()
        {
            return View("~/Views/Admin/Authors/Create.cshtml");
        }

        // POST: Authors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AuthorId,FirstName,LastName,ImageFile")] Author author, HttpPostedFileBase ImageFile)
        {
            if (ModelState.IsValid)
            {
                if (ImageFile != null && ImageFile.ContentLength > 0)
                {
                    // Generate a short, unique file name using timestamp
                    var fileName = DateTime.Now.ToString("yyyyMMddHHmmss") + Path.GetExtension(ImageFile.FileName);
                    var directoryPath = Server.MapPath("~/Content/images/authors/");
                    var filePath = Path.Combine(directoryPath, fileName);

                    // Ensure the directory exists
                    if (!Directory.Exists(directoryPath))
                    {
                        Directory.CreateDirectory(directoryPath);
                    }

                    ImageFile.SaveAs(filePath);
                    author.Image = fileName; // Store only the file name
                }

                db.Authors.Add(author);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View("~/Views/Admin/Authors/Create.cshtml", author);
        }


        // GET: Authors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Author author = db.Authors.Find(id);
            if (author == null)
            {
                return HttpNotFound();
            }
            return View("~/Views/Admin/Authors/Edit.cshtml", author);
        }

        // POST: Authors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AuthorId,FirstName,LastName,Image")] Author author)
        {
            if (ModelState.IsValid)
            {
                db.Entry(author).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("~/Views/Admin/Authors/Index.cshtml");
            }
            return View(author);
        }

        // GET: Authors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Author author = db.Authors.Find(id);
            if (author == null)
            {
                return HttpNotFound();
            }
            return View("~/Views/Admin/Authors/Delete.cshtml", author);
        }

        // POST: Authors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Author author = db.Authors.Find(id);
            db.Authors.Remove(author);
            db.SaveChanges();
            return RedirectToAction("~/Views/Admin/Authors/Index.cshtml");
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
            var authors = db.Authors;
            return new Rotativa.ViewAsPdf("~/Views/Admin/Authors/Report.cshtml", authors.ToList())
            {
                FileName = "All Authors.pdf"
            };
        }
    }
}
