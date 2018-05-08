using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using DataAccess;
using _BudgetManager.Models;
using BusinessLayer;
using DataAccess.Models;

namespace _BudgetManager.Controllers
{
    //Postavljanje Autorizacije
    [Authorize]
    public class ArticleController : Controller
    {

        [AllowAnonymous]
        public ActionResult Index()
        {

            var data = DataLayer.GetView(); 

            //Oslobađanje memorije 
           
            return View(data);
            //return View();
        }

        [HttpGet]
        [Route("Article/Min/{s:maxlength(3)?}")]
        [AllowAnonymous]
        public ActionResult Min(string s )
        {
            var data = BusinessLayer.BusinessLayer.GetSmalliest(s);
            if (data == null)
                return RedirectToAction("Index");

            return View("Details", data);
        }

        //    article/max
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Max(string s )
        {
            var data = BusinessLayer.BusinessLayer.GetMax(s);
            if (data == null)
                return RedirectToAction("Index");

            return View("Details", data);
        }


        [AllowAnonymous]
        public ActionResult Convert(int Id)
        {
            decimal d = BusinessLayer.BusinessLayer.Convert(Id);

            string value = String.Format("{0:0.00}", d);
            ViewBag.Value = value;
            ViewBag.Hrk = "HRK";
           
            return View();
        }



        [AllowAnonymous]
        public ActionResult Details(int? id = null)
        {
            if (id == null)
                return View();
    
            var model = DataLayer.GetArticleId(id);

                return View(model);
        }



        [HttpPost]
        [AllowAnonymous]
        public ActionResult IndexAjax(ArticleFilterModel filter)
        {
            //Kreiranje konteksta 
            try
            {
                var dataQuery = DataLayer.GetArticleFilter(filter);
                return PartialView("_IndexTable", dataQuery);

            }
            
            catch
            {
                return Redirect("Index");
            }
                
        }


        [AllowAnonymous]
        public ActionResult FilterForm()
        {
            return PartialView("_ArticleFilter");
        }


        // GET: Article/Create

        public ActionResult Create()
            {
                this.FillDropDownValuesCategory();
                this.FillDropDownValuesCurrency();
                this.FillDropDownValuesTypeOfPay();

                return View();
            }



        // POST: Article/Create
        [HttpPost]
        [ActionName("Create")]
        [Authorize(Roles = "Admin")]
        public ActionResult CreatePost(Article model)
        {
            try
            {
                if (ModelState.IsValid)

                {
                    
                    DataLayer.CreateModel(model);       

                    return RedirectToAction("Index");
                    //return PartialView("PayView");
                }
                else
                {
                    this.FillDropDownValuesCategory();
                    this.FillDropDownValuesCurrency();
                    this.FillDropDownValuesTypeOfPay();

                    return View();
                }
            }
            catch
            {
                this.FillDropDownValuesCategory();
                this.FillDropDownValuesCurrency();
                this.FillDropDownValuesTypeOfPay();
                return View();
            }
        }



        // GET: Article/Edit/5
      
        public ActionResult Edit(int? id)
        {
            this.FillDropDownValuesCategory();
            this.FillDropDownValuesCurrency();
            this.FillDropDownValuesTypeOfPay();

            //Kreiranje konteksta 
            if (id.HasValue && ModelState.IsValid)
            {
              //  var context = new BudgetManagerDbContext();
                var model = DataLayer.GetArticleId(id);
               // context.Dispose();
                 return View(model);
            }

            else
            {
                return View();
            }
        }


        // POST: Article/Edit/5
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id, Article model)
        {
            if (ModelState.IsValid)
            {
                bool didUpdateModelSucceed = DataLayer.EditArticle(id, model);
                if (didUpdateModelSucceed)
                {

                    return RedirectToAction("Index");
                }
            }
            this.FillDropDownValuesCategory();
            this.FillDropDownValuesCurrency();
            this.FillDropDownValuesTypeOfPay();

            return View(model);
        }




        // GET: Article/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {

            return View();
        }

        // POST: Article/Delete/5
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public JsonResult Delete(int? id)
        {
            bool delete = DataLayer.DeleteArticle(id);
                if(delete)
                 return Json("OK", JsonRequestBehavior.AllowGet);
            
                   return Json(false, JsonRequestBehavior.AllowGet);
        }




        //drop down category
        private void FillDropDownValuesCategory()
        {
          

            var Categorys = DataLayer.GetListCategory();

            var selectItems = new List<System.Web.Mvc.SelectListItem>();

            //Polje je opcionalno
            var listItem = new SelectListItem();
            //listItem.Text = "- odaberite -";
            //listItem.Value = "";
            //selectItems.Add(listItem);

            foreach (var c in Categorys)
            {
                listItem = new SelectListItem();
                listItem.Text = c.Name;
                listItem.Value = c.Id.ToString();
                listItem.Selected = false;
                selectItems.Add(listItem);
            }
          //  context.Dispose();
            ViewBag.PossibleCategory = selectItems;
        }


        //drop down typeofpay
        private void FillDropDownValuesTypeOfPay()
        {
            //Kreiranje konteksta 
         

            var TypeofPay = DataLayer.GetListTypeOfPay();

            var selectItems = new List<System.Web.Mvc.SelectListItem>();

            //Polje je opcionalno
            var listItem = new SelectListItem();
            //listItem.Text = "- odaberite -";
            //listItem.Value = "";
            //selectItems.Add(listItem);

            foreach (var pay in TypeofPay)
            {
                listItem = new SelectListItem();
                listItem.Text = pay.Name;
                listItem.Value = pay.Id.ToString();
                listItem.Selected = false;
                selectItems.Add(listItem);
            }
        
            ViewBag.PossibleTypeOfPay = selectItems;
        }



        //drop down typeofpay
        private void FillDropDownValuesCurrency()
        {

            var Currencys = DataLayer.GetListCirrency();

            var selectItems = new List<System.Web.Mvc.SelectListItem>();

            //Polje je opcionalno
            var listItem = new SelectListItem();
            //listItem.Text = "- HRK -";
            //listItem.Value = "";
            //selectItems.Add(listItem);

            foreach (var currency in Currencys)
            {
                listItem = new SelectListItem();
                listItem.Text = currency.Symbol;
                listItem.Value = currency.Id.ToString();
                listItem.Selected = false;
                selectItems.Add(listItem);
            }
          //  context.Dispose();
            ViewBag.PossibleCurrency = selectItems;
        }





    }
}
