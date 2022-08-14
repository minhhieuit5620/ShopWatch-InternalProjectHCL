using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopWatch.Models;
namespace ShopWatch.Controllers
{
    public class BaseController : Controller
    {
        // GET: Base
        public ShopEntities db = new ShopEntities() ;
       
    }
}