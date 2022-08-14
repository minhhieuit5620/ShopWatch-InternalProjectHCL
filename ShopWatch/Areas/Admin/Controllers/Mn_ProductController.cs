using PagedList;
using ShopWatch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopWatch.Models.DTO;

namespace ShopWatch.Areas.Admin.Controllers
{
    public class Mn_ProductController : BaseController
    {
        // GET: Admin/Mn_Product
        public ActionResult ShowProduct()
        {
            List<Product> listProduct = db.Products.ToList();
            return View(listProduct);
            //return View(listProduct.ToPagedList(page ?? 1, 8));           
        }
        public ActionResult Detail(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("ShowProduct");
            }
            Product product = db.Products.Find(id);
            ProductInformation Infoproducts = db.ProductInformations.Where(op => op.ProductID == id).FirstOrDefault();
            //Manufacturer manufacturer=db.Manufacturers.Where(mn=>mn.Product.)
            // var nameManufacturer = db.Manufacturers.Where(c => c.Manufacturer_id == id).FirstOrDefault();
            // ViewBag.nameManufac = nameManufacturer;
            Category nameCate = db.Categories.Where(c => c.Category_id == product.Category_id).FirstOrDefault();
            ViewBag.nameCategory = nameCate;
            Product_ProductInformation_Manufacturer Product_ProductInformation_Manufacturer = new Product_ProductInformation_Manufacturer()
            {
                Product = product,
                ProductInformation = Infoproducts,
               // Manufacturer = nameManufacturer
            };
            ViewBag.pro_info_manu = Product_ProductInformation_Manufacturer;
            return View(Product_ProductInformation_Manufacturer);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("ShowProduct");
            }
            Product product = db.Products.Find(id);
            ProductInformation Infoproducts = db.ProductInformations.Where(op => op.ProductID == id).FirstOrDefault();
            //Manufacturer manufacturer=db.Manufacturers.Where(mn=>mn.Product.)
            // var nameManufacturer = db.Manufacturers.Where(c => c.Manufacturer_id == id).FirstOrDefault();
            // ViewBag.nameManufac = nameManufacturer;
            var ma = db.Manufacturers.ToList();
            ViewBag.manu = ma;

            Category nameCate = db.Categories.Where(c => c.Category_id == product.Category_id).FirstOrDefault();
            ViewBag.nameCategory = nameCate;
            Product_ProductInformation_Manufacturer Product_ProductInformation_Manufacturer = new Product_ProductInformation_Manufacturer()
            {
                Product = product,
                ProductInformation = Infoproducts,
                // Manufacturer = nameManufacturer
            };
            ViewBag.pro_info_manu = Product_ProductInformation_Manufacturer;
            return View(Product_ProductInformation_Manufacturer);
        }

        [HttpPost]
        public ActionResult Edit(Product_ProductInformation_Manufacturer  prd)
        {       
            var ds = db.Products.FirstOrDefault(s => s.ProductID == prd.Product.ProductID);
            var Inforproducts = db.ProductInformations.FirstOrDefault(op => op.ProductID == ds.ProductID);

            if (ds != null && Inforproducts!=null)
            {

                ds.ProductName = prd.Product.ProductName;
                ds.Description = prd.Product.Description;
                ds.ProductImage = prd.Product.ProductImage;
                ds.Price = prd.Product.Price;
                ds.PriceOld = prd.Product.PriceOld;
                ds.ImportPrice = prd.Product.ImportPrice;
                ds.Category_id = prd.Product.Category_id;
                ds.Manufacturer_id = prd.Product.Manufacturer_id;
                ds.Amount = prd.Product.Amount;
                db.SaveChanges();
            }
            return Json(new { success = true, message = "Bạn đã sửa thành công" }, JsonRequestBehavior.AllowGet);
            //}

        }
    }
}