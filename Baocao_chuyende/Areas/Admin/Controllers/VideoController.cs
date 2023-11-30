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
    public class VideoController : Controller
    {
        Web_NangcaoEntities db = new Web_NangcaoEntities();
        // GET: Admin/Video
        public ActionResult Index(int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);

            List<VideoHome> videos = db.VideoHomes.ToList();
            IPagedList<VideoHome> paged = videos.ToPagedList(pageNumber, pageSize);

            return View(paged);
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
                return RedirectToAction("Index");
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
                existingvideo.urlVideo = video.urlVideo;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(video);
        }
        public ActionResult Delete(int id)
        {
            var videos = db.VideoHomes.Find(id);
            if (videos != null)
            {
                db.VideoHomes.Remove(videos);
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
                    if (int.TryParse(id, out int brandId))
                    {
                        var videos = db.VideoHomes.Find(brandId);
                        if (videos != null)
                        {
                            db.VideoHomes.Remove(videos);
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