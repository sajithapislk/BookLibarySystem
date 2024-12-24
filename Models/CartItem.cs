using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EBookStore.Models
{
    public class CartItem
    {
        public int CartItemId { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
        public int Quantity { get; set; }
    }
}