using BookLibarySystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookLibarySystem.Controllers
{
    public class OrderController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Order
        public ActionResult OrderInfoReport(int orderId)
        {
            var order = db.Orders.Include("OrderInfo.Book").SingleOrDefault(o => o.OrderId == orderId) ;
            if (order == null) { return HttpNotFound(); }
            return Json(order, JsonRequestBehavior.AllowGet);
            //return new Rotativa.ViewAsPdf("BookReport", books)
            //{
            //    FileName = "BookReport.pdf"
            //};
        }
    }
}