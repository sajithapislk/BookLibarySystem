using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookLibarySystem.Controllers.Admin
{
    [Authorize(Roles = "Admin")]
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View("~/Views/Admin/Home/Index.cshtml");
        }
    }
}