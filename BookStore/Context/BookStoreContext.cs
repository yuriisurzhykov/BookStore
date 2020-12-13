using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using BookStore.Models;

namespace BookStore.Context
{
    public class BookStoreContext: DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<User> Users { get; set; }

        private BookStoreContext(): base("BookStoreContext")
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<BookStoreContext>());
        }

        public static BookStoreContext GetInstance()
        {
            return new BookStoreContext();
        }
    }
}