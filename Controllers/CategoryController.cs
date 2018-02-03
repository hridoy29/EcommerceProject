using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project_497.Models;

namespace Project_497.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
        public ActionResult Index()
        {
            return View();
        }
        projectEntities3 db = new projectEntities3();

        public PartialViewResult Categorypartial()
        {
            var CategoryList = db.categories.OrderBy(x => x.name).ToList();
            return PartialView(CategoryList);

        }
    }
}