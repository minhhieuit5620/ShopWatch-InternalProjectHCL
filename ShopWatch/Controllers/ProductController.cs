using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopWatch.Models;
using PagedList;
namespace ShopWatch.Controllers
{
    public class ProductController : BaseController
    {
        // GET: Product
        public ActionResult ViewProduct(int? page)
        {
            List<Cart> cartItem = Session["ShoppingCart"] as List<Cart>;         
            if (cartItem != null)
            {
                var Id_ProductCookie = Request.Cookies["Id_Pro"];
                var QuantityCookie = Request.Cookies["Quantity"];
                if (QuantityCookie.Value == null || Id_ProductCookie.Value == null)
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
                        Cart UpdateItem = cartItem.FirstOrDefault(m => m.IdProduct == pIds[j]);
                        if (UpdateItem != null)
                        {

                            UpdateItem.Quantity = pQtts[j];

                        }

                    }
                }
            }
            List<Product> listProduct = db.Products.ToList();
            List<Manufacturer> productcate = db.Manufacturers.ToList();
            ViewBag.listProductCate = productcate;
            return View(listProduct.ToPagedList(page ?? 1, 8));
        }
        public ActionResult SingleProduct(int? id )
        { 
            if (id == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var singlePro = db.Products.FirstOrDefault(c => c.ProductID == id);
            List<Product> listRelated = db.Products.Where(s => s.Manufacturer_id == singlePro.Manufacturer_id && s.ProductID != singlePro.ProductID).ToList();
            if(listRelated == null)
            {
                ViewBag.relatedProduct = "There are no similar products";
            }
            ViewBag.relatedProduct = listRelated;
            var infoProduct = db.ProductInformations.Where(a => a.ProductID == singlePro.ProductID ).FirstOrDefault();
            var moreImgProduct = db.ProductInformations.Where(a => a.ProductID == singlePro.ProductID).ToList();
            ViewBag.MoreImg = moreImgProduct;
            ViewBag.InfomationPro = infoProduct;
            return View(singlePro);
        }
        public ActionResult ManufacturerProduct(int? id,int? page)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var listCate = db.Products.Where(s => s.Manufacturer_id == id).ToList();
            List<Manufacturer> productcate = db.Manufacturers.ToList();
            ViewBag.listProductCate = productcate;
            var nameManufacturer = db.Manufacturers.Where(c => c.Manufacturer_id == id).FirstOrDefault();
            ViewBag.nameManufac = nameManufacturer;
            ViewBag.id = id;
            return View(listCate.ToPagedList(page ?? 1, 8));
        }
    }
}