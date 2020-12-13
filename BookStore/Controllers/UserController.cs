using System.Linq;
using BookStore.Context;
using System.Web.Mvc;
using System.Threading.Tasks;
using BookStore.Utils;
using BookStore.Models;


namespace BookStore.Controllers
{
    public class UserController : Controller
    {
        protected BookStoreContext dbContext = BookStoreContext.GetInstance();
        public ActionResult Login()
        {
            /*var user = new User
            {
                Login = "admin",
                Password = "admin12345654321",
                UserType = UserType.ADMIN
            };
            dbContext.Users.Add(user);
            dbContext.SaveChanges();*/
            if (SessionHelper.IsUserLoggedIn(Session))
            {
                return RedirectToAction("Index", "Main");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login([Bind(Include = "Id,Login,Password")] User user)
        {
            if (ModelState.IsValid)
            {
                var fromDb = dbContext.Users.Where(usr => usr.Login == user.Login && usr.Password == user.Password).FirstOrDefault();
                if (fromDb != null)
                {
                    SessionHelper.Set(Session, SessionKey.USER, fromDb);
                    return RedirectToAction("Index", "Main");
                }
                else
                {
                    ViewBag.ErrorMessage = "Incorrect login or password!";
                    return View(user);
                }
            }
            return View(user);
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register([Bind(Include = "Id,Login,Password")] User user)
        {
            if(SessionHelper.IsUserLoggedIn(Session))
            {
                return RedirectToAction("Index", "Main");
            }
            if (ModelState.IsValid)
            {
                var userDb = dbContext.Users.Where(usr => usr.Login == user.Login).FirstOrDefault();
                if(userDb == null)
                {
                    userDb = dbContext.Users.Add(user);
                    await dbContext.SaveChangesAsync();
                    SessionHelper.Set(Session, SessionKey.USER, userDb);
                    return RedirectToAction("Index", "Main");
                }
                else
                {
                    ViewBag.ErrorMessage = "User with such login already exists!";
                    return View(user);
                }
            }
            return View(user);
        }

        public ActionResult Logout()
        {
            SessionHelper.Set(Session, SessionKey.USER, null);
            return RedirectToAction("Login");
        }
    }
}