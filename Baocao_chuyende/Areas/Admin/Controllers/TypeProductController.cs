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
    public class TypeProductController : Controller
    {
        Web_NangcaoEntities db = new Web_NangcaoEntities();
        // GET: Admin/TypeProduct
        public ActionResult Index(int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);

            List<TypeProduct> types = db.TypeProducts.ToList();
            IPagedList<TypeProduct> paged = types.ToPagedList(pageNumber, pageSize);

            return View(paged);
        }
        [HttpGet]
        public ActionResult Create()
        {
            var categories = db.Categories.ToList();
            ViewBag.Categories = new SelectList(categories, "id", "nameCategory");
            ViewData["idCategory"] = new SelectList(categories, "id", "nameCategory");
            return View();
        }

        [HttpPost]
        public ActionResult Create(TypeProduct typeProduct)
        {
            if (ModelState.IsValid)
            {
                db.TypeProducts.Add(typeProduct);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            var categories = db.Categories.ToList();
            ViewBag.Categories = new SelectList(categories, "id", "nameCategory");

            return View(typeProduct);
        }
        public ActionResult Edit(int id)
        {
            var typeProduct = db.TypeProducts.Find(id);

            if (typeProduct == null)
            {
                return HttpNotFound();
            }
            var categories = db.Categories.ToList();
            ViewBag.Categories = new SelectList(categories, "id", "nameCategory");
            ViewData["idCategory"] = new SelectList(categories, "id", "nameCategory");

            return View(typeProduct);
        }
        [HttpPost]
        public ActionResult Edit(TypeProduct typeProduct)
        {
            if (ModelState.IsValid)
            {
                var existingTypeProduct = db.TypeProducts.Find(typeProduct.id);
                if (existingTypeProduct == null)
                {
                    return HttpNotFound();
                }

                existingTypeProduct.nameType = typeProduct.nameType;
                existingTypeProduct.idCategory = typeProduct.idCategory;

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            var categories = db.Categories.ToList();
            ViewBag.Categories = new SelectList(categories, "id", "nameCategory");

            return View(typeProduct);
        }
        //public ActionResult Delete(int id)
        //{
        //    var typeProduct = db.TypeProducts.Find(id);

        //    if (typeProduct == null)
        //    {
        //        return HttpNotFound();
        //    }

        //    return View(typeProduct);
        //}
        //[HttpPost]
        //[ActionName("Delete")]
        //public ActionResult DeleteConfirmedType(int id)
        //{
        //    var typeProduct = db.TypeProducts.Find(id);
        //    if (typeProduct == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    db.TypeProducts.Remove(typeProduct);
        //    db.SaveChanges();
        //    return RedirectToAction("TypeProduct");
        //}
        public ActionResult Delete(int id)
        {
            var types = db.TypeProducts.Find(id);
            if (types != null)
            {
                db.TypeProducts.Remove(types);
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
                    if (int.TryParse(id, out int typeId))
                    {
                        var types = db.TypeProducts.Find(typeId);
                        if (types != null)
                        {
                            db.TypeProducts.Remove(types);
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