using ShopWatch.Controllers;
using ShopWatch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopWatch.Areas.Admin.Controllers
{
    public class Home_AdminController : BaseController
    {
        // GET: Admin/Home
        public ActionResult Index()
        {
            var proCount = db.Products.Count();
            ViewBag.Pro = proCount;
            var userCount = db.Users.Count();
            ViewBag.user = userCount;
            var cateCount = db.Categories.Count();
            ViewBag.cate = cateCount;
            var orderCount = db.Orders.Count();
            ViewBag.order = orderCount;
            return View();
        }
        public ActionResult EditProfile()
        {
            var userAvai = Session["User"];
            var Dob= ((User)Session["User"]).Dob.ToString();
            
            ViewBag.birth = changeDate(Dob);

            ViewBag.user = userAvai;
            return View();
        }
        public static string changeDate(string str)
        {
            var sp = str.Split(' ');
            var e = sp[0].Split('/');

            var f = "";
            if (e[0].Length == 1 && e[1].Length == 1)
            {
                string e0 = "0" + e[0];
                string e1 = "0" + e[1];
                f = e[2] + "-" + e1 + "-" + e0;
            }
            else if (e[1].Length == 1)
            {
                string e1 = "0" + e[1];
                f = e[2] + "-" + e1 + "-" + e[0];
            }
            else if (e[0].Length == 1)
            {

                string e0 = "0" + e[0];
                f = e[2] + "-" + e[1] + "-" + e0;
            }
            else
            {
                f = e[2] + "-" + e[1] + "-" + e[0];
            }

            return f;
        }

    }
}