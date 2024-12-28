using BookLibarySystem.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookLibarySystem.Services
{
    public class CartService
    {
        private const string CartSessionKey = "CartSession";

        public List<CartItem> GetCartItems()
        {
            var cart = (List<CartItem>)HttpContext.Current.Session[CartSessionKey];
            return cart ?? new List<CartItem>();
        }

        public void AddToCart(Book book)
        {
            var cart = GetCartItems();
            var cartItem = cart.FirstOrDefault(c => c.BookId == book.BookId);
            if (cartItem == null)
            {
                cart.Add(new CartItem { BookId = book.BookId, Book = book, Quantity = 1 });
            }
            else
            {
                cartItem.Quantity++;
            }
            HttpContext.Current.Session[CartSessionKey] = cart;
        }

        public void UpdateCart(int bookId, int quantity)
        {
            var cart = GetCartItems();
            var cartItem = cart.FirstOrDefault(c => c.BookId == bookId);
            if (cartItem != null)
            {
                if (quantity <= 0)
                {
                    cart.Remove(cartItem);
                }
                else
                {
                    cartItem.Quantity = quantity;
                }
            }
            HttpContext.Current.Session[CartSessionKey] = cart;
        }
        public void RemoveCart(int bookId)
        {
            var cart = GetCartItems();
            var cartItem = cart.FirstOrDefault(c => c.BookId == bookId);
            if (cartItem != null)
            {
                cart.Remove(cartItem);
            }
            HttpContext.Current.Session[CartSessionKey] = cart;
        }

        public void ClearCart()
        {
            HttpContext.Current.Session[CartSessionKey] = null;
        }
    }
}