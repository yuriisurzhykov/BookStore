using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using BookStore.Context;
using BookStore.Models;
using BookStore.Utils;
using PagedList;

namespace BookStore.Controllers
{
    public class AdminController : Controller
    {
        protected BookStoreContext dbContext = BookStoreContext.GetInstance();

        private static int PAGE_SIZE = 6;
        public ActionResult Index(int? page)
        {
            if (SessionHelper.IsUserLoggedIn(Session) && SessionHelper.GetUser(Session).UserType == UserType.ADMIN)
            {
                var viewModel = new AdminViewModel();
                viewModel.Books = dbContext.Books.ToList().ToPagedList((page == null || page < 1) ? 1 : (int)page, PAGE_SIZE);
                if (viewModel.Books == null)
                {
                    viewModel.Books = new List<Book>().ToPagedList((page == null || page < 1) ? 1 : (int)page, PAGE_SIZE);
                }
                return View(viewModel);
            }
            else
            {
                return RedirectToAction("Login", "User");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index(int? page, string context)
        {
            if (SessionHelper.IsUserLoggedIn(Session) && SessionHelper.GetUser(Session).UserType == UserType.ADMIN)
            {
                var viewModel = new AdminViewModel();
                viewModel.Books = dbContext.Books.ToList().ToPagedList((page == null || page < 1) ? 1 : (int)page, PAGE_SIZE);
                if (viewModel.Books == null)
                {
                    viewModel.Books = new List<Book>().ToPagedList((page == null || page < 1) ? 1 : (int)page, PAGE_SIZE);
                }
                if (context != null)
                {
                    viewModel.Books = viewModel.Books
                        .ToList()
                        .Where( bk => 
                            bk.Name.Contains(context) ||
                            bk.Description.Contains(context) ||
                            bk.Author.Contains(context)
                        )
                        .ToList()
                        .ToPagedList(page ?? 1, PAGE_SIZE);
                }
                return View(viewModel);
            }
            else
            {
                return RedirectToAction("Login", "User");
            }
        }

        public ActionResult CreateBook()
        {
            if (SessionHelper.IsUserLoggedInAndAdmin(Session))
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "User");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateBook([Bind(Include = "Id,Name,Author,Description,Cost")] Book book)
        {
            if (SessionHelper.IsUserLoggedInAndAdmin(Session))
            {
                if (ModelState.IsValid)
                {
                    dbContext.Books.Add(book);
                    await dbContext.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                return View(book);
            }
            else
            {
                return RedirectToAction("Login", "User");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditBook([Bind(Include = "Id,Name,Description,Author,Cost")] Book book)
        {
            if (SessionHelper.IsUserLoggedInAndAdmin(Session))
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "User");
            }
        }

        public async Task<ActionResult> DeleteBook(long? bookId)
        {
            if (SessionHelper.IsUserLoggedInAndAdmin(Session))
            {
                var book = await dbContext.Books.FindAsync(bookId);
                if (book == null)
                {
                    return RedirectToAction("NoSuchBook", "Error", new { id = bookId });
                }
                return View(book);
            }
            return RedirectToAction("Login", "User");
        }

        [HttpPost, ActionName("DeleteBook")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long? id)
        {
            if (SessionHelper.IsUserLoggedInAndAdmin(Session))
            {
                var book = await dbContext.Books.FindAsync(id);
                dbContext.Books.Remove(book);
                await dbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Login", "User");
        }
    }
}