using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopWatch.Models;
using ShopWatch.Models.DTO;
namespace ShopWatch.Controllers
{
    public class CheckoutController : BaseController
    {
        // GET: Checkout
        [HttpGet]
        public ActionResult Checkout()
        {
            ViewBag.infoUser = Session["User"];
            if (Request.Cookies["CookieCart"] != null)
            {
                var carts = Request.Cookies["CookieCart"].Value;
                var proValue = Server.UrlDecode(carts);
                var base64EncodedBytes = System.Convert.FromBase64String(proValue);
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
           


            //var Cookie_id = Request.Cookies["Id"];
            //var Cookie_quantity = Request.Cookies["Quantity"];
            //if (Cookie_id != null)
            //{
            //    var Product_Id = Cookie_id.Value;
            //}

            //return View(cartItem);
        }
        [HttpPost]
        public ActionResult Checkout(string Fullname , string Address, int phone, string email,string note, string Payment_method)
        {
               
                Order order = new Order();
                order.OrderName = Fullname;
                order.OrderAddress = Address;
                order.OrderEmail = email;
                order.OrderPhone = phone;
                order.Payment_method = Payment_method;
                order.OrderNote = note;
                order.StaffID = 2;
                order.UserID = ((User)Session["User"]).UserID;
                order.OrderDate = DateTime.Now;

                order.Status = 1;
                double Total = 0;

            if (Request.Cookies["CookieCart"] != null)
            {
                var carts = Request.Cookies["CookieCart"].Value;

                var proValue = Server.UrlDecode(carts);

                var base64EncodedBytes = System.Convert.FromBase64String(proValue);
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
                var Subtotal = 0;
                var Tax = 0;
                foreach (Cart item in cart)
                {
                    Subtotal += item.Quantity * item.Price;
                }
                Tax = Subtotal * 10 / 100;
                Total = Subtotal + Tax;

            }
            order.TotalMoney = Total;
            db.Orders.Add(order);
            db.SaveChanges();


            if (Request.Cookies["CookieCart"] != null)
            {
                var carts = Request.Cookies["CookieCart"].Value;
                var proValue = Server.UrlDecode(carts);

                var base64EncodedBytes = System.Convert.FromBase64String(proValue);
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
                foreach (Cart item in cart)
                {
                    OrderDetail orderDetail = new OrderDetail();
                    orderDetail.OrderID = order.OrderID;
                    orderDetail.ProductID = item.IdProduct;
                    orderDetail.ProductName = item.NameProduct;
                    orderDetail.Quantity = item.Quantity;
                    orderDetail.Price = item.Price;
                    orderDetail.Total = item.Price * item.Quantity;
                    orderDetail.Image = item.Image;
                    orderDetail.OrderDate = DateTime.Now;
                    orderDetail.Status = 1;
                    db.OrderDetails.Add(orderDetail);
                    db.SaveChanges();
                }

            }           
            Response.Cookies["CookieCart"].Expires = DateTime.Now.AddDays(-1);








            #region orderDetail Session
            //if (Session["ShoppingCart"] != null)
            //    {
            //        var Cart = (List<Cart>)Session["ShoppingCart"];
            //        var Subtotal = 0;
            //        var Tax = 0;
            //        foreach (Cart item in Cart)
            //        {
            //            Subtotal += item.Quantity * item.Price;
            //        }
            //        Tax = Subtotal * 10 / 100;
            //        Total = Subtotal + Tax;
            //    }
            //    order.TotalMoney = Total;
            //    db.Orders.Add(order);
            //    db.SaveChanges();

            //    //Import to orderDeatail
            //    if (Session["ShoppingCart"] != null)
            //    {
            //        var Cart = (List<Cart>)Session["ShoppingCart"];
            //        foreach (Cart item in Cart)
            //        {
            //            OrderDetail orderDetail = new OrderDetail();
            //            orderDetail.OrderID = order.OrderID;
            //            orderDetail.ProductID = item.IdProduct;
            //            orderDetail.ProductName = item.NameProduct;
            //            orderDetail.Quantity = item.Quantity;
            //            orderDetail.Price = item.Price;
            //            orderDetail.Total = item.Price*item.Quantity;                        
            //            orderDetail.Image = item.Image;
            //            orderDetail.OrderDate = DateTime.Now;
            //            orderDetail.Status = 1;
            //            db.OrderDetails.Add(orderDetail);
            //            db.SaveChanges();
            //        }
            //    }

            //    Session["ShoppingCart"] = null;

            #endregion

            var idNewOrder = order.OrderID;
            return RedirectToAction("GetOrderDetail/" + idNewOrder, "Checkout");
           // return RedirectToAction("ShoppingCart","Cart");          
        }

        [HttpGet]
        public ActionResult Getorder()
        {
           
            if (Session["User"] != null)
            {               
                var UserId = ((User)Session["User"]).UserID;
               List<Order> listOrder = db.Orders.OrderByDescending(c=>c.OrderID).Where(s => s.UserID ==UserId ).ToList();

               // List<OrderDetail> a = db.OrderDetails.Where(c => c.OrderID == ).ToList();
                return View(listOrder);
            }
            else
            {
                return View();
            }

        }
        public ActionResult GetOrderDetail(int id)
        {
            var order = db.Orders.FirstOrDefault(s => s.OrderID == id);
            var orderDetail = db.OrderDetails.Where(s => s.OrderID == order.OrderID).ToList();
            ViewBag.orderDetail = orderDetail;

            return View(order);
        }
        public ActionResult ChangeStatus(int id)
        {
            var orderDetail = db.OrderDetails.FirstOrDefault(c=>c.ID==id);
            if (orderDetail != null)
            {
                orderDetail.Status = -1;
                db.SaveChanges();
            }                       
            return RedirectToAction("Getorder");
        }
    }
}