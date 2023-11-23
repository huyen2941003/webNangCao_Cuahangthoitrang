using Baocao_chuyende.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Baocao_chuyende.Areas.Admin.Controllers
{
    [Authorize]
    public class TypeProductController : Controller
    {
        Web_NangcaoEntities db = new Web_NangcaoEntities();
        // GET: Admin/TypeProduct
        public ActionResult TypeProduct()
        {
            List<TypeProduct> typeProduct = db.TypeProducts.ToList();
            return View(typeProduct);
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
                return RedirectToAction("TypeProduct");
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
                return RedirectToAction("TypeProduct");
            }
            var categories = db.Categories.ToList();
            ViewBag.Categories = new SelectList(categories, "id", "nameCategory");

            return View(typeProduct);
        }
        public ActionResult Delete(int id)
        {
            var typeProduct = db.TypeProducts.Find(id);

            if (typeProduct == null)
            {
                return HttpNotFound();
            }

            return View(typeProduct);
        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirmedType(int id)
        {
            var typeProduct = db.TypeProducts.Find(id);
            if (typeProduct == null)
            {
                return HttpNotFound();
            }
            db.TypeProducts.Remove(typeProduct);
            db.SaveChanges();
            return RedirectToAction("TypeProduct");
        }
    }
}