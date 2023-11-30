using Baocao_chuyende.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace Baocao_chuyende.Areas.Admin.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {
        Web_NangcaoEntities db = new Web_NangcaoEntities();
        // GET: Admin/Category
        public ActionResult Index(int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);

            List<Category> categories = db.Categories.ToList();
            IPagedList<Category> paged = categories.ToPagedList(pageNumber, pageSize);

            return View(paged);
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
                return RedirectToAction("Index");
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
                return RedirectToAction("Index");
            }

            return View(category);
        }
        //public ActionResult Delete(int id)
        //{
        //    var category = db.Categories.Find(id);

        //    if (category == null)
        //    {
        //        return HttpNotFound();
        //    }

        //    return View(category);
        //}
        //[HttpPost]
        //[ActionName("Delete")]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    var category = db.Categories.Find(id);

        //    if (category == null)
        //    {
        //        return HttpNotFound();
        //    }

        //    db.Categories.Remove(category);
        //    db.SaveChanges();
        //    return RedirectToAction("Category");
        //}
        public ActionResult Delete(int id)
        {
            var news = db.Categories.Find(id);
            if (news != null)
            {
                db.Categories.Remove(news);
                db.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }

        [HttpPost]
        public ActionResult DeleteAll(string ids)
        {
            if (!string.IsNullOrEmpty(ids))
            {
                var items = ids.Split(',');
                foreach (var id in items)
                {
                    if (int.TryParse(id, out int categoryId))
                    {
                        var category = db.Categories.Find(categoryId);
                        if (category != null)
                        {
                            db.Categories.Remove(category);
                        }
                    }
                }
                db.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }

    }
}