using System;

namespace EBookStore.Models
{
    public class OrderInfo
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int BookId { get; set; }
        public int Qty { get; set; }

        // Navigation properties
        public virtual Book Book { get; set; }
    }
}