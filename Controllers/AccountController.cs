using BookLibarySystem.Models;
using BookLibarySystem.Models.Views;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace BookLibarySystem.Controllers
{
    public class AccountController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext(); // GET: Account/Login 
        public ActionResult Login(string returnUrl) {
            ViewBag.ReturnUrl = returnUrl;
            ViewBag.LoginRedirectMessage = TempData["LoginRedirectMessage"];
            return View();
        }
        // POST: Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var customer = db.Customers.SingleOrDefault(c => c.Email == model.Email);
            if (customer != null)
            {
                var passwordHasher = new PasswordHasher();
                var result = passwordHasher.VerifyHashedPassword(customer.Password, model.Password);
                if (result == PasswordVerificationResult.Success)
                {
                    // Set authentication cookie
                    FormsAuthentication.SetAuthCookie(model.Email, false);

                    // Store user ID in session
                    Session["UserId"] = customer.CustomerId;

                    return RedirectToLocal(returnUrl);
                }
            }

            ModelState.AddModelError("", "Invalid login attempt.");
            return View(model);
        }


        // POST: Account/LogOff
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult TestAuth()
        {
            if (User.Identity.IsAuthenticated)
            {
                var userId = User.Identity.GetUserId();
                return Content($"User is authenticated. User ID: {userId}");
            }
            else
            {
                return Content("User is not authenticated.");
            }
        }


    }
}