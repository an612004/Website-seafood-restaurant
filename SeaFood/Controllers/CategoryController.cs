using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SeaFood.Models;

namespace SeaFood.Controllers
{
    public class CategoryController : Controller
    {
        DBSeaFoodEntities database = new DBSeaFoodEntities();
        // GET: Category
        public ActionResult Index()
        {
            return View(database.Categories.ToList());
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Category cate)
        {
            try
            {
                database.Categories.Add(cate);
                database.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return Content("Error Create New");
            }
        }
        public ActionResult Details(int id)
        {
            return View(database.Categories.Where(s => s.CategoryID == id).FirstOrDefault());
        }
        public ActionResult Edit(int id)
        {
            return View(database.Categories.Where(s => s.CategoryID == id).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult Edit(int id, Category cate)
        {
            database.Entry(cate).State = System.Data.Entity.EntityState.Modified;
            database.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int id)
        {
            return View(database.Categories.Where(s => s.CategoryID == id).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult Delete(int id, Category cate)
        {
            try
            {
                cate = database.Categories.Where(s => s.CategoryID == id).FirstOrDefault();
                database.Categories.Remove(cate);
                database.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return Content("This data is using in other table, Error Delete");
            }
        }
        public ActionResult Classify()
        {
            var list = database.Categories.ToList();
            ViewBag.listCategory=list;
            return PartialView(list);
        }
    }
}