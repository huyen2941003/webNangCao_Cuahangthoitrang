using Baocao_chuyende.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using PagedList;
using PagedList.Mvc;
using System.Web.Mvc;
using System.Web.UI;

namespace Baocao_chuyende.Areas.Admin.Controllers
{
    [Authorize]
    public class ProductDetailController : Controller
    {
        Web_NangcaoEntities db = new Web_NangcaoEntities();
        // GET: Admin/ProductDetails
        public ActionResult Index(int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);

            List<ProductDetail> products = db.ProductDetails.ToList();
            IPagedList<ProductDetail> pagedProducts = products.ToPagedList(pageNumber, pageSize);

            return View(pagedProducts);
        }
        [HttpGet]
        public ActionResult Create()
        {
            var product = db.Products.ToList();
            ViewBag.Products = new SelectList(product, "id", "nameProduct");
            ViewData["idProduct"] = new SelectList(product, "id", "nameProduct");

            var size = db.Sizes.ToList();
            ViewBag.Sizes = new SelectList(size, "id", "nameSize");
            ViewData["idSize"] = new SelectList(size, "id", "nameSize");

            var color = db.Colors.ToList();
            ViewBag.Colors = new SelectList(color, "id", "nameColor");
            ViewData["idColor"] = new SelectList(color, "id", "nameColor");
            return View();
        }

        [HttpPost]
        public ActionResult Create(ProductDetail productDetail, HttpPostedFileBase upLoad)
        {
            db.ProductDetails.Add(productDetail);
            db.SaveChanges();

            if (upLoad != null && upLoad.ContentLength > 0)
            {
                int id = int.Parse(db.ProductDetails.ToList().Last().id.ToString());
                string _FileName = "";
                int index = upLoad.FileName.LastIndexOf('.');
                _FileName = "pr" + id.ToString() + upLoad.FileName.Substring(index);
                string _path = Path.Combine(Server.MapPath("~/UpLoad/Product"), _FileName);
                upLoad.SaveAs(_path);
                ProductDetail images = db.ProductDetails.FirstOrDefault(x => x.id == id);
                images.image = _FileName;
                db.SaveChanges();
            }
            var product = db.Products.ToList();
            ViewBag.Products = new SelectList(product, "id", "nameProduct");

            var size = db.Sizes.ToList();
            ViewBag.Sizes = new SelectList(size, "id", "nameSize");

            var color = db.Colors.ToList();
            ViewBag.Colors = new SelectList(color, "id", "nameColor");

            return RedirectToAction("ProductDetail");
        }
        public ActionResult Edit(int id)
        {
            var brand = db.ProductDetails.Find(id);

            var product = db.Products.ToList();
            ViewBag.Products = new SelectList(product, "id", "nameProduct");
            ViewData["idProduct"] = new SelectList(product, "id", "nameProduct");

            var size = db.Sizes.ToList();
            ViewBag.Sizes = new SelectList(size, "id", "nameSize");
            ViewData["idSize"] = new SelectList(size, "id", "nameSize");

            var color = db.Colors.ToList();
            ViewBag.Colors = new SelectList(color, "id", "nameColor");
            ViewData["idColor"] = new SelectList(color, "id", "nameColor");

            if (brand == null)
            {
                return HttpNotFound();
            }

            return View(brand);
        }

        [HttpPost]
        public ActionResult Edit(ProductDetail productDetail, HttpPostedFileBase upLoad)
        {
            if (ModelState.IsValid)
            {
                var existingProductDetail = db.ProductDetails.Find(productDetail.id);
                if (existingProductDetail == null)
                {
                    return HttpNotFound();
                }

                if (upLoad != null && upLoad.ContentLength > 0)
                {
                    if (!string.IsNullOrEmpty(existingProductDetail.image))
                    {
                        var imagePath = Path.Combine(Server.MapPath("~/UpLoad/Product"), existingProductDetail.image);
                        if (System.IO.File.Exists(imagePath))
                        {
                            System.IO.File.Delete(imagePath);
                        }
                    }
                    string _FileName = "pr" + existingProductDetail.id.ToString() + Path.GetExtension(upLoad.FileName);
                    string _path = Path.Combine(Server.MapPath("~/UpLoad/Product"), _FileName);
                    upLoad.SaveAs(_path);
                    existingProductDetail.image = _FileName;
                }
                existingProductDetail.idProduct = productDetail.idProduct;
                existingProductDetail.idColor = productDetail.idColor;
                existingProductDetail.idSize = productDetail.idSize;

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(productDetail);
        }
    }
}