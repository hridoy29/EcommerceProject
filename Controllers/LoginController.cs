using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project_497.Models;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace Project_new.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        projectEntities3 db = new projectEntities3();
        [HttpPost]
        public ActionResult LoginAuthentication(Admin id)
        {
            var UserDetails = db.Admins.Where(x => x.userName == id.userName && x.password == id.password).FirstOrDefault();
            if (UserDetails == null)
            {

                id.LoginErrorMessage = "User Name or Password is not valid";
                return View("Index", id);
            }
            else
            {
                Session["userId"] = id.userId;
                Session["userName"] = id.userName;
                return RedirectToAction("Index", "Admin");
            }


        }
        public ActionResult Logout()
        {
            int userId = (int)Session["userId"];
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }


    }
}                //var Status = db.Database.ExecuteSqlCommand("INSERT INTO [ Product_Sell](productId,productName,image,sellEndDate,price) VALUES({0},{1},{2},{3},{4})", r.productId = q.productId, r.productName = q.productName, r.image = q.image, r.sellEndDate, r.price = 10000);
