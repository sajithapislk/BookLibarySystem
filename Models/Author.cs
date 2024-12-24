using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EBookStore.Models
{
    public class Author
    {
        public int AuthorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Image { get; set; }
    }
}