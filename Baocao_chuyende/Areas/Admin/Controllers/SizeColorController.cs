using Baocao_chuyende.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Baocao_chuyende.Areas.Admin.Controllers
{
    [Authorize]
    public class SizeColorController : Controller
    {
        public class SizeColorViewModel
        {
            public List<Size> Sizes { get; set; }
            public List<Color> Colors { get; set; }
        }

        Web_NangcaoEntities db = new Web_NangcaoEntities();
        public ActionResult Index()
        {
            List<Size> sizes = db.Sizes.ToList();
            List<Color> colors = db.Colors.ToList();

            var viewModel = new SizeColorViewModel
            {
                Sizes = sizes,
                Colors = colors
            };

            return View(viewModel);
        }

        [HttpGet]
        public ActionResult CreateSize()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateSize(Size size)
        {
            if (ModelState.IsValid)
            {
                db.Sizes.Add(size);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(size);
        }
        public ActionResult EditSize(int id)
        {
            var size = db.Sizes.Find(id);

            if (size == null)
            {
                return HttpNotFound();
            }

            return View(size);
        }

        [HttpPost]
        public ActionResult EditSize(Size size)
        {
            if (ModelState.IsValid)
            {
                var existingsize = db.Sizes.Find(size.id);
                if (existingsize == null)
                {
                    return HttpNotFound();
                }

                existingsize.nameSize = size.nameSize;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(size);
        }
        public ActionResult DeleteSize(int id)
        {
            var sizes = db.Sizes.Find(id);
            if (sizes != null)
            {
                db.Sizes.Remove(sizes);
                db.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }

        [HttpGet]
        public ActionResult CreateColor()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateColor(Color color)
        {
            if (ModelState.IsValid)
            {
                db.Colors.Add(color);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(color);
        }
        public ActionResult EditColor(int id)
        {
            var color = db.Colors.Find(id);

            if (color == null)
            {
                return HttpNotFound();
            }

            return View(color);
        }

        [HttpPost]
        public ActionResult EditColor(Color color)
        {
            if (ModelState.IsValid)
            {
                var existingcolor = db.Colors.Find(color.id);
                if (existingcolor == null)
                {
                    return HttpNotFound();
                }

                existingcolor.nameColor = color.nameColor;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(color);
        }
        public ActionResult DeleteColor(int id)
        {
            var colors = db.Colors.Find(id);
            if (colors != null)
            {
                db.Colors.Remove(colors);
                db.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
    }
}