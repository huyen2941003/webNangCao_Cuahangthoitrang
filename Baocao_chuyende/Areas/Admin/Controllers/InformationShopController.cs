using Baocao_chuyende.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Baocao_chuyende.Areas.Admin.Controllers
{
    [Authorize]
    public class InformationShopController : Controller
    {
        Web_NangcaoEntities db = new Web_NangcaoEntities();
        // GET: Admin/InformationShop
        public ActionResult InformationShop()
        {
            List<InformationShop> product = db.InformationShops.ToList();
            return View(product);
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
                return RedirectToAction("InformationShop");
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
                return RedirectToAction("InformationShop");
            }

            return View(InformationShop);
        }
        public ActionResult Delete(int id)
        {
            var InformationShop = db.InformationShops.Find(id);

            if (InformationShop == null)
            {
                return HttpNotFound();
            }

            return View(InformationShop);
        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var InformationShop = db.InformationShops.Find(id);

            if (InformationShop == null)
            {
                return HttpNotFound();
            }

            db.InformationShops.Remove(InformationShop);
            db.SaveChanges();
            return RedirectToAction("InformationShop");
        }
    }
}