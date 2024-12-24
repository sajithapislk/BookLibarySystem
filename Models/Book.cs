using BookLibarySystem.Models.Enums;
using System;

namespace BookLibarySystem.Models
{
    public class Book
    {
        public int BookId { get; set; }
        public int AuthorId { get; set; }
        public string Title { get; set; }
        public string Image1 { get; set; }
        public string Image2 { get; set; }
        public string ISBN { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public Category Category { get; set; }
        // Navigation properties
        public virtual Author Author { get; set; }
    }
}