using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookStore.Models
{
    public class BooksViewModel
    {
        public int AmountInRow { get; set; }
        public List<Book> Books { get; set; }
    }
}