using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Project_497.Models;
using System.Globalization;
using PagedList;

namespace Project_new.Controllers
{
    public class productsDetailsController : Controller
    {
        private projectEntities3 db = new projectEntities3();
       



        // GET: productsDetails
        public ActionResult Index()
        {
            return View();
        }

        //------UPDATE THE CLICK OF A SPESIFIC PRODUCT----------

        public ActionResult Details(int? id)
        {
            product p = null;

            if (id != null)
           
            {
                p = db.products.Find(id);
                if (p == null)
                {
                    return HttpNotFound();
                }
                var count_status = db.Database.ExecuteSqlCommand("UPDATE [product] SET click = {0} WHERE productId={1}", p.click + 1, p.productId);
                var click_status = db.Database.ExecuteSqlCommand("UPDATE [product] SET clickDate = {0} WHERE productId={1}",p.clickDate=DateTime.Now.Date, p.productId);// lagbe na
                db.SaveChanges();
            }

            return View(p);

        }
        //------IN EVERY MONTH HOW MANY TIME A SPECIFIC PRODUCT HAS BEEN SEEL--------

        public ActionResult BuyProduct(int? id)
        {
            product q = null;
            Product_Sell r = new Product_Sell();
            q = db.products.Find(id);
           
            
            if (id != null)
            {

                if (q == null)
                {
                    return HttpNotFound();
                }
                else
                {
                    if (q.Buy > 0)   // buy use korchi koita product ase er jonno
                    {
                        var Buy_status = db.Database.ExecuteSqlCommand("UPDATE [product] SET Buy = {0}, count = {1},sellEndDate={2} WHERE productId={3}", q.Buy - 1, q.count + 1, q.sellEndDate = DateTime.Now, q.productId);
                        r.productId = q.productId;
                        r.productName = q.productName;
                        r.image = q.image;
                        r.sellEndDate = DateTime.Now;
                        r.price = q.price;
                        var pr =( (q.count * 100 )/ q.click );//ekhane count ta buy
                        //if(pr>100)
                        //{
                        //    pr = 100;
                        //}
                        var predect = db.Database.ExecuteSqlCommand("UPDATE [product] SET predection = {0} WHERE productId={1}", q.predection=pr, q.productId);//predection ta daynamic
                        db.Product_Sells.Add(r); // ja sell hocche seta table e add hocche

                       
                       
                        var date = DateTime.Now.Date.Day;
                        var notnow = DateTime.Now;
                        var monthName1 = notnow.ToString("MMMM");
                        if (monthName1 == "January")
                        {

                            var status = db.Database.ExecuteSqlCommand("UPDATE [product] SET January = {0} WHERE productId={1}", q.January + 1, q.productId);
                            
                            
                            
                            var pred = (((q.January * 31) / date) + q.December + q.October + q.November) / 4;
                            var mod = (((q.January * 31) / date) + q.December + q.October + q.November) % 4;
                            pred = pred + mod;
                            var st = db.Database.ExecuteSqlCommand("UPDATE [product] SET PredectionResult = {0} WHERE productId={1}", q.PredectionResult = pred, q.productId);
                            db.SaveChanges();
                            if (q.PredectionResult > q.Buy)
                            {
                                var pre = q.PredectionResult;
                                var b = q.Buy;
                                var result = pre - b;
                                var status1 = db.Database.ExecuteSqlCommand("UPDATE [product] SET Result = {0} WHERE productId={1}", q.Result = result, q.productId);
                            }
                            //db.products.Add(q);
                            db.SaveChanges();

                        }
                        else if (monthName1 == "February")
                        {

                            var status1 = db.Database.ExecuteSqlCommand("UPDATE [product] SET February = {0} WHERE productId={1}", q.February + 1, q.productId);
                            
                           
                            var pred = (((q.February * 28) / date) + q.January + q.December + q.November) / 4;
                            var mod = (((q.February * 28) / date) + q.January + q.December + q.November) % 4;
                            pred = pred + mod;
                            var st = db.Database.ExecuteSqlCommand("UPDATE [product] SET PredectionResult = {0} WHERE productId={1}", q.PredectionResult = pred, q.productId);
                            db.SaveChanges();
                            if (q.PredectionResult > q.Buy)
                            {
                                var pre = q.PredectionResult;
                                var b = q.Buy;
                                var result = pre - b;
                                var status
                                    = db.Database.ExecuteSqlCommand("UPDATE [product] SET Result = {0} WHERE productId={1}", q.Result = result, q.productId);
                               
                            }
                            //db.products.Add(q);
                            db.SaveChanges();

                        }
                        else if (monthName1 == "March")
                        {

                            var status1 = db.Database.ExecuteSqlCommand("UPDATE [product] SET March = {0} WHERE productId={1}", q.March + 1, q.productId);
                           
                            
                            var pred = (q.February + q.January + q.December + ((q.March * 31) / date)) / 4;
                            var mod = (q.February + q.January + q.December + ((q.March * 31) / date)) % 4;
                            pred = pred + mod;
                            var st = db.Database.ExecuteSqlCommand("UPDATE [product] SET PredectionResult = {0} WHERE productId={1}", q.PredectionResult = pred, q.productId);
                            db.SaveChanges();
                            if (q.PredectionResult > q.Buy)
                            {
                                var pre = q.PredectionResult;
                                var b = q.Buy;
                                var result = pre - b;
                                var status = db.Database.ExecuteSqlCommand("UPDATE [product] SET Result = {0} WHERE productId={1}", q.Result = result, q.productId);
                                 }

                            //db.products.Add(q);
                            db.SaveChanges();

                        }
                        else if (monthName1 == "April")
                        {
                            var status1 = db.Database.ExecuteSqlCommand("UPDATE [product] SET April = {0} WHERE productId={1}", q.April + 1, q.productId);
                           
                            var pred = (q.February + q.January + ((q.April * 30) / date) + q.March) / 4;
                            var mod = (q.February + q.January + ((q.April * 30) / date) + q.March) % 4;
                            pred = pred + mod;
                            var st = db.Database.ExecuteSqlCommand("UPDATE [product] SET PredectionResult = {0} WHERE productId={1}", q.PredectionResult = pred, q.productId);
                            db.SaveChanges();
                            if (q.PredectionResult > q.Buy)
                            {
                                var pre = q.PredectionResult;
                                var b = q.Buy;
                                var result = pre - b;
                                var status = db.Database.ExecuteSqlCommand("UPDATE [product] SET Result = {0} WHERE productId={1}", q.Result = result, q.productId);
                               
                            }
                            //db.products.Add(q);
                            db.SaveChanges();


                        }
                        else if (monthName1 == "May")
                        {

                            var status1 = db.Database.ExecuteSqlCommand("UPDATE [product] SET May = {0} WHERE productId={1}", q.May + 1, q.productId);
                           
                           
                            var pred = (q.February + ((q.May * 31) / date) + q.April + q.March) / 4;
                            var mod = (q.February + ((q.May * 31) / date) + q.April + q.March) % 4;
                            pred = pred + mod;
                            var st = db.Database.ExecuteSqlCommand("UPDATE [product] SET PredectionResult = {0} WHERE productId={1}", q.PredectionResult = pred, q.productId);
                            db.SaveChanges();
                            if (q.PredectionResult > q.Buy)
                            {
                                var pre = q.PredectionResult;
                                var b = q.Buy;
                                var result = pre - b;
                                var status = db.Database.ExecuteSqlCommand("UPDATE [product] SET Result = {0} WHERE productId={1}", q.Result = result, q.productId);
                            }
                            //db.products.Add(q);
                            db.SaveChanges();

                        }
                        else if (monthName1 == "June")
                        {

                            var status1 = db.Database.ExecuteSqlCommand("UPDATE [product] SET June = {0} WHERE productId={1}", q.June + 1, q.productId);
                           
                            var pred = (((q.June * 30) / date) + q.May + q.April + q.March) / 4;
                            var mod = (((q.June * 30) / date) + q.May + q.April + q.March) % 4;
                            pred = pred + mod;
                            var st = db.Database.ExecuteSqlCommand("UPDATE [product] SET PredectionResult = {0} WHERE productId={1}", q.PredectionResult = pred, q.productId);
                            db.SaveChanges();
                            if (q.PredectionResult > q.Buy)
                            {
                                var pre = q.PredectionResult;
                                var b = q.Buy;
                                var result = pre - b;
                                var status = db.Database.ExecuteSqlCommand("UPDATE [product] SET Result = {0} WHERE productId={1}", q.Result = result, q.productId);
                            }
                            //db.products.Add(q);
                            db.SaveChanges();


                        }
                        else if (monthName1 == "July")
                        {

                            var status1 = db.Database.ExecuteSqlCommand("UPDATE [product] SET November = {0} WHERE productId={1}", q.July + 1, q.productId);
                           
                            var pred = (((q.July * 31) / date) + q.May + q.April + q.July) / 4;
                            var mod = (((q.July * 31) / date) + q.May + q.April + q.July) % 4;
                            pred = pred + mod;
                            var st = db.Database.ExecuteSqlCommand("UPDATE [product] SET PredectionResult = {0} WHERE productId={1}", q.PredectionResult = pred, q.productId);
                            db.SaveChanges();
                            if (q.PredectionResult > q.Buy)
                            {
                                var pre = q.PredectionResult;
                                var b = q.Buy;
                                var result = pre - b;
                                var status = db.Database.ExecuteSqlCommand("UPDATE [product] SET Result = {0} WHERE productId={1}", q.Result = result, q.productId);
                               
                            }
                            //db.products.Add(q);
                            db.SaveChanges();


                        }
                        else if (monthName1 == "August")
                        {

                            var status1 = db.Database.ExecuteSqlCommand("UPDATE [product] SET August = {0} WHERE productId={1}", q.August + 1, q.productId);
                          
                            var pred = (q.June + q.May + ((q.August * 31) / date) + q.July) / 4;
                            var mod = (q.June + q.May + ((q.August * 31) / date) + q.July) % 4;
                            pred = pred + mod;
                            var st = db.Database.ExecuteSqlCommand("UPDATE [product] SET PredectionResult = {0} WHERE productId={1}", q.PredectionResult = pred, q.productId);
                            db.SaveChanges();
                            if (q.PredectionResult > q.Buy)
                            {
                                var pre = q.PredectionResult;
                                var b = q.Buy;
                                var result = pre - b;
                                var status = db.Database.ExecuteSqlCommand("UPDATE [product] SET Result = {0} WHERE productId={1}", q.Result = result, q.productId);
                               
                            }

                            //db.products.Add(q);
                            db.SaveChanges();

                        }
                        else if (monthName1 == "September")
                        {

                            var status1 = db.Database.ExecuteSqlCommand("UPDATE [product] SET September = {0} WHERE productId={1}", q.September + 1, q.productId);
                            
                           
                            var pred = (q.June + ((q.September * 30) / date) + q.August + q.July) / 4;
                            var mod = (q.June + ((q.September * 30) / date) + q.August + q.July) % 4;
                            pred = pred + mod;
                            var st = db.Database.ExecuteSqlCommand("UPDATE [product] SET PredectionResult = {0} WHERE productId={1}", q.PredectionResult = pred, q.productId);
                            db.SaveChanges();
                            if (q.PredectionResult > q.Buy)
                            {
                                var pre = q.PredectionResult;
                                var b = q.Buy;
                                var result = pre - b;
                                var status = db.Database.ExecuteSqlCommand("UPDATE [product] SET Result = {0} WHERE productId={1}", q.Result = result, q.productId);
                               
                            }
                            //db.products.Add(q);
                            db.SaveChanges();

                        }
                        else if (monthName1 == "October")
                        {

                            var status1 = db.Database.ExecuteSqlCommand("UPDATE [product] SET October = {0} WHERE productId={1}", q.October + 1, q.productId);
                           
                           
                            var pred = (((q.October * 31) / date) + q.September + q.August + q.July) / 4;
                            var mod = (((q.October * 31) / date) + q.September + q.August + q.July) % 4;
                            pred = pred + mod;
                            var st = db.Database.ExecuteSqlCommand("UPDATE [product] SET PredectionResult = {0} WHERE productId={1}", q.PredectionResult = pred, q.productId);
                            db.SaveChanges();
                            if (q.PredectionResult > q.Buy)
                            {
                                var pre = q.PredectionResult;
                                var b = q.Buy;
                                var result = pre - b;
                                var status = db.Database.ExecuteSqlCommand("UPDATE [product] SET Result = {0} WHERE productId={1}", q.Result = result, q.productId);
                              
                            }
                            //db.products.Add(q);
                            db.SaveChanges();

                        }
                        else if (monthName1 == "November")
                        {

                            var status1 = db.Database.ExecuteSqlCommand("UPDATE [product] SET November = {0} WHERE productId={1}", q.November + 1, q.productId);
                            db.SaveChanges();
                           
                            var pred = (q.August + q.September + q.October + ((q.November * 30) / date)) / 4;
                            var mod = (q.August + q.September + q.October + ((q.November * 30) / date)) % 4;
                            pred = pred + mod;
                            var st = db.Database.ExecuteSqlCommand("UPDATE [product] SET PredectionResult = {0} WHERE productId={1}", q.PredectionResult = pred, q.productId);
                            if (q.PredectionResult > q.Buy)
                            {
                                var pre = q.PredectionResult;
                                var b = q.Buy;
                                var result = pre - (b);
                                var status = db.Database.ExecuteSqlCommand("UPDATE [product] SET Result = {0} WHERE productId={1}", q.Result = result, q.productId);
                            
                            }
                            //db.products.Add(q);
                            db.SaveChanges();

                        }
                        else if (monthName1 == "December")
                        {

                            var status1 = db.Database.ExecuteSqlCommand("UPDATE [product] SET December = {0} WHERE productId={1}", q.December + 1, q.productId);
                            
                            
                            var pred = (((q.December * 31) / date) + q.September + q.October + q.November) / 4;
                            var mod = (((q.December * 31) / date) + q.September + q.October + q.November) % 4;
                            pred = pred + mod;
                            var st = db.Database.ExecuteSqlCommand("UPDATE [product] SET PredectionResult = {0} WHERE productId={1}", q.PredectionResult = pred, q.productId);
                            db.SaveChanges();
                            if (q.PredectionResult > q.Buy)
                            {
                                var pre = q.PredectionResult;
                                var b = q.Buy;
                                var result = pre - b;
                                var status = db.Database.ExecuteSqlCommand("UPDATE [product] SET Result = {0} WHERE productId={1}", q.Result = result, q.productId);
                               
                                
                            }
                            //db.products.Add(q);
                            db.SaveChanges();
                        }



                        db.SaveChanges();
                    }

                    else
                    {
                        return RedirectToAction("Index", "Product");
                    }
                }
                
                
            }
            return View(q);
        }


        public ActionResult PurchaseSuggestionForAdmin(int?page)
        {

            product p = new product();
            var suggestion = db.products.Where(x=>x.Result>0).OrderBy(x => x.productId).ToList().ToPagedList(page??1,20);
            return PartialView(suggestion);
        }

        //------SEARCH FOR A SPESIFIC PRODUCT FROM [MAIN SESRCH BAR]-------

        public ViewResult Search(string q)
        {
            var persons = from p in db.products select p;
            if (!string.IsNullOrWhiteSpace(q))
            {
                persons = persons.Where(p => p.productName.Contains(q));
            }
            return View(persons);
        }


        //public ActionResult DailySell()
        //{
        //    Product_Sell sell = new Product_Sell();


        //    if (sell.sellEndDate == DateTime.Today)

        //    {
        //         list = db.Product_Sells.OrderBy(x => x.sellEndDate).ToList();
        //    }

        //    return View(list);
        //}



        public ActionResult DailySell(int?page)
        {
            Product_Sell sell = new Product_Sell();
            var list = db.Product_Sells.OrderByDescending(x => x.sellEndDate).ToList().ToPagedList(page?? 1,50);
            return View(list);
        }



        public ActionResult predections()
        {
            product p = new product();
            var list = db.products.Where(x=>x.predection>=50).OrderByDescending(x => x.predection).ToList();
            return View(list);
        }



        




















    //public ActionResult Need( )
    //{
    //    product p = new product();

    //    return View();


    //    //List<product> cnt = db.products.OrderByDescending(x => x.count).ToList<product>();
    //    //List<product> by = db.products.OrderByDescending(x => x.Buy).ToList<product>();



    //    //foreach (product z in cnt)
    //    //{
    //    //    foreach (product p in by)
    //    //    {
    //    //        if (z.productId == p.productId)
    //    //        {
    //    //            if(p.count>= z.Buy)
    //    //            {
    //    //                int Buy_status = db.Database.ExecuteSqlCommand("UPDATE [product] SET Need = {0} WHERE productId={1}",p.Buy, p.productId);
    //    //            }
    //    //            else if(p.Buy>=z.count)
    //    //            {

    //    //                int Buy_status = db.Database.ExecuteSqlCommand("UPDATE [product] SET Need = {0} WHERE productId={1}", p.count , p.productId);
    //    //            }
    //    //            break;

    //    //        }
    //    //        db.SaveChanges();
    //    //    }
    //    //}
    //    //return View();
    //}



    //public ActionResult Search(string searchString)
    //{
    //    var srch = from m in db.products
    //                 select m;
    //    if (!String.IsNullOrEmpty(searchString))
    //    {
    //         srch = db.products.Where(x => x.productName == searchString);

    //    }
    //    return View(srch);
    //}

}
}

//Product_Sells.Include(m => m.productId).Include(m => m.productName == "HRIDOy").Include(m => m.image).Include(m => m.sellEndDate).Include(m => m.price);
// var Status = db.Database.ExecuteSqlCommand("INSERT INTO [ Product_Sell](productId,productName,image,sellEndDate,price) VALUES({0},{1},{2},{3},{4})", r.productId = q.productId, r.productName = q.productName, r.image = q.image, r.sellEndDate = DateTime.Now, r.price = 10000);

//string query = "INSERT INTO page_counter (month, year, page_count) VALUES (@Month, @Year, @PageCount)";

    /////////////////////////////////////////////////////////////////////////////////////
//private int GetMonthDifference(DateTime now, DateTime past)
//{
//    throw new NotImplementedException();
//}
//p.productId = q.productId;
//                p.productName = q.productName;
//                p.image = q.image;
//                string date1 = null;
//DateTime dt1 = DateTime.ParseExact(date1, "dd-MM-yyyy", null);
//string date2 = null;
//DateTime dt2 = DateTime.ParseExact(date2, "dd-MM-yyyy", null);

//int cmp = dt1.CompareTo(dt2);

//                if (cmp > 0)
//                {
//                    // date1 is greater means date1 is comes after date2
//                }
//                else if (cmp< 0)
//                {
//                    // date2 is greater means date1 is comes after date1
//                }
//                else
//                {
//                    // date1 is same as date2
//                }
//                DateTime now = DateTime.UtcNow;
//DateTime past = now.AddMonths(-13);
//int monthDiff = GetMonthDifference(now, past);