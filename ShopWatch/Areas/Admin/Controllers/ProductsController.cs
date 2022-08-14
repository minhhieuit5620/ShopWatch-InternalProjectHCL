using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using ShopWatch.Models;

namespace ShopWatch.Areas.Admin.Controllers
{
    public class ProductsController : BaseController
    {
        // private ShopEntities db = new ShopEntities();

        // GET: Admin/Products
        public ActionResult Index(int? page)
        {
            List<Product> products = db.Products.Include(p => p.Manufacturer).Include(p => p.ProductInformation).ToList();
            return View(products.ToPagedList(page ?? 1, 8));
        }

        // GET: Admin/Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            var info = db.ProductInformations.Where(c => c.ProductID == id).FirstOrDefault();
            ViewBag.Information = info;
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Admin/Products/Create
        public ActionResult Create()
        {
            ViewBag.ProductID = new SelectList(db.Manufacturers, "Manufacturer_id", "Name");
            ViewBag.ProductID = new SelectList(db.ProductInformations, "ProductID", "Brand");
            ViewBag.manufacturers = db.Manufacturers.ToList();
            ViewBag.category = db.Categories.ToList();
            return View();
        }

        // POST: Admin/Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //
        //string ProductName,string Description,string ProductImage,int Price,int PriceOld,int ImportPrice,int Manufacturer_id,int Amount,int Category_id
        // public ActionResult Create([Bind(Include = "ProductName,Description,ProductImage,Price,PriceOld,ImportPrice,Manufacturer_id,Amount,Category_id")] Product product)
        public ActionResult Create(string ProductName, string Description, HttpPostedFileBase ProductImage, int Price, int PriceOld, int ImportPrice, int Manufacturer_id, int Amount, int Category_id
            , string Brand, string Origin, string Type, string Thickness, string Shell_material, string Strap_material, string Glass_material, string Water_resisstance, string Sex, HttpPostedFileBase[] MoreImage)
        {
            if (ModelState.IsValid)
            {
                if (db.Products.FirstOrDefault(a => a.ProductName == ProductName) != null)
                {
                    ModelState.AddModelError("Name", "This name already existed in the Database");

                    return View();

                }
                if (ProductImage != null && MoreImage[0] != null)
                {
                    var guidname = Guid.NewGuid().ToString();
                    string _FileName = guidname + Path.GetFileName(ProductImage.FileName);
                    string _path = Path.Combine(Server.MapPath("~/Asset/img/"), _FileName);
                    ProductImage.SaveAs(_path);
                    var countInfor = db.ProductInformations.Count();

                    Product newitem = new Product()
                    {
                        //ProductID = count + 1,
                        ProductName = ProductName,
                        Amount = Amount,
                        Price = Price,
                        Description = Description,
                        ProductImage = _FileName,
                        PriceOld = PriceOld,
                        ImportPrice = ImportPrice,
                        Category_id = Category_id,
                        Manufacturer_id = Manufacturer_id,
                    };
                    db.Products.Add(newitem);
                    db.SaveChanges();
                    if (MoreImage[0] != null)
                    {
                        ProductInformation newInfor = new ProductInformation()
                        {
                            ProductID = newitem.ProductID,//countInfor+1,
                            Brand = Brand,
                            Origin = Origin,
                            Type = Type,
                            Thickness = Thickness + " mm",
                            Shell_material = Shell_material,
                            Strap_material = Strap_material,
                            Glass_material = Glass_material,
                            Water_resistance = Water_resisstance + " m",
                            Sex = Sex,
                        };
                        foreach (HttpPostedFileBase file in MoreImage)
                        {
                            //Checking file is available to save.  
                            if (file != null)
                            {
                                string FileNameMore = guidname + Path.GetFileName(file.FileName);
                                string ServerSavePath = Path.Combine(Server.MapPath("~/Asset/img/more-image/"), FileNameMore);
                                //Save file to server folder  
                                file.SaveAs(ServerSavePath);
                                //System.IO.File.Delete(ServerSavePath);
                                newInfor.MoreImage += FileNameMore + ";";
                            }
                        }
                        db.ProductInformations.Add(newInfor);
                        db.SaveChanges();
                    }
                    else
                    {
                        ProductInformation newInfor = new ProductInformation()
                        {
                            ProductID = newitem.ProductID,//countInfor+1,
                            Brand = Brand,
                            Origin = Origin,
                            Type = Type,
                            Thickness = Thickness + " mm",
                            Shell_material = Shell_material,
                            Strap_material = Strap_material,
                            Glass_material = Glass_material,
                            Water_resistance = Water_resisstance + " m",
                            Sex = Sex,
                        };
                    }
                    return RedirectToAction("Index");
                }
            }
            return View();
        }
        // GET: Admin/Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            var singlePro = db.Products.FirstOrDefault(c => c.ProductID == id);
            var productInformation = db.ProductInformations.Where(a => a.ProductID == singlePro.ProductID).FirstOrDefault();
            if (product == null || productInformation == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductInfo = productInformation;

            ViewBag.ProductID = new SelectList(db.Manufacturers, "Manufacturer_id", "Name", product.ProductID);
            ViewBag.ProductID = new SelectList(db.ProductInformations, "ProductID", "Brand", product.ProductID);
            //var a = db.Manufacturers.Where(c => c.Manufacturer_id == singlePro.Manufacturer_id && c.ProductID != singlePro.ProductID).T
            ViewBag.manufacturers = db.Manufacturers.ToList();
            ViewBag.category = db.Categories.ToList();
            return View(product);
        }

        // POST: Admin/Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( /*Product pro, HttpPostedFileBase ProductImage,ProductInformation proInfo)*/
            int ProductID, string ProductName, string Description, HttpPostedFileBase ProductImage, int Price, int PriceOld, int ImportPrice, int Manufacturer_id, int Amount, int Category_id
            , string Brand, string Origin, string Type, string Thickness, string Shell_material, string Strap_material, string Glass_material, string Water_resistance, string Sex, HttpPostedFileBase[] MoreImage)

        {
            if (ModelState.IsValid)
            {


                if (ProductImage != null)
                {
                    var guidname = Guid.NewGuid().ToString();
                    string _FileName = guidname + Path.GetFileName(ProductImage.FileName);
                    string _path = Path.Combine(Server.MapPath("~/Asset/img/"), _FileName);
                    ProductImage.SaveAs(_path);

                    var Product = db.Products.FirstOrDefault(c => c.ProductID == ProductID);

                    var Info = db.ProductInformations.FirstOrDefault(s => s.ProductID == ProductID);

                    if (MoreImage[0] != null)
                    {
                        string[] imageList = Info.MoreImage.Split(';');

                        for (int i = 0; i < imageList.Length - 1; i++)
                        {
                            System.IO.File.Delete(Path.Combine(Server.MapPath("~/Asset/img/more-image/"), imageList[i]));
                        }
                        Info.MoreImage = "";
                        if (Product != null && Info != null)
                        {
                            Product.ProductName = ProductName;
                            Product.Description = Description;
                            Product.ProductImage = _FileName;
                            Product.Price = Price;
                            Product.PriceOld = PriceOld;
                            Product.ImportPrice = ImportPrice;
                            Product.Manufacturer_id = Manufacturer_id;
                            Product.Amount = Amount;
                            Product.Category_id = Category_id;

                            //-------------info                        
                            Info.ProductID = ProductID;
                            Info.Brand = Brand;
                            Info.Origin = Origin;
                            Info.Type = Type;
                            Info.Thickness = Thickness;
                            Info.Shell_material = Shell_material;
                            Info.Strap_material = Strap_material;
                            Info.Glass_material = Glass_material;
                            Info.Water_resistance = Water_resistance;
                            Info.Sex = Sex;
                            //Info.MoreImage = MoreImage;
                            foreach (HttpPostedFileBase file in MoreImage)
                            {
                                //Checking file is available to save.  
                                if (file != null)
                                {
                                    string FileNameMore = guidname + Path.GetFileName(file.FileName);
                                    string ServerSavePath = Path.Combine(Server.MapPath("~/Asset/img/more-image/"), FileNameMore);
                                    //Save file to server folder  
                                    file.SaveAs(ServerSavePath);
                                    Info.MoreImage += FileNameMore + ";";
                                }
                            }
                            // db.ProductInformations.Add(newInfor);
                            db.SaveChanges();
                        }
                        // db.SaveChanges();
                    }
                    else
                    {
                        if (Product != null && Info != null)
                        {
                            Product.ProductName = ProductName;
                            Product.Description = Description;
                            Product.ProductImage = _FileName;
                            Product.Price = Price;
                            Product.PriceOld = PriceOld;
                            Product.ImportPrice = ImportPrice;
                            Product.Manufacturer_id = Manufacturer_id;
                            Product.Amount = Amount;
                            Product.Category_id = Category_id;

                            //-------------info                        
                            Info.ProductID = ProductID;
                            Info.Brand = Brand;
                            Info.Origin = Origin;
                            Info.Type = Type;
                            Info.Thickness = Thickness;
                            Info.Shell_material = Shell_material;
                            Info.Strap_material = Strap_material;
                            Info.Glass_material = Glass_material;
                            Info.Water_resistance = Water_resistance;
                            Info.Sex = Sex;
                            Info.MoreImage = Info.MoreImage;
                        }
                        db.SaveChanges();
                    }
                    RedirectToAction("Index");
                }
                else
                {
                    var Products = db.Products.FirstOrDefault(c => c.ProductID == ProductID);

                    var Infos = db.ProductInformations.FirstOrDefault(s => s.ProductID == ProductID);
                    if (MoreImage[0] != null)
                    {
                        string[] imageList = Infos.MoreImage.Split(';');

                        for (int i = 0; i < imageList.Length - 1; i++)
                        {
                            System.IO.File.Delete(Path.Combine(Server.MapPath("~/Asset/img/more-image/"), imageList[i]));
                        }
                        Infos.MoreImage = "";
                        if (Products != null && Infos != null)
                        {
                            Products.ProductName = ProductName;
                            Products.Description = Description;
                            Products.ProductImage = Products.ProductImage;
                            Products.Price = Price;
                            Products.PriceOld = PriceOld;
                            Products.ImportPrice = ImportPrice;
                            Products.Manufacturer_id = Manufacturer_id;
                            Products.Amount = Amount;
                            Products.Category_id = Category_id;

                            //-------------info
                            Infos.ProductID = ProductID;
                            Infos.Brand = Brand;
                            Infos.Origin = Origin;
                            Infos.Type = Type;
                            Infos.Thickness = Thickness;
                            Infos.Shell_material = Shell_material;
                            Infos.Strap_material = Strap_material;
                            Infos.Glass_material = Glass_material;
                            Infos.Water_resistance = Water_resistance;
                            Infos.Sex = Sex;
                            foreach (HttpPostedFileBase file in MoreImage)
                            {
                                //Checking file is available to save.  
                                if (file != null)
                                {
                                    var guidname = Guid.NewGuid().ToString();
                                    string FileNameMore = guidname + Path.GetFileName(file.FileName);
                                    string ServerSavePath = Path.Combine(Server.MapPath("~/Asset/img/more-image/"), FileNameMore);
                                    //Save file to server folder  
                                    file.SaveAs(ServerSavePath);
                                    Infos.MoreImage += FileNameMore + ";";
                                }
                            }
                            // db.ProductInformations.Add(newInfor);
                            db.SaveChanges();

                        }
                    }
                    else
                    {
                        if (Products != null && Infos != null)
                        {
                            Products.ProductName = ProductName;
                            Products.Description = Description;
                            Products.ProductImage = Products.ProductImage;
                            Products.Price = Price;
                            Products.PriceOld = PriceOld;
                            Products.ImportPrice = ImportPrice;
                            Products.Manufacturer_id = Manufacturer_id;
                            Products.Amount = Amount;
                            Products.Category_id = Category_id;

                            //-------------info                        
                            Infos.ProductID = ProductID;
                            Infos.Brand = Brand;
                            Infos.Origin = Origin;
                            Infos.Type = Type;
                            Infos.Thickness = Thickness;
                            Infos.Shell_material = Shell_material;
                            Infos.Strap_material = Strap_material;
                            Infos.Glass_material = Glass_material;
                            Infos.Water_resistance = Water_resistance;
                            Infos.Sex = Sex;
                            Infos.MoreImage = Infos.MoreImage;
                        }
                        db.SaveChanges();
                    }
                }



                //    db.Entry(product).State = EntityState.Modified;
                //    db.Entry(ProInfo).State=EntityState.Modified;
                //    db.SaveChanges();
                //    return RedirectToAction("Index");
            }
            //ViewBag.ProductID = new SelectList(db.Manufacturers, "Manufacturer_id", "Name", product.ProductID);
            //ViewBag.ProductID = new SelectList(db.ProductInformations, "ProductID", "Brand", product.ProductID);
            return RedirectToAction("Index");
        }

        // GET: Admin/Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Admin/Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        public RedirectToRouteResult DeletePr(int id)
        {
            Product Deleteitem = db.Products.FirstOrDefault(c => c.ProductID == id);
            ProductInformation DeleteInfoitem = db.ProductInformations.FirstOrDefault(c => c.ProductID == id);
            //Cart Deleteitem = CartItem.FirstOrDefault(m => m.IdProduct == IdProduct);
            if (Deleteitem != null)
            {
                db.Products.Remove(Deleteitem);
                db.ProductInformations.Remove(DeleteInfoitem);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
