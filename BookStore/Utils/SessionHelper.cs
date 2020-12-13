using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using BookStore.Models;

namespace BookStore.Utils
{
    public enum SessionKey
    {
        CART,
        RETURN_URL,
        USER
    }

    public static class SessionHelper
    {
        public static void Set(HttpSessionStateBase session, SessionKey key, object value)
        {
            session[Enum.GetName(typeof(SessionKey), key)] = value;
        }

        public static T Get<T>(HttpSessionStateBase session, SessionKey key)
        {
            object dataValue = session[Enum.GetName(typeof(SessionKey), key)];
            if (dataValue != null && dataValue is T)
            {
                return (T)dataValue;
            }
            else
            {
                return default(T);
            }
        }

        public static Cart GetCart(HttpSessionStateBase session)
        {
            Cart myCart = Get<Cart>(session, SessionKey.CART);
            if (myCart == null)
            {
                myCart = new Cart();
                Set(session, SessionKey.CART, myCart);
            }
            return myCart;
        }
        public static User GetUser(HttpSessionStateBase session)
        {
            User curUser = Get<User>(session, SessionKey.USER);
            return curUser;
        }

        public static bool IsUserLoggedIn(HttpSessionStateBase session)
        {
            User curUser = Get<User>(session, SessionKey.USER);
            return curUser != null;
        }

        public static bool IsUserLoggedInAndAdmin(HttpSessionStateBase session)
        {
            User curUser = Get<User>(session, SessionKey.USER);
            return curUser != null && curUser.UserType == UserType.ADMIN;
        } 
    }
}