using ShopWatch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopWatch.Areas.Admin.Controllers
{
    [SessionAuthorize]
    public class BaseController : Controller
    {
        // GET: Admin/Base
       
        public ShopEntities db = new ShopEntities();

    }
}