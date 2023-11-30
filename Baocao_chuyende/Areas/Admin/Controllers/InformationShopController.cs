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
    public class InformationShopController : Controller
    {
        Web_NangcaoEntities db = new Web_NangcaoEntities();
        // GET: Admin/InformationShop
        public ActionResult Index(int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            List<InformationShop> info = db.InformationShops.ToList();
            IPagedList<InformationShop> paged = info.ToPagedList(pageNumber, pageSize);

            return View(paged);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(InformationShop InformationShop)
        {
            if (ModelState.IsValid)
            {
                db.InformationShops.Add(InformationShop);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(InformationShop);
        }
        public ActionResult Edit(int id)
        {
            var InformationShop = db.InformationShops.Find(id);

            if (InformationShop == null)
            {
                return HttpNotFound();
            }

            return View(InformationShop);
        }

        [HttpPost]
        public ActionResult Edit(InformationShop InformationShop)
        {
            if (ModelState.IsValid)
            {
                var existingInformationShop = db.InformationShops.Find(InformationShop.id);
                if (existingInformationShop == null)
                {
                    return HttpNotFound();
                }

                existingInformationShop.address = InformationShop.address;
                existingInformationShop.zalo = InformationShop.zalo;
                existingInformationShop.hotline = InformationShop.hotline;
                existingInformationShop.wholesale = InformationShop.wholesale;

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(InformationShop);
        }

        public ActionResult Delete(int id)
        {
            var info = db.InformationShops.Find(id);
            if (info != null)
            {
                db.InformationShops.Remove(info);
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
                    if (int.TryParse(id, out int infoId))
                    {
                        var info = db.InformationShops.Find(infoId);
                        if (info != null)
                        {
                            db.InformationShops.Remove(info);
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