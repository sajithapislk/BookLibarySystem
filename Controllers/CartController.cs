using BookLibarySystem.Models;
using BookLibarySystem.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookLibarySystem.Controllers
{
    public class CartController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private CartService cartService = new CartService();
        // GET: Cart
        public ActionResult Index()
        {
            var cartItems = cartService.GetCartItems();
            //return Json(cartItems, JsonRequestBehavior.AllowGet);
            return View(cartItems);
        }


        public ActionResult AddToCart(int id)
        {
            var book = db.Books.Find(id); // Fetch the book from the database

            if (book != null)
            {
                cartService.AddToCart(book); // Pass the book object to the service
                return RedirectToAction("Index"); // Redirect to the Cart view after adding
            }

            // Optionally handle the case where the book is not found
            return HttpNotFound(); // Or redirect to an error page
        }
        [HttpPost]
        public ActionResult UpdateCart(int bookId, int quantity)
        {
            cartService.UpdateCart(bookId, quantity);
            return Json(new{status = "success"});
        }
        [HttpPost]
        public ActionResult RemoveCart(int bookId)
        {
            cartService.RemoveCart(bookId);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult Checkout(string address)
        {
            if (!User.Identity.IsAuthenticated)
            {
                TempData["LoginRedirectMessage"] = "Please log in to place an order.";
                return RedirectToAction("Login", "Account");
            }
            int userId = (int)Session["UserId"];
            //return Json(new { userId = userId });
            var cartItems = cartService.GetCartItems();
            if (cartItems == null || !cartItems.Any())
            {
                TempData["ErrorMessage"] = "Cart is empty.";
                return RedirectToAction("Index", "Cart"); // Assuming there's a Cart index view
            }

            var order = new Order
            {
                CustomerId = userId,
                Address = address,
                Status = "Pending",
                Created_At = DateTime.Now,
                OrderInfo = new List<OrderInfo>()
            };

            foreach (var cartItem in cartItems)
            {
                var orderInfo = new OrderInfo
                {
                    BookId = cartItem.BookId,
                    Qty = cartItem.Quantity
                };

                order.OrderInfo.Add(orderInfo);
            }

            db.Orders.Add(order);
            db.SaveChanges();

            cartService.ClearCart(); // Clear cart after checkout
            return View("CheckoutConfirmation");
        }

        public ActionResult Checkout()
        {
            var cartItems = cartService.GetCartItems();

            // Calculate the total price
            decimal total = cartItems.Sum(item => item.Book.Price * item.Quantity);

            ViewBag.Total = total;
            return View(cartItems);
        }


    }
}