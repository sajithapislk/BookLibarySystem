using System.Data.Entity;
using EBookStore.Models;
using Microsoft.AspNet.Identity;

namespace EBookStore.Data
{
    public class DatabaseSeeder : DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            var passwordHasher = new PasswordHasher();
            string _password = passwordHasher.HashPassword("12345678");

            // Add initial Customers
            context.Customers.Add(new Customer
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "customer@test.com",
                Password = _password
            });

            // Add initial Admin
            context.Admins.Add(new Admin
            {
                Email = "admin@test.com",
                Password = _password
            });

            // Save changes
            context.SaveChanges();

            base.Seed(context);
        }
    }
}
