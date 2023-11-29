using Baocao_chuyende.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Baocao_chuyende.Areas.Admin.Controllers
{
    [Authorize]
    public class ContactUsController : Controller
    {
        // GET: Admin/ContactUs
        Web_NangcaoEntities db = new Web_NangcaoEntities();
        // GET: Admin/Video
        public ActionResult ContactUs()
        {
            List<ContactU> video = db.ContactUs.ToList();
            return View(video);
        }
        
        public ActionResult Edit(int id)
        {
            var video = db.ContactUs.Find(id);

            if (video == null)
            {
                return HttpNotFound();
            }

            return View(video);
        }

        [HttpPost]
        public ActionResult Edit(ContactU video)
        {
            if (ModelState.IsValid)
            {
                var existingvideo = db.ContactUs.Find(video.id);
                if (existingvideo == null)
                {
                    return HttpNotFound();
                }

                existingvideo.id = video.id;
                existingvideo.nameCustomer = video.nameCustomer;
                existingvideo.email = video.email;
                existingvideo.phone = video.phone;
                existingvideo.textContactUs = video.textContactUs;

                db.SaveChanges();
                return RedirectToAction("ContactUs");
            }

            return View(video);
        }
        public ActionResult Delete(int id)
        {
            var video = db.ContactUs.Find(id);

            if (video == null)
            {
                return HttpNotFound();
            }

            return View(video);
        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var video = db.ContactUs.Find(id);

            if (video == null)
            {
                return HttpNotFound();
            }

            db.ContactUs.Remove(video);
            db.SaveChanges();
            return RedirectToAction("ContactUs");
        }
    }
}