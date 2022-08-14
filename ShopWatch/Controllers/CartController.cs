using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopWatch.Models;
using System.Text;
using Newtonsoft.Json;

namespace ShopWatch.Controllers
{
    public class CartController : BaseController
    {

        public ActionResult ShoppingCart()
        {

            if (Request.Cookies["CookieCart"] != null)
            {
                var carts = Request.Cookies["CookieCart"].Value;

                //var proValue = Server.UrlDecode(carts);
                var base64EncodedBytes = System.Convert.FromBase64String(carts);
                var cartItem = System.Text.Encoding.UTF8.GetString(base64EncodedBytes);

                var qtts = cartItem.Split('#');


                List<Cart> cart = new List<Cart>();


                for (int i = 0; i < qtts.Length - 1; i++)
                {
                    var qttss = qtts[i].Split('|');

                    if (qtts[i].Length == 0) { continue; }
                    List<string> itemm = qttss.Cast<string>().ToList();
                    Cart items = new Cart();
                    items.IdProduct = int.Parse(itemm[0]);
                    items.NameProduct = itemm[1];
                    items.Quantity = int.Parse(itemm[2]);
                    items.Image = itemm[3];
                    items.Price = int.Parse(itemm[4]);
                    cart.Add(items);
                }
                return View(cart);
            }
            else
            {

                return View();
            }
        }
        public ActionResult AddCartCookie(int id, int quantity)
        {
            // List < Cart > cartItem = new List<Cart>();
            //cartItem = JsonConvert.DeserializeObject(shoppingItems.Value) as List<Cart>;               
            if (Request.Cookies["CookieCart"] == null)
            {

                HttpCookie CookieCart = new HttpCookie("CookieCart");
                var list = db.Products.FirstOrDefault(s => s.ProductID == id);
                CookieCart.Expires = DateTime.Now.AddDays(1);
                CookieCart.Path = "/";     
                var value =  id.ToString() +
                                    "|" + list.ProductName +
                                    "|" + quantity.ToString() +
                                    "|" + list.ProductImage +
                                    "|" + list.Price.ToString() + "#";
                //encode
                
                var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(value);
                var proValue= System.Convert.ToBase64String(plainTextBytes);
                CookieCart.Value = proValue;
               // CookieCart.Value = Server.UrlEncode(proValue);
                Response.Cookies.Add(CookieCart);
            }
            else
            {
                var cart = Request.Cookies["CookieCart"].Value;

                //decode
               // var proValue = Server.UrlDecode(cart);
                var base64EncodedBytes = System.Convert.FromBase64String(cart);
                var cartItem = System.Text.Encoding.UTF8.GetString(base64EncodedBytes);

                
                var qtts = cartItem.Split('#');


                var str = "";

                var checkFlag = false;
                for (int i = 0; i < qtts.Length - 1; i++)
                {
                    var productDetails = qtts[i].Split('|');
                    if (id == int.Parse(productDetails[0]))
                    {
                        checkFlag = true;
                        productDetails[2] = (int.Parse(productDetails[2]) + quantity).ToString();
                        var itemNew = String.Join("|", productDetails);
                        qtts[i] = itemNew;
                        break;
                    }
                }
                if (checkFlag == false)
                {

                    var list = db.Products.FirstOrDefault(s => s.ProductID == id);
                    var newvalue = id.ToString() +
                                        "|" + list.ProductName +
                                        "|" + quantity.ToString() +
                                        "|" + list.ProductImage +
                                        "|" + list.Price.ToString();
                    str = Request.Cookies["CookieCart"].Value;

                    //var proValues = Server.UrlDecode(str);
                    var base64EncodedBytess = System.Convert.FromBase64String(str);
                    var strs = System.Text.Encoding.UTF8.GetString(base64EncodedBytess);

                    str = strs + newvalue + "#";

                    //encode
                   // var proEn = Server.UrlEncode(str);

                    var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(str);
                    var proEn = System.Convert.ToBase64String(plainTextBytes);
                    //  Response.Cookies["CookieCart"].Value= Server.UrlEncode(proEn);
                    Response.Cookies["CookieCart"].Value = proEn;
                    Response.Cookies["CookieCart"].Expires = DateTime.Now.AddDays(1);
                }
                else
                {
                    var qttsNew = String.Join("#", qtts);
                 
                    var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(qttsNew);
                    var proEn = System.Convert.ToBase64String(plainTextBytes);
                    // Response.Cookies["CookieCart"].Value= Server.UrlEncode(proEn);
                    Response.Cookies["CookieCart"].Value = proEn;
                     Response.Cookies["CookieCart"].Expires= DateTime.Now.AddDays(1);                   
                }
                     
            }

            return RedirectToAction("ShoppingCart");
        

        }

        #region add cart by session
        public ActionResult AddToCart(int? idProduct, int quantity)
        {
            List<Cart> cart = new List<Cart>();
            if (Session["ShoppingCart"] == null)
            {
                var list = db.Products.FirstOrDefault(s => s.ProductID == idProduct);
                Cart pro = new Cart()
                {
                    IdProduct = list.ProductID,
                    NameProduct = list.ProductName,
                    Image = list.ProductImage,
                    Quantity = quantity,
                    Price = (int)list.Price,
                };
                cart.Add(pro);
            }

            else
            {
                cart = (List<Cart>)Session["ShoppingCart"];
                var dt = cart.FirstOrDefault(s => s.IdProduct == idProduct);
                if (dt != null)
                {
                    dt.Quantity = dt.Quantity + 1;

                }
                else// If the product is not in the cart before
                {
                    var ds = db.Products.FirstOrDefault(s => s.ProductID == idProduct);
                    Cart sp = new Cart()
                    {
                        IdProduct = ds.ProductID,
                        NameProduct = ds.ProductName,
                        Image = ds.ProductImage,
                        Quantity = quantity,
                        Price = (int)ds.Price,


                    };
                    cart.Add(sp);

                }
            }
            Session["ShoppingCart"] = cart;
            return RedirectToAction("ShoppingCart");
        }
        #endregion
       
        public RedirectToRouteResult DeleteCart(int IdProduct)
        {
            #region delete cart session
            //List<Cart> CartItem = Session["ShoppingCart"] as List<Cart>;
            //Cart Deleteitem = CartItem.FirstOrDefault(m => m.IdProduct == IdProduct);
            //if (Deleteitem != null)
            //{
            //    CartItem.Remove(Deleteitem);
            //}
            #endregion
            var cart = Request.Cookies["CookieCart"].Value;           

            //var proValue = Server.UrlDecode(cart);
            var base64EncodedBytes = System.Convert.FromBase64String(cart);
            var cartItem = System.Text.Encoding.UTF8.GetString(base64EncodedBytes);

            var qtts = cartItem.Split('#');
            for (int i = 0; i < qtts.Length-1; i++)
            {
                var detail = qtts[i].Split('|');
               
                if (int.Parse(detail[0]) == IdProduct)
                {
                    qtts[i] = "";
                    break;
                }
            }
            var qttsNew = "";

            for (int i = 0; i < qtts.Length; i++)
            {
                if (qtts[i] != "")
                {
                    qttsNew += qtts[i] + "#";
                }

            }

            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(qttsNew);
            var proEn = System.Convert.ToBase64String(plainTextBytes);
            //Response.Cookies["CookieCart"].Value = Server.UrlEncode(proEn);
            Response.Cookies["CookieCart"].Value = proEn;
            Response.Cookies["CookieCart"].Expires = DateTime.Now.AddDays(1);
            return RedirectToAction("ShoppingCart");
        }


        public RedirectToRouteResult DeleteCartMutiple(int[] IdProduct)
        {
            #region delete cart_mutiple session
            //List<Cart> CartItem = Session["ShoppingCart"] as List<Cart>;

            //foreach (var i in IdProduct)
            //{
            //    Cart Deleteitem = CartItem.FirstOrDefault(m => m.IdProduct == i);
            //    if (Deleteitem != null)
            //    {
            //        CartItem.Remove(Deleteitem);
            //    }
            //}
            #endregion

            var cart = Request.Cookies["CookieCart"].Value;
            // var cartItem = Server.UrlDecode(cart);
            //var base64EncodedBytes = System.Convert.FromBase64String(cart);
            //var cartItem = System.Text.Encoding.UTF8.GetString(base64EncodedBytes);


            //var proValue = Server.UrlDecode(cart);
            var base64EncodedBytes = System.Convert.FromBase64String(cart);
            var cartItem = System.Text.Encoding.UTF8.GetString(base64EncodedBytes);

            var qtts = cartItem.Split('#');           
            for (int i = 0; i < qtts.Length - 1; i++)
            {
                var detail = qtts[i].Split('|');
                foreach (var item in IdProduct)
                {
                    if (int.Parse(detail[0]) == item)
                    {
                        qtts[i] = "";
                        break;
                    }
                }  
               
            }
            var qttsNew = "";

            for (int i = 0; i < qtts.Length; i++)
            {
                if (qtts[i] != "")
                {
                    qttsNew += qtts[i] + "#";                   
                }

            }
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(qttsNew);
            var proEn = System.Convert.ToBase64String(plainTextBytes);
            Response.Cookies["CookieCart"].Value = proEn;
          // Response.Cookies["CookieCart"].Value = Server.UrlEncode(proEn);
            Response.Cookies["CookieCart"].Expires = DateTime.Now.AddDays(1);

            return RedirectToAction("ShoppingCart");
        }

        public RedirectToRouteResult updateQuantity_MTP()
        {
            List<Cart> CartItem = Session["ShoppingCart"] as List<Cart>;
            var Id_ProductCookie = Request.Cookies["Id_Pro"];
            var QuantityCookie = Request.Cookies["Quantity"];
            if (QuantityCookie.Value == "" || Id_ProductCookie.Value == "")
            {
                return RedirectToAction("ShoppingCart");
            }
            else
            {
                var IdList = Id_ProductCookie.Value;
                var Ids = IdList.Split('-');
                List<int> pIds = Ids.Select(c => int.Parse(c)).ToList();

                var qttList = QuantityCookie.Value;
                var qtts = qttList.Split('-');
                List<int> pQtts = qtts.Select(c => int.Parse(c)).ToList();
                for (int j = 0; j < pIds.Count; j++)
                {
                    Cart UpdateItem = CartItem.FirstOrDefault(m => m.IdProduct == pIds[j]);
                    if (UpdateItem != null)
                    {

                        UpdateItem.Quantity = pQtts[j];

                    }

                }

                return RedirectToAction("ShoppingCart");

            }

        }
        public RedirectToRouteResult UpdateMutiple(int[] IdProduct, int[] Quantity)
        {
            List<Cart> CartItem = Session["ShoppingCart"] as List<Cart>;

            if (IdProduct == null || Quantity == null)
            {
                return RedirectToAction("ShoppingCart");
            }
            else
            {


                for (int j = 0; j < IdProduct.Length; j++)
                {

                    Cart UpdateItem = CartItem.FirstOrDefault(m => m.IdProduct == IdProduct[j]);
                    if (UpdateItem != null)
                    {                       
                        UpdateItem.Quantity = Quantity[j];

                    }
                }
                return RedirectToAction("ShoppingCart");
            }


        }



        //---------------------------------------------------------------

        public ActionResult Favorite()
        {
            //if (Session["User"] == null)
            //{
            //    return RedirectToAction("Login", "Home");
            //}
            //else
            //{
            if (Session["User"] != null)
            {
                var User_id = ((User)Session["User"]).UserID;
                List<Favorite> favorites = db.Favorites.Where(c => c.User_id == User_id).ToList();

                //ViewBag.ProductID = new SelectList(db.Manufacturers, "Manufacturer_id", "Name", favorites.ProductID);
                //ViewBag.ProductID = new SelectList(db.ProductInformations, "ProductID", "Brand", product.ProductID);
                return View(favorites);
            }
            else
            {
                return View();
            }

        }
        public ActionResult AddToFavorite(int? id)
        {
            var list = db.Products.FirstOrDefault(s => s.ProductID == id);
            var fvr = db.Favorites.FirstOrDefault(s => s.Product_Id == id);
            if (fvr == null)
            {
                //if(Session["User"] != null)
                //{
                Favorite favorite = new Favorite()
                {
                    Product_Id = list.ProductID,
                    NameProduct = list.ProductName,
                    ProductImage = list.ProductImage,
                    Price = (int)list.Price,
                    Manufacturer_id = list.Manufacturer_id,
                    Category_id = list.Category_id,
                    User_id = ((User)Session["User"]).UserID,

                };
                db.Favorites.Add(favorite);
                db.SaveChanges();
                return RedirectToAction("Favorite");
                //}
                //else
                //{
                //    return RedirectToAction("Login", "Home");
                //}

            }
            else
            {
                Session["Message"] = "Added to favorites.";
                //return Redirect(ControllerContext.HttpContext.Request.UrlReferrer.ToString());
                return RedirectToAction("Login", "Home");
            }
        }
        public RedirectToRouteResult DeleteFavorite(int id)
        {
            Favorite Deleteitem = db.Favorites.FirstOrDefault(c => c.Favorite_Id == id);
            //Cart Deleteitem = CartItem.FirstOrDefault(m => m.IdProduct == IdProduct);
            if (Deleteitem != null)
            {
                db.Favorites.Remove(Deleteitem);
                db.SaveChanges();
            }
            return RedirectToAction("Favorite");
        }


    }
}