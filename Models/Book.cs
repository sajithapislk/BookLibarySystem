using System;

namespace EBookStore.Models
{
    public class Book
    {
        public int BookId { get; set; }
        public int AuthorId { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public string ISBN { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }

        // Navigation properties
        public virtual Author Author { get; set; }
    }
}