using System.Data.Entity;

namespace BookLibarySystem.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() : base("DefaultConnection")
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderInfo> OrderInfos { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<OrderInfo>()
            .HasRequired(oi => oi.Book)
            .WithMany(b => b.OrderInfos)
            .HasForeignKey(oi => oi.BookId)
            .WillCascadeOnDelete(false);

            modelBuilder.Entity<OrderInfo>()
                .HasRequired(oi => oi.Order)
                .WithMany(o => o.OrderInfo)
                .HasForeignKey(oi => oi.OrderId)
                .WillCascadeOnDelete(false);
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

    }
}