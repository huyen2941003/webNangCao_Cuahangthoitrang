using Baocao_chuyende.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
        public ActionResult Index(int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);

            List<ContactU> contactUs = db.ContactUs.OrderBy(c => c.isCheck == true ? 1 : 0).ToList();
            IPagedList<ContactU> paged = contactUs.ToPagedList(pageNumber, pageSize);

            return View(paged);
        }

        public ActionResult Delete(int id)
        {
            var news = db.ContactUs.Find(id);
            if (news != null)
            {
                db.ContactUs.Remove(news);
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
                    if (int.TryParse(id, out int categoryId))
                    {
                        var category = db.ContactUs.Find(categoryId);
                        if (category != null)
                        {
                            db.ContactUs.Remove(category);
                        }
                    }
                }
                db.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
        [HttpPost]
        public ActionResult IsActive(int id)
        {
            var video = db.ContactUs.Find(id);
            if (video != null)
            {
                video.isCheck = !video.isCheck;
                db.Entry(video).State = EntityState.Modified;
                db.SaveChanges();
                return Json(new { success = true, isActive = video.isCheck });
            }
            return Json(new { success = false });
        }
    }
}