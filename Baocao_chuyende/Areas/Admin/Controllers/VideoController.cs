using Baocao_chuyende.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Baocao_chuyende.Areas.Admin.Controllers
{
    [Authorize]
    public class VideoController : Controller
    {
        Web_NangcaoEntities db = new Web_NangcaoEntities();
        // GET: Admin/Video
        public ActionResult VideoHome()
        {
            List<VideoHome> video = db.VideoHomes.ToList();
            return View(video);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(VideoHome video)
        {
            if (ModelState.IsValid)
            {
                db.VideoHomes.Add(video);
                db.SaveChanges();
                return RedirectToAction("VideoHome");
            }
            return View(video);
        }
        public ActionResult Edit(int id)
        {
            var video = db.VideoHomes.Find(id);

            if (video == null)
            {
                return HttpNotFound();
            }

            return View(video);
        }

        [HttpPost]
        public ActionResult Edit(VideoHome video)
        {
            if (ModelState.IsValid)
            {
                var existingvideo = db.VideoHomes.Find(video.id);
                if (existingvideo == null)
                {
                    return HttpNotFound();
                }

                existingvideo.text = video.text;
                db.SaveChanges();
                return RedirectToAction("VideoHome");
            }

            return View(video);
        }
        public ActionResult Delete(int id)
        {
            var video = db.VideoHomes.Find(id);

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
            var video = db.VideoHomes.Find(id);

            if (video == null)
            {
                return HttpNotFound();
            }

            db.VideoHomes.Remove(video);
            db.SaveChanges();
            return RedirectToAction("VideoHome");
        }
    }
}