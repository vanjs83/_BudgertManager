using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using _BudgetManager.Models;
using DataAccess;
using BusinessLayer;
using DataAccess.Models;

namespace _BudgetManager.Controllers
{

   
    public class CategoryController : Controller
    {
        // GET: Category
        [AllowAnonymous]
        public ActionResult Index()
        {

            var data = DataLayer.GetViewCategory();
            return View(data);
            //return View();
        }

        // GET: Category/Details/5
        public ActionResult Details(int? id)
        {
            return View();
        }

        // GET: Category/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Category/Create
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Create(Category model)
        {
            
                try
                {
                    if (ModelState.IsValid)

                    {
                         DataLayer.CreateCategory(model);

                        // TODO: Add insert logic here

                        return RedirectToAction("Index");
                        //return PartialView("PayView");
                }
             
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Category/Edit/5
     
        public ActionResult Edit(int? id)
        {

            //Kreiranje konteksta 
            if (id.HasValue && ModelState.IsValid)
            {

                var model = DataLayer.GetCategoryId(id);

                return View(model);
            }

            else
            {
                return View();
            }


        }

        // POST: Category/Edit/5
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id, Category model)
        {
            try
            {

                bool didUpdateModelSucceed = DataLayer.EditCategory(id, model);

                if (didUpdateModelSucceed && ModelState.IsValid)
                    return RedirectToAction("Index");
                else
                return View(model);
            }
            catch
            {
                return View();
            }
        }

        // GET: Category/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            return View();
        }



        // POST: Category/Delete/5
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public JsonResult Delete(int? id)
        {


                bool delete = DataLayer.DeleteCategory(id);
                if(delete)
                return Json("OK", JsonRequestBehavior.AllowGet);
            
            else   
            return Json(false, JsonRequestBehavior.AllowGet);
        }
    }
}
