using System;
using System.Collections.Generic;

namespace BookLibarySystem.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public string Address { get; set; }
        public string Status { get; set; }
        public DateTime Created_At { get; set; }

        // Navigation properties
        public virtual Customer Customer { get; set; }
        public virtual ICollection<OrderInfo> OrderInfo { get; set; }
    }
}