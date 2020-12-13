using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookStore.Models;
using BookStore.Utils;
using BookStore.Context;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using BookStore.Utils.Payment;

namespace BookStore.Controllers
{
    public class CartController : Controller
    {
        protected BookStoreContext dbContext = BookStoreContext.GetInstance();
        // GET: Cart
        public ActionResult Index()
        {
            if(SessionHelper.IsUserLoggedIn(Session))
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "User");
            }
        }

        public async Task<ActionResult> AddToCart(long? bookId)
        {
            if (SessionHelper.IsUserLoggedIn(Session))
            {
                if (bookId != null)
                {
                    var book = await dbContext.Books.FindAsync(bookId);
                    SessionHelper.GetCart(Session).AddItem(book, 1);
                }
                return RedirectToAction("Index", "Main");
            }
            else
            {
                return RedirectToAction("Login", "User");
            }
        }

        public ActionResult ClearCart()
        {
            if (SessionHelper.IsUserLoggedIn(Session))
            {
                SessionHelper.Set(Session, SessionKey.CART, null);
                return RedirectToAction("Index", "Main");
            }
            else
            {
                return RedirectToAction("Login", "User");
            }
        }

        public async Task<ActionResult> DeleteFromCart(long? bookId)
        {
            if (SessionHelper.IsUserLoggedIn(Session))
            {
                var book = await dbContext.Books.FindAsync(bookId);
                SessionHelper.GetCart(Session).RemoveLine(book);
                return RedirectToAction("Index", "Cart");
            }
            return RedirectToAction("Login", "User");
        }

        public ActionResult ConfirmPayment()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ConfirmPayment([Bind(Include = "Receiver,Name,Address")] EmailContext emailContext)
        {
            if (SessionHelper.IsUserLoggedIn(Session))
            {
                if(!ModelState.IsValid)
                {
                    return View(emailContext);
                }
                try
                {
                    emailContext.Cart = SessionHelper.GetCart(Session);
                    ViewBag.MessageResult = new EmailPaymentSender().TrySendReceipt(emailContext, ModelState.IsValid);
                    return RedirectToAction("ClearCart");
                } catch(Exception ex)
                {
                    Console.WriteLine(ex.StackTrace);
                    return RedirectToAction("", "Error");
                }
            }
            else
            {
                return RedirectToAction("Login", "User");
            }
        }
    }
}