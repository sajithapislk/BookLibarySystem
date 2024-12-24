using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookLibarySystem.Models.Views
{
    public class BooksViewModel {
        public IEnumerable<Book> AllBooks { get; set; }
        public IEnumerable<Book> NewBooks { get; set; }
    }
}