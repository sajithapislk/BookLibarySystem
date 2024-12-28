using System;
using System.Data.Entity;
using BookLibarySystem.Models;
using BookLibarySystem.Models.Enums;
using Microsoft.AspNet.Identity;

namespace BookLibarySystem.Data
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

            // Add initial Authors
            var author1 = new Author
            {
                FirstName = "Jane",
                LastName = "Austen",
                Image = "jane_austen.jpg"
            };

            var author2 = new Author
            {
                FirstName = "Mark",
                LastName = "Twain",
                Image = "mark_twain.jpg"
            };

            var author3 = new Author
            {
                FirstName = "Leo",
                LastName = "Tolstoy",
                Image = "leo_tolstoy.jpg"
            };

            context.Authors.Add(author1);
            context.Authors.Add(author2);
            context.Authors.Add(author3);

            // Add initial Books
            var book1 = new Book
            {
                Title = "Pride and Prejudice",
                Author = author1,
                ISBN = "978-0141040349",
                Price = 9.99m,
                Description = "A romantic novel of manners.",
                Image1 = "pride_and_prejudice1.jpg",
                Image2 = "pride_and_prejudice2.jpg",
                CreatedAt = DateTime.Now,
                Category = Category.Romance
            };

            var book2 = new Book
            {
                Title = "Adventures of Huckleberry Finn",
                Author = author2,
                ISBN = "978-0486280615",
                Price = 8.99m,
                Description = "A young boy's adventures on the Mississippi River.",
                Image1 = "huck_finn1.jpg",
                Image2 = "huck_finn2.jpg",
                CreatedAt = DateTime.Now,
                Category = Category.Fiction
            };

            var book3 = new Book
            {
                Title = "Scinece",
                Author = author3,
                ISBN = "978-0307388875",
                Price = 12.99m,
                Description = "A historical novel that chronicles the French invasion of Russia.",
                Image1 = "war_and_peace1.jpg",
                Image2 = "war_and_peace2.jpg",
                CreatedAt = DateTime.Now,
                Category = Category.Fiction
            };

            var book4 = new Book
            {
                Title = "Emma",
                Author = author1,
                ISBN = "978-0141439587",
                Price = 7.99m,
                Description = "A novel about youthful hubris and romantic misunderstandings.",
                Image1 = "emma1.jpg",
                Image2 = "emma2.jpg",
                CreatedAt = DateTime.Now,
                Category = Category.Romance
            };

            var book5 = new Book
            {
                Title = "The Adventures of Tom Sawyer",
                Author = author2,
                ISBN = "978-0486400779",
                Price = 8.49m,
                Description = "A book about the adventures of a young boy growing up along the Mississippi River.",
                Image1 = "tom_sawyer1.jpg",
                Image2 = "tom_sawyer2.jpg",
                CreatedAt = DateTime.Now,
                Category = Category.Fiction
            };

            var book6 = new Book
            {
                Title = "Anna Karenina",
                Author = author3,
                ISBN = "978-0143035008",
                Price = 10.99m,
                Description = "A novel about love and infidelity in Russian high society.",
                Image1 = "anna_karenina1.jpg",
                Image2 = "anna_karenina2.jpg",
                CreatedAt = DateTime.Now,
                Category = Category.Fiction
            };

            context.Books.Add(book1);
            context.Books.Add(book2);
            context.Books.Add(book3);
            context.Books.Add(book4);
            context.Books.Add(book5);
            context.Books.Add(book6);

            // Save changes
            context.SaveChanges();

            base.Seed(context);
        }
    }
}
