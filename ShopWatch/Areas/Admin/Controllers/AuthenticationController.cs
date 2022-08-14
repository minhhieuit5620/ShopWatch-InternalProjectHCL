using ShopWatch.Models;
using System.Linq;
using System.Web.Mvc;

namespace ShopWatch.Areas.Admin.Controllers
{
    public class AuthenticationController : Controller
    {
        // GET: Admin/Authentication
        private ShopEntities db = new ShopEntities();
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string username ,string password)
        {
            User u = db.Users.FirstOrDefault(a => a.UserName == username|| a.Email==username && a.PassWord == password);
            if (u != null)
            {
                Session["User"] = u;
                Session["Name"] = u.FullName;


                Session["Message"] = "Login successfully !";
                return RedirectToAction("Index", "Home_Admin");
            }
            else
                Session["Message"] = "Login unsuccessfully, username or password incorrect !";
            ModelState.AddModelError("User_Name", "Incorrect account or password");
            return View();
        }
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }
    }
}