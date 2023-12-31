﻿using Baocao_chuyende.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using System.Net;
using System.Web.UI;

namespace Baocao_chuyende.Areas.Admin.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        Web_NangcaoEntities db = new Web_NangcaoEntities();
        // GET: Admin/Product
        public ActionResult Index(string SearchText, int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);

            IQueryable<Product> products = db.Products;
            if (!string.IsNullOrEmpty(SearchText))
            {
                products = products.Where(x => x.nameProduct.Contains(SearchText));
            }
            IPagedList<Product> pagedList = products.OrderByDescending(x => x.id).ToPagedList(pageNumber, pageSize);

            return View(pagedList);

        }

        [HttpGet]
        public ActionResult Create()
        {
            var categories = db.Categories.ToList();
            ViewBag.Categories = new SelectList(categories, "id", "nameCategory");
            ViewData["idCategory"] = new SelectList(categories, "id", "nameCategory");

            var brands = db.Brands.ToList();
            ViewBag.Brands = new SelectList(brands, "id", "nameBrands");
            ViewData["idBrands"] = new SelectList(brands, "id", "nameBrands");

            var types = db.TypeProducts.ToList();
            ViewBag.TypeProducts = new SelectList(types, "id", "nameType");
            ViewData["idType"] = new SelectList(types, "id", "nameType");

            return View();
        }

        [HttpPost]
        public ActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                product.saleoff = ((product.cost - product.price) / product.cost) * 100;
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            var categories = db.Categories.ToList();
            ViewBag.Categories = new SelectList(categories, "id", "nameCategory");
            var brands = db.Brands.ToList();
            ViewBag.Brands = new SelectList(brands, "id", "nameBrands");
            var type = db.TypeProducts.ToList();
            ViewBag.TypeProducts = new SelectList(type, "id", "nameType");

            return View(product);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var product = db.Products.Find(id);

            if (product == null)
            {
                return HttpNotFound();
            }
            var categories = db.Categories.ToList();
            ViewBag.Categories = new SelectList(categories, "id", "nameCategory");
            ViewData["idCategory"] = new SelectList(categories, "id", "nameCategory");

            var brands = db.Brands.ToList();
            ViewBag.Brands = new SelectList(brands, "id", "nameBrands");
            ViewData["idBrands"] = new SelectList(brands, "id", "nameBrands");

            var types = db.TypeProducts.ToList();
            ViewBag.TypeProducts = new SelectList(types, "id", "nameType");
            ViewData["idType"] = new SelectList(types, "id", "nameType");

            return View(product);
        }

        [HttpPost]
        public ActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                var existingProduct = db.Products.Find(product.id);
                if (existingProduct == null)
                {
                    return HttpNotFound();
                }

                existingProduct.nameProduct = product.nameProduct;
                existingProduct.idBrands = product.idBrands;
                existingProduct.idCategory = product.idCategory;
                existingProduct.idType = product.idType;
                existingProduct.description = product.description;
                existingProduct.cost = product.cost; 
                existingProduct.price = product.price;
                existingProduct.saleoff = ((existingProduct.cost - existingProduct.price) / existingProduct.cost) * 100;
                existingProduct.inventory = product.inventory;

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(product);
        }
        public ActionResult Delete(int id)
        {
            var product = db.Products.Find(id);
            if (product != null)
            {
                db.Products.Remove(product);
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
                    if (int.TryParse(id, out int productId))
                    {
                        var product = db.Products.Find(productId);
                        if (product != null)
                        {
                            db.Products.Remove(product);
                        }
                    }
                }
                db.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }

        public JsonResult GetTypesByCategory(int categoryId)
        {
            var types = db.TypeProducts
                .Where(t => t.idCategory == categoryId)
                .Select(t => new {
                    value = t.id,
                    text = t.nameType
                })
                .ToList();
            return Json(types, JsonRequestBehavior.AllowGet);
        }
    }
}