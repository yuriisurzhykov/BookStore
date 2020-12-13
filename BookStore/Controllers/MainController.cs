using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookStore.Context;
using BookStore.Utils;

namespace BookStore.Controllers
{
    public class MainController : Controller
    {
        protected BookStoreContext dbContext = BookStoreContext.GetInstance();
        public ActionResult Index()
        {
            var user = SessionHelper.GetUser(Session);
            if (user != null)
            {
                var books = dbContext.Books.ToList();
                var viewModel = new Models.BooksViewModel();
                viewModel.Books = books;
                viewModel.AmountInRow = 2;
                return View(viewModel);
            }
            return RedirectToAction("Login", "User");
        }

        public ActionResult About()
        {
            if (SessionHelper.IsUserLoggedInAndAdmin(Session))
            {

                ViewBag.Message = "This website was created as course project for WEB-technologies discipline.";

                return View();
            }
            else
            {
                return RedirectToAction("Login", "User");
            }
        }

        public ActionResult Contact()
        {
            if (SessionHelper.IsUserLoggedInAndAdmin(Session))
            {

                ViewBag.Message = "Contact page.";

                return View();
            }
            else
            {
                return RedirectToAction("Login", "User");
            }
        }
    }
}