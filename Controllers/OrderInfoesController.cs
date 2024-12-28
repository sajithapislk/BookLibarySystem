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
    public class OrderInfoesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        // GET: OrderInfoes/Details/5
        public ActionResult Index(int orderId)
        {
            var orderInfos = db.OrderInfos.Where(o => o.OrderId == orderId).ToList();

            return View(orderInfos); // Example: passing the list to a view
        }

    }
}
