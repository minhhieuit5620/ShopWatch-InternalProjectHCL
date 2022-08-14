using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.Azure.ActiveDirectory.GraphClient;
using PagedList;
using ShopWatch.Models;
using System.IO;
using OfficeOpenXml;
using Excel = Microsoft.Office.Interop.Excel;

namespace ShopWatch.Areas.Admin.Controllers
{
    public class OrdersController : Controller
    {
        private ShopEntities db = new ShopEntities();

        // GET: Admin/Orders
        public ActionResult Index(int? page)
        {
            List<Order> orders = db.Orders.Include(o => o.User).ToList();
            return View(orders.ToPagedList(page ?? 1, 8));
            //List<Product> products = db.Products.Include(p => p.Manufacturer).Include(p => p.ProductInformation).ToList();
            //return View(products.ToPagedList(page ?? 1, 8));
        }

        // GET: Admin/Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: Admin/Orders/Create
        public ActionResult Create()
        {
            ViewBag.UserID = new SelectList(db.Users, "UserID", "UserName");
            return View();
        }

        // POST: Admin/Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OrderID,UserID,StaffID,OrderName,OrderEmail,OrderPhone,OrderAddress,OrderNote,TotalMoney,OrderDate,Status,Payment_method")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Orders.Add(order);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserID = new SelectList(db.Users, "UserID", "UserName", order.UserID);
            return View(order);
        }

        // GET: Admin/Orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserID = new SelectList(db.Users, "UserID", "UserName", order.UserID);
            return View(order);
        }

        public ActionResult ExportEX(string path)
        {
            
            List<Order> orders = db.Orders.ToList();
            Excel.Application application = new Excel.Application();
            application.Application.Workbooks.Add(Type.Missing);

            for (int i = 0; i < orders.Count(); i++)
            {
                application.Cells[1, i + 1] = orders[i];
            }
            for (int i = 0; i < orders.Count(); i++)
            {

            }
            application.Columns.AutoFit();
            application.ActiveWorkbook.SaveCopyAs(path);
            application.ActiveWorkbook.Saved=true;
            return RedirectToAction("Index(1)");
        //    try
        //    {
        //        //Saving the workbook to disk.
        //        if (path == "Xls")
        //        {
        //            //Save as .xls format
        //            worknp.SaveAs("SpreadSheet.xls", HttpContext.ApplicationInstance.Response, ExcelDownloadType.PromptDialog, ExcelHttpContentType.Excel97);
        //        }
        //        //Save as .xlsx format
        //        else
        //        {
        //            workbook.Version = ExcelVersion.Excel2016;
        //            workbook.SaveAs("SpreadSheet.xlsx", HttpContext.ApplicationInstance.Response, ExcelDownloadType.PromptDialog, ExcelHttpContentType.Excel2016);
        //        }
        //    }
        //    catch (Exception)
        //    {
        //    }

        //    //Close the workbook.
        //    workbook.Close();
        //    excelEngine.Dispose();
        //    return View();
        //}

            }



        // POST: Admin/Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
            [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrderID,UserID,StaffID,OrderName,OrderEmail,OrderPhone,OrderAddress,OrderNote,TotalMoney,OrderDate,Status,Payment_method")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserID = new SelectList(db.Users, "UserID", "UserName", order.UserID);
            return View(order);
        }

        // GET: Admin/Orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Admin/Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = db.Orders.Find(id);
            db.Orders.Remove(order);
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
    }
}
