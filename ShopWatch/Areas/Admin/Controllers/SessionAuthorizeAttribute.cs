using ShopWatch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopWatch.Areas.Admin.Controllers
{
    public class SessionAuthorizeAttribute : AuthorizeAttribute
    {
        protected ShopEntities db = new ShopEntities();
        //public ShopEntities db = new ShopEntities();
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            bool check = true;
            if (System.Web.HttpContext.Current.Session["User"] == null)
                check = false;
            else if (db.Users.Find(((User)System.Web.HttpContext.Current.Session["User"]).UserID).Rol == 1)

            {
                check = false;
                System.Web.HttpContext.Current.Session["Message"] = "Please login with admin account";
            }
            return check;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectResult("/Admin/Authentication/Login");
        }
    }
    public class AdminSessionAuthorizeAttribute : AuthorizeAttribute
    {
        protected ShopEntities db = new ShopEntities();
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            bool check = true;
            if (System.Web.HttpContext.Current.Session["User"] == null)
                check = false;
            else if (db.Users.Find(((User)System.Web.HttpContext.Current.Session["User"]).UserID).Rol == 1 ||
                db.Users.Find(((User)System.Web.HttpContext.Current.Session["User"]).UserID).Rol ==2)
                check = false;
            return check;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectResult("/Admin/Dashboard");
        }
    }
}