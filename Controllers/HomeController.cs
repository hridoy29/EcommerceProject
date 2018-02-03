using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project_497.Controllers
{
    public class HomeController : Controller
    {
        // GET: HomeNew
        public ActionResult Index()
        {
            //ViewBag.name = "sfsdfasdfasdf"; 
            return View();
        }
    }
}