using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookStore.Models
{
    public class AdminViewModel
    {
        public IPagedList<Book> Books { get; set; }
    }
}