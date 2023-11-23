using Baocao_chuyende.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Baocao_chuyende.Areas.Admin.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {
        Web_NangcaoEntities db = new Web_NangcaoEntities();
        // GET: Admin/Category
        public ActionResult Category()
        {
            List<Category> product = db.Categories.ToList();
            return View(product);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                db.Categories.Add(category);
                db.SaveChanges();
                return RedirectToAction("Category");
            }
            return View(category);
        }
        public ActionResult Edit(int id)
        {
            var category = db.Categories.Find(id);

            if (category == null)
            {
                return HttpNotFound();
            }

            return View(category);
        }

        [HttpPost]
        public ActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                var existingCategory = db.Categories.Find(category.id);
                if (existingCategory == null)
                {
                    return HttpNotFound();
                }

                existingCategory.nameCategory = category.nameCategory;
                db.SaveChanges();
                return RedirectToAction("Category");
            }

            return View(category);
        }
        public ActionResult Delete(int id)
        {
            var category = db.Categories.Find(id);

            if (category == null)
            {
                return HttpNotFound();
            }

            return View(category);
        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var category = db.Categories.Find(id);

            if (category == null)
            {
                return HttpNotFound();
            }

            db.Categories.Remove(category);
            db.SaveChanges();
            return RedirectToAction("Category");
        }
    }
}