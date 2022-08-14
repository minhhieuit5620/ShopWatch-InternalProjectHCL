using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using ShopWatch.Models;
using ShopWatch.Models.DTO;
using System.Web.UI.HtmlControls;
using System.Web.Services.Description;

using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace ShopWatch.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            List<Product> product = db.Products.OrderByDescending(c => c.ProductID).Take(8).ToList();
            List<Manufacturer> productcate = db.Manufacturers.ToList();
            ViewBag.listCate = productcate;
            return View(product);
        }
        public ActionResult Login()
        {
            string port = Request.UrlReferrer.ToString();
            Session["urlBefore"] = port;
            return View();
        }
        [HttpPost]
        public ActionResult Login(string username, string password)
        {

            var scPass = GetMD5(password);
            User u = db.Users.FirstOrDefault(a => a.UserName == username && a.PassWord == scPass);
            if (u != null)
            {
                Session["User"] = u;
                Session["Name"] = u.FullName;


                Session["Message"] = "Login successfully !";
                var url = Session["urlBefore"];

                // return RedirectToAction("Index");
                return Redirect(url.ToString());
            }
            else
                Session["Message"] = "Login unsuccessfully, username or password incorrect !";
            ModelState.AddModelError("User_Name", "Incorrect account or password");
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(string Fullname, DateTime birthday, string gender, string address, string username, string password, string email, string phone)
        {
            var checkUsername = db.Users.Where(c => c.UserName == username).FirstOrDefault();
            var checkEmail = db.Users.Where(c => c.Email == email).FirstOrDefault();
            if (checkUsername == null && checkEmail == null)
            {
                User user = new User();
                {
                    user.FullName = Fullname;
                    user.Dob = birthday;
                    user.Gender = gender;
                    user.Address = address;
                    user.UserName = username;
                    user.PassWord = GetMD5(password);
                    user.Phone = phone;
                    user.Email = email;
                    user.Rol = 1;
                }
                db.Configuration.ValidateOnSaveEnabled = false;
                db.Users.Add(user);
                db.SaveChanges();
                Session["Message"] = "Register successfully, Please Login again to continue";
                return RedirectToAction("RegisterSuccess");
            }
            else
            {
                //Session["Message"] = "Register unsuccessfully, username or password incorrect !";
                ModelState.AddModelError("User_Name", "User name already exists.");
                return View();
            }

        }
        public ActionResult RegisterSuccess()
        {
            return View();
        }
        public ActionResult Logout()
        {
            Session["User"] = null;
            return RedirectToAction("Index", "Home");
        }

        public ActionResult EditProfile()
        {
            //User user = (User)Session["User"];
            //var password_User = new Password_User()
            //{
            //    User_Id = user.UserID,
            //    CurrentPassword = user.PassWord

            //};
            if (Session["User"] != null)
            {
                ViewBag.infoUser = Session["User"];
            }
            return View();
        }
        [HttpPost]
        // public ActionResult EditProfile( string Fullname,string Address,string phone,string email,string gender,DateTime dob)

        public ActionResult EditProfile(User users)
        {
            var UserId = ((User)Session["User"]).UserID;
            var list = db.Users.FirstOrDefault(s => s.UserID == UserId);
            if (list != null)
            {
                list.FullName = users.FullName;
                list.Dob = users.Dob;
                list.Gender = users.Gender;
                list.Address = users.Address;
                list.PassWord = users.PassWord;
                list.Phone = users.Phone;
                list.Email = users.Email;
                db.SaveChanges();
                Session["User"] = null;
                Session["Message"] = "Updated profile successfully, Please Login again to continue";
            }
            return RedirectToAction("Index", "Home");



        }
        //create a string MD5
        public static string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");

            }
            return byte2String;
        }

        [System.ComponentModel.Browsable(false)]
        public virtual string InnerHtml { get; set; }

        static readonly HttpClient httpClient = new HttpClient();
        public ActionResult Test(string url)
        {


            var abc = Emb(url);
            ViewBag.check = abc;
            return View(abc);
        }
        public async Task<ActionResult> Emb(string url)
        {
            if (url == null)
            {
                RedirectToAction("Index");
            }
            List<ortherWeb> info = new List<ortherWeb>();            
            httpClient.DefaultRequestHeaders.Add("Accept", "text/html,application/xhtml+xml+json");
            HttpResponseMessage response = await httpClient.GetAsync(url);
            string htmltext = await response.Content.ReadAsStringAsync();        
            var listImg = Regex.Matches(htmltext, @"<div class=""product-img(.*?)</div>", RegexOptions.Singleline);            
            var listText = Regex.Matches(htmltext, @"<div class=""product-info text-center(.*?)</div>", RegexOptions.Singleline);

          
            for (int i = 0; i < listText.Count; i++)              
            {
                ortherWeb list = new ortherWeb();               
                string linkImg = Regex.Match(listImg[i].ToString(), @"data-src=""(.*?)""").Value.Replace("data-src=\"", "").Replace(@"\", "").Replace("\"", "");
                                                               
                string title=Regex.Match(listText[i].ToString(), @"class=""product-title a"">(.*?)</a>").Value.Replace("class=", "").Replace("product-title a","").Replace("\"", "").Replace("</a>","").Replace(">", "");
                string price=Regex.Match(listText[i].ToString(), @"class=""current-price"">(.*?)</span>").Value.Replace("class=", "").Replace("current-price", "").Replace("\"", "").Replace("</span>", "").Replace(">", "");
                list.img = linkImg;
                list.name = title;
                list.price = price;
                info.Add(list);
            }
            
            
            return View(info);
        }

      
    }
}