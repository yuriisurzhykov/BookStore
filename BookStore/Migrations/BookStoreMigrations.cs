using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace BookStore.Migrations
{
    public class BookStoreContext: DbMigrationsConfiguration<Context.BookStoreContext>
    {
        public BookStoreContext()
        {
            AutomaticMigrationsEnabled = true;
        }
    }
}