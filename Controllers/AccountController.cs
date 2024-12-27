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

            var user = db.Customers.SingleOrDefault(c => c.Email == model.Email);
            if (user == null)
            {
                // Check if it's an admin
                var admin = db.Admins.SingleOrDefault(a => a.Email == model.Email);
                if (admin != null)
                {
                    var passwordHasher = new PasswordHasher();
                    var result = passwordHasher.VerifyHashedPassword(admin.Password, model.Password);
                    if (result == PasswordVerificationResult.Success)
                    {
                        FormsAuthentication.SetAuthCookie(model.Email, false);
                        Session["UserId"] = admin.AdminId;
                        Session["UserRole"] = "Admin";
                        if (!Roles.RoleExists("Admin"))
                        {
                            Roles.CreateRole("Admin");
                        }
                        if (!Roles.IsUserInRole(model.Email, "Admin"))
                        {
                            Roles.AddUserToRole(model.Email, "Admin");
                        }
                        return RedirectToLocal(returnUrl);
                    }
                }
            }
            else
            {
                var passwordHasher = new PasswordHasher();
                var result = passwordHasher.VerifyHashedPassword(user.Password, model.Password);
                if (result == PasswordVerificationResult.Success)
                {
                    FormsAuthentication.SetAuthCookie(model.Email, false);
                    Session["UserId"] = user.CustomerId;
                    Session["UserRole"] = "Customer";
                    if (!Roles.RoleExists("Customer"))
                    {
                        Roles.CreateRole("Customer");
                    }
                    if (!Roles.IsUserInRole(model.Email, "Customer"))
                    {
                        Roles.AddUserToRole(model.Email, "Customer");
                    }
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Customer customer)
        {
            if (ModelState.IsValid)
            {
                var passwordHasher = new PasswordHasher();
                customer.Password = passwordHasher.HashPassword(customer.Password);

                db.Customers.Add(customer);
                db.SaveChanges();

                return View("Login");
            }

            return View("Login");
        }
    }
}