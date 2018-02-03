using Microsoft.Ajax.Utilities;
using PagedList;
using Project_497.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Project_497.Controllers
{
    public class ProductController : Controller
    {
        projectEntities3 db = new projectEntities3();
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }
        //// /////////////------Sell OF The Product in [Spesific MONTH]------------/////////////////////////


        public ActionResult Need(int? page)
        {
            
            var improve = db.products.OrderBy(x => x.productId).ToList().ToPagedList(page??1,10);
            return View(improve);

          
        }



        ///////////////////-------[Search For Product]----------/////////////////////////////         
        public ViewResult Search(string q)
        {
            //var persons = from p in db.products select p;           
            if (!string.IsNullOrWhiteSpace(q))
            {
               var persons = db.products.Where(p => p.productName.Contains(q));
                return View(persons);
            }
            return View();
        }

        ///////////////////-------[Search For Dailysell update]----------/////////////////////////////

        public ViewResult Search_for_Daily_sell(string q)
        {
            var persons = from p in db.Product_Sells select p;
            if (!string.IsNullOrWhiteSpace(q))
            {
                persons = persons.Where(p => p.productName.Contains(q));
            }
            return View(persons);

        }
        ///////////////////////----Mix Low Click Product Into The 1st page----///////////////////////
        
        public PartialViewResult View(int? page, int? category)
        {

            var PageNumber = page ?? 1; //?? null colesen paramiter if it's null it will take 1st page other wise it will take the current page number
            var PageSize = 12;      //page a product koita rakhbo
            int middle = 0;
            int top = 0;
            var t = db.products.OrderByDescending(x => x.click).Take(1);
            Percentage per = new Percentage();           
            var H = db.Percentages.Where(p => p.percentageId == 1).Select(x => x.percentage1);
            var temp = H.First();
            int perc = (temp * PageSize) / 100;

            foreach (product p in t)
            {
                top = (int)p.click;
                middle = (int)(p.click) / 2;
                break;
            }
            if (category != null)
            {
                ViewBag.category = category;
                List<product> lowest = db.products.OrderBy(x => x.click).Take(perc).ToList<product>(); //aikhan theke perc ar theke j pabo sei koita product rakhbo 
                List<product> highest = db.products.OrderByDescending(x => x.click).ToList<product>(); // sb product rakhbo aitar 



                foreach (product z in lowest)
                {
                    foreach (product p in highest)
                    {
                        if (p.productId == z.productId)
                        {
                            Random r = new Random();

                            int i = r.Next(highest.Count / 2);
                            int j = highest.IndexOf(p);   //lowest clicked  product er index.....
                            for (int k = j; k > i; k--)
                            {
                                highest[k] = highest[k - 1];
                            }
                            highest[i] = z;
                            break;
                        }

                    }
                }
                var ProductList = highest.Where(x => x.categoryId == category).ToPagedList(PageNumber, PageSize);
                return PartialView(ProductList);
            }

            else

            {
                {
                   
                    List<product> lowest = db.products.OrderBy(x => x.click).Take(perc).ToList<product>();
                    List<product> highest = db.products.OrderByDescending(x => x.click).ToList<product>();



                    foreach (product z in lowest)
                    {
                        foreach (product p in highest)
                        {
                            if (p.productId == z.productId)
                            {
                                Random r = new Random();

                                int i =r.Next(highest.Count / 2);
                                int j = highest.IndexOf(p);   //lowest clicked  product er index.....
                                for (int k = j; k > i; k--)
                                {
                                    highest[k] = highest[k - 1];
                                }
                                highest[i] = z;  
                                break;
                            }

                        }
                    }
                    //var click_list = db.products.Select(x => x.click);

                    var ProductList = highest.ToPagedList(PageNumber, PageSize);
                    return PartialView(ProductList);

                }
            }

        }
        ////////////////////--------SEE HOW MUCH  Clik Get The Product---------///////////////////////////////////         
        public ActionResult Improve()
        {
            var improve1 = db.Click_Differences.OrderBy(x => x.productId).ToList();
            product p = new product();
            foreach (var item in improve1)
            {

                var count_status = db.Database.ExecuteSqlCommand("UPDATE [product] SET previous_click = {0} WHERE productId={1}", p.previous_click = item.click, p.productId = item.productId);
                db.SaveChanges();
            }
            db.SaveChanges();
            var click_list = db.products.OrderBy(x => x.productId).ToList();
            foreach (var item in click_list)
            {
                
                var count_status = db.Database.ExecuteSqlCommand("UPDATE [product] SET count_improvment = {0} WHERE productId={1}", p.count_improvment = item.click - item.previous_click, p.productId = item.productId);
                db.SaveChanges();
            }
            var list = db.products.OrderBy(x => x.previous_click).ToList();
            return View(list);
            





        }
   
    }
}




//product pr = new product();
//Percentage per = new Percentage();
//var H = db.Percentages.Where(p => p.percentageId == 1).Select(x => x.date);
//var temp = H.First();
//var improve = db.products.Where(x=>x.clickDate==temp).OrderBy(x => x.productId).ToList();
//var improve1 = db.products.OrderBy(x => x.productId).ToList();

//foreach (var item in improve)
//{


//    {
//        Click_Difference c = new Click_Difference();
//        c.productId = item.productId;
//        c.ProductName = item.productName;
//        c.click = item.click;
//        c.Cdate = item.clickDate;
//        db.Click_Differences.Add(c);
//        db.SaveChanges();
//    }
//}
//var q = (from pd in db.products
//         join od in db.Click_Differences on pd.productId equals od.productId
//         orderby od.click
//         select new
//         {
//             ID= od.productId,
//             Name= pd.productName,
//             UpadatedClick= pd.click,
//             PreviousClick= od.click
//            }).ToList();




//foreach (var item in improve1)
//            {


//                {
//                    Click_Difference c = new Click_Difference();


//c.productId = item.productId;
//                    c.ProductName = item.productName;
//                    c.updatedClick = item.click;
//                    c.Cdate = item.clickDate;
//                    db.Click_Differences.Add(c);
//                    db.SaveChanges();
//                }
//            }




//////////////////////////////---------------- Purchase Suggestion For BUY-------------//////////////////////////////////

//public ActionResult PurchaseSuggestion()
//{
//    var improve = db.products.OrderBy(x => x.productId).ToList();
//    return View(improve);
//}
//public ActionResult PurchaseSuggestionForAdmin()
//{
//product p = new product();


//         {
//            var now = DateTime.Now;
//            var monthName = now.ToString("MMMM");
//            var date = DateTime.Now.Date.Day;
//            if (monthName == "November")
//            {
//                 var pred = (p.August + p.September + p.October +( (p.November*30)/date)) / 4;
//                 var mod=(p.August + p.September + p.October + ((p.November * 30) / date)) % 4;
//                 pred = pred + mod;
//                 var st = db.Database.ExecuteSqlCommand("UPDATE [product] SET PredectionResult = {0} WHERE productId={1}", p.PredectionResult = pred, p.productId);
//                db.SaveChanges();
//                if (p.PredectionResult > p.Buy)
//                {
//                    var pre = p.PredectionResult;
//                    var b = p.Buy;
//                    var result = pre - b;
//                    var status = db.Database.ExecuteSqlCommand("UPDATE [product] SET Result = {0} WHERE productId={1}", p.Result = result, p.productId);
//                    db.SaveChanges();
//                }
//                var res = db.products.Select(x=>x.Result);
//                return PartialView(res);

//            }
//              else if (monthName == "December")
//                   {
//                         var pred = (((p.December * 30) / date) + p.September + p.October + p.November) / 4;
//                         var mod = (((p.December * 30) / date) + p.September + p.October + p.November) % 4;
//                         pred = pred + mod;
//                         var st = db.Database.ExecuteSqlCommand("UPDATE [product] SET PredectionResult = {0} WHERE productId={1}", p.PredectionResult = pred, p.productId);
//                         db.SaveChanges();
//                         if (p.PredectionResult > p.Buy)
//                            {
//                              var pre = p.PredectionResult;
//                              var b = p.Buy;
//                              var result = pre - b;
//                              var status = db.Database.ExecuteSqlCommand("UPDATE [product] SET Result = {0} WHERE productId={1}", p.Result = result, p.productId);
//                              db.SaveChanges();
//                            }
//        var res = db.products.Select(x => x.Result);
//        return PartialView(res);

//    }
//             else  if (monthName == "January")
//                {
//                    var pred = (((p.January * 30) / date) + p.December + p.October + p.November) / 4;
//                    var mod = (((p.January * 30) / date) + p.December + p.October + p.November) % 4;
//                    pred = pred + mod;
//                    var st = db.Database.ExecuteSqlCommand("UPDATE [product] SET PredectionResult = {0} WHERE productId={1}", p.PredectionResult = pred, p.productId);
//                    db.SaveChanges();
//                    if (p.PredectionResult > p.Buy)
//                      {
//                       var pre = p.PredectionResult;
//                       var b = p.Buy;
//                       var result = pre - b;
//                       var status = db.Database.ExecuteSqlCommand("UPDATE [product] SET Result = {0} WHERE productId={1}", p.Result = result, p.productId);
//                       db.SaveChanges();
//                      }
//        var res = db.products.Select(x => x.Result);
//        return PartialView(res);

//    }
//            else  if (monthName == "February")
//                {
//                    var pred = (((p.February * 30) / date) + p.January +p.December + p.November) / 4;
//                    var mod = (((p.February * 30) / date) + p.January + p.December + p.November) % 4;
//                    pred = pred + mod;
//                    var st = db.Database.ExecuteSqlCommand("UPDATE [product] SET PredectionResult = {0} WHERE productId={1}", p.PredectionResult = pred, p.productId);
//                    db.SaveChanges();
//                  if (p.PredectionResult > p.Buy)
//                     {
//                        var pre = p.PredectionResult;
//                        var b = p.Buy;
//                        var result = pre - b;
//                        var status = db.Database.ExecuteSqlCommand("UPDATE [product] SET Result = {0} WHERE productId={1}", p.Result = result, p.productId);
//                        db.SaveChanges();
//                     }
//        var res = db.products.Select(x => x.Result);
//        return PartialView(res);

//    }
//             else if (monthName == "March")
//                {
//                  var pred = (p.February + p.January + p.December + ((p.March * 30) / date)) / 4;
//                  var mod = (p.February + p.January + p.December + ((p.March * 30) / date)) % 4;
//                  pred = pred + mod;
//                  var st = db.Database.ExecuteSqlCommand("UPDATE [product] SET PredectionResult = {0} WHERE productId={1}", p.PredectionResult = pred, p.productId);
//                  db.SaveChanges();
//                  if (p.PredectionResult > p.Buy)
//                      {
//                         var pre = p.PredectionResult;
//                         var b = p.Buy;
//                         var result = pre - b;
//                         var status = db.Database.ExecuteSqlCommand("UPDATE [product] SET Result = {0} WHERE productId={1}", p.Result = result, p.productId);
//                        db.SaveChanges();
//                      }
//        var res = db.products.Select(x => x.Result);
//        return PartialView(res);

//    }
//            else   if (monthName == "April")
//                {
//                     var pred = (p.February + p.January + ((p.April * 30) / date) + p.March) / 4;
//                     var mod = (p.February + p.January + ((p.April * 30) / date) + p.March) % 4;
//                     pred = pred + mod;
//                     var st = db.Database.ExecuteSqlCommand("UPDATE [product] SET PredectionResult = {0} WHERE productId={1}", p.PredectionResult = pred, p.productId);
//                     db.SaveChanges();
//                     if (p.PredectionResult > p.Buy)
//                        {
//                            var pre = p.PredectionResult;
//                            var b = p.Buy;
//                            var result = pre - b;
//                            var status = db.Database.ExecuteSqlCommand("UPDATE [product] SET Result = {0} WHERE productId={1}", p.Result = result, p.productId);
//                            db.SaveChanges();
//                        }
//        var res = db.products.Select(x => x.Result);
//        return PartialView(res);

//    }
//            else if (monthName == "May")
//                {
//                   var pred = (p.February + ((p.May * 30) / date) + p.April + p.March) / 4;
//                   var mod = (p.February + ((p.May * 30) / date) + p.April + p.March) % 4;
//                   pred = pred + mod;
//                   var st = db.Database.ExecuteSqlCommand("UPDATE [product] SET PredectionResult = {0} WHERE productId={1}", p.PredectionResult = pred, p.productId);
//                   db.SaveChanges();
//                   if (p.PredectionResult > p.Buy)
//                      {
//                          var pre = p.PredectionResult;
//                          var b = p.Buy;
//                          var result = pre - b;
//                          var status = db.Database.ExecuteSqlCommand("UPDATE [product] SET Result = {0} WHERE productId={1}", p.Result = result, p.productId);
//                          db.SaveChanges();
//                      }
//        var res = db.products.Select(x => x.Result);
//        return PartialView(res);

//    }
//             else if (monthName == "June")
//                {
//                   var pred = (((p.June * 30) / date) + p.May + p.April + p.March) / 4;
//                   var mod = (((p.June * 30) / date) + p.May + p.April + p.March) % 4;
//                   pred = pred + mod;
//                   var st = db.Database.ExecuteSqlCommand("UPDATE [product] SET PredectionResult = {0} WHERE productId={1}", p.PredectionResult = pred, p.productId);
//                   db.SaveChanges();
//                     if (p.PredectionResult > p.Buy)
//                         {
//                            var pre = p.PredectionResult;
//                            var b = p.Buy;
//                            var result = pre - b;
//                            var status = db.Database.ExecuteSqlCommand("UPDATE [product] SET Result = {0} WHERE productId={1}", p.Result = result, p.productId);
//                            db.SaveChanges();
//                         }
//        var res = db.products.Select(x => x.Result);
//        return PartialView(res);

//    }
//            else if (monthName == "July")
//                {
//                   var pred = (((p.July * 30) / date) + p.May + p.April + p.July) / 4;
//                   var mod = (((p.July * 30) / date) + p.May + p.April + p.July) % 4;
//                   pred = pred + mod;
//                   var st = db.Database.ExecuteSqlCommand("UPDATE [product] SET PredectionResult = {0} WHERE productId={1}", p.PredectionResult = pred, p.productId);
//                   db.SaveChanges();
//                        if (p.PredectionResult > p.Buy)
//                         {
//                             var pre = p.PredectionResult;
//                             var b = p.Buy;
//                             var result = pre - b;
//                              var status = db.Database.ExecuteSqlCommand("UPDATE [product] SET Result = {0} WHERE productId={1}", p.Result = result, p.productId);
//                             db.SaveChanges();
//                         }
//        var res = db.products.Select(x => x.Result);
//        return PartialView(res);
//    }
//              else  if (monthName == "August")
//                 {
//                     var pred = (p.June + p.May + ((p.August * 30) / date) + p.July) / 4;
//                     var mod = (p.June + p.May + ((p.August * 30) / date) + p.July) % 4;
//                     pred = pred + mod;
//                     var st = db.Database.ExecuteSqlCommand("UPDATE [product] SET PredectionResult = {0} WHERE productId={1}", p.PredectionResult = pred, p.productId);
//                     db.SaveChanges();
//                      if (p.PredectionResult > p.Buy)
//                             {
//                                  var pre = p.PredectionResult;
//                                  var b = p.Buy;
//                                  var result = pre - b;
//                                  var status = db.Database.ExecuteSqlCommand("UPDATE [product] SET Result = {0} WHERE productId={1}", p.Result = result, p.productId);
//                                  db.SaveChanges();
//                             }
//        var res = db.products.Select(x => x.Result);
//        return PartialView(res);

//    }
//              else  if (monthName == "Septembe")
//                   {
//                      var pred = (p.June + ((p.September * 30) / date) + p.August + p.July) / 4;
//                      var mod = (p.June + ((p.September * 30) / date) + p.August + p.July) % 4;
//                      pred = pred + mod;
//                      var st = db.Database.ExecuteSqlCommand("UPDATE [product] SET PredectionResult = {0} WHERE productId={1}", p.PredectionResult = pred, p.productId);
//                      db.SaveChanges();
//                      if (p.PredectionResult > p.Buy)
//                          {
//                              var pre = p.PredectionResult;
//                              var b = p.Buy;
//                              var result = pre - b;
//                              var status = db.Database.ExecuteSqlCommand("UPDATE [product] SET Result = {0} WHERE productId={1}", p.Result = result, p.productId);
//                             db.SaveChanges();
//                          }
//        var res = db.products.Select(x => x.Result);
//        return PartialView(res);

//    }
//              else if (monthName == "October")
//                {
//                     var pred = (((p.October * 30) / date) + p.September + p.August + p.July) / 4;
//                     var mod = (((p.October * 30) / date) + p.September + p.August + p.July) % 4;
//                     pred = pred + mod;
//                      var st = db.Database.ExecuteSqlCommand("UPDATE [product] SET PredectionResult = {0} WHERE productId={1}", p.PredectionResult = pred, p.productId);
//                      db.SaveChanges();
//                      if (p.PredectionResult > p.Buy)
//                      {
//                         var pre = p.PredectionResult;
//                         var b = p.Buy;
//                         var result = pre - b;
//                         var status = db.Database.ExecuteSqlCommand("UPDATE [product] SET Result = {0} WHERE productId={1}", p.Result = result, p.productId);
//                         db.SaveChanges();
//                      }
//        var res = db.products.Select(x => x.Result);
//        return PartialView(res);

//    }

//         }
//        return View(p);

//}


// else
//            {
//                ViewBag.category = category;
//                List<product> lowest = db.products.OrderBy(x => x.click).Take(perc).ToList<product>();
//List<product> highest = db.products.OrderByDescending(x => x.click).ToList<product>();



//                foreach (product z in lowest)
//                {
//                    foreach (product p in highest)
//                    {
//                        if (p.productId == z.productId)
//                        {
//                            Random r = new Random();
//int i = r.Next(highest.Count / 2);
//int j = highest.IndexOf(p);
//                            for (int k = j; k > i; k--)
//                            {
//                                highest[k] = highest[k - 1];
//                            }
//                            highest[i] = z;
//                            break;
//                        }

//                    }
//                }
//                int[] number = new int[30];
//int u = 0;
//                while (u != 30)
//                {
//                    number[u] = (int)highest[u].click;
//                    u++;
//                }
//                var ProductList = highest.ToPagedList(PageNumber, PageSize);
//                return PartialView(ProductList);
//            }

//        }
//    }
//}
//if (category != null)
//{

//    ///For get product throug category List
//    ViewBag.category = category;

//    var ProductList = db.products.OrderByDescending(x => x.click >= 1 && x.click >= middle).Where(x => x.categoryId == category).ToPagedList(PageNumber, PageSize);
//    return PartialView(ProductList);
//}
//        public ActionResult RegisterClick(product model)
//        {
//            model.RegisterClick();
//            return Json(model);
//        }
//        public ActionResult ResetClicks(product model)
//        {
//            model.ResetClicks();
//            return Json(model);
//        }

//        / FOR Communicate with data base ....
//        /*public PartialViewResult ProductListPartial()
//        {


//            var ProductList = db.products.OrderByDescending(x => x.categoryId).ToList();

//            return PartialView(ProductList);
//        }*/

//        db.Persentages.Attach(Persentage); // State = Unchanged
//        db.Persentages= ; // State = Modified, and only the FirstName property is dirty.
//        db.SaveChanges();
//        db.Entry(per).State = EntityState.Modified;
//        db.SaveChanges();
//        var number = db.Database.ExecuteSqlCommand("INSERT INTO [Persentage] VALUES [persentage] = {0} ", per.persentage1);
//         db.SaveChanges();
//          var data=int.Parse(Number);
//    }

//}

