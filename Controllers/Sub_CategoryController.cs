using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project_497.Models;

namespace Project_497.Controllers
{
    public class Sub_CategoryController : Controller
    {
        projectEntities3 db = new projectEntities3();
        // GET: Sub_Category
        public ActionResult Index()
        {
            return View();
        }
        public PartialViewResult SubCategorypartial()
        {
            var CategoryList = db.SubCategoryIDs.OrderBy(x => x.name).ToList();
            return PartialView(CategoryList);

        }
    }
}