﻿using Baocao_chuyende.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Baocao_chuyende.Areas.Admin.Controllers
{
    [Authorize]
    public class BrandsController : Controller
    {
        
        Web_NangcaoEntities db = new Web_NangcaoEntities();
        // GET: Admin/Brands
        public ActionResult Brand()
        {
            List<Brand> brand = db.Brands.ToList();
            return View(brand);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Brand brand, HttpPostedFileBase upLoad)
        {
            db.Brands.Add(brand);
            db.SaveChanges();

            if (upLoad != null && upLoad.ContentLength > 0)
            {
                int id = int.Parse(db.Brands.ToList().Last().id.ToString());
                string _FileName = "";
                int index = upLoad.FileName.LastIndexOf('.');
                _FileName = "brands" + id.ToString() + upLoad.FileName.Substring(index);
                string _path = Path.Combine(Server.MapPath("~/UpLoad/Brands"), _FileName);
                upLoad.SaveAs(_path);
                Brand images = db.Brands.FirstOrDefault(x => x.id == id);
                images.imageBrands = _FileName;
                db.SaveChanges();
            }
            return RedirectToAction("Brand");
        }
        public ActionResult Edit(int id)
        {
            var brand = db.Brands.Find(id);

            if (brand == null)
            {
                return HttpNotFound();
            }

            return View(brand);
        }

        [HttpPost]
        public ActionResult Edit(Brand brand, HttpPostedFileBase upLoad)
        {
            if (ModelState.IsValid)
            {
                var existingBrand = db.Brands.Find(brand.id);
                if (existingBrand == null)
                {
                    return HttpNotFound();
                }
                existingBrand.nameBrands = brand.nameBrands;
                if (upLoad != null && upLoad.ContentLength > 0)
                {
                    if (!string.IsNullOrEmpty(existingBrand.imageBrands))
                    {
                        var imagePath = Path.Combine(Server.MapPath("~/UpLoad/Brands"), existingBrand.imageBrands);
                        if (System.IO.File.Exists(imagePath))
                        {
                            System.IO.File.Delete(imagePath);
                        }
                    }
                    string _FileName = "brands" + existingBrand.id.ToString() + Path.GetExtension(upLoad.FileName);
                    string _path = Path.Combine(Server.MapPath("~/UpLoad/Brands"), _FileName);
                    upLoad.SaveAs(_path);
                    existingBrand.imageBrands = _FileName;
                }
                db.SaveChanges();
                return RedirectToAction("Brand");
            }

            return View(brand);
        }


        public ActionResult Delete(int id)
        {
            var brand = db.Brands.Find(id);

            if (brand == null)
            {
                return HttpNotFound();
            }

            return View(brand);
        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var brand = db.Brands.Find(id);

            if (brand == null)
            {
                return HttpNotFound();
            }

            db.Brands.Remove(brand);
            db.SaveChanges();
            return RedirectToAction("Brand");
        }
    }
}