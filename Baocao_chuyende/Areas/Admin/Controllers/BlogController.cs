using Baocao_chuyende.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace Baocao_chuyende.Areas.Admin.Controllers
{
    [Authorize]
    public class BlogController : Controller
    {
        Web_NangcaoEntities db = new Web_NangcaoEntities();
        // GET: Admin/Blog
        public ActionResult Index(int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);

            List<Blog> blogs = db.Blogs.ToList();
            IPagedList<Blog> paged = blogs.ToPagedList(pageNumber, pageSize);

            return View(paged);
        }

        [HttpGet]
        public ActionResult CreateBlog()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateBlog(Blog blog, HttpPostedFileBase upLoad)
        {
            db.Blogs.Add(blog);
            db.SaveChanges();

            if (upLoad != null && upLoad.ContentLength > 0)
            {
                int id = int.Parse(db.Blogs.ToList().Last().id.ToString());
                string _FileName = "";
                int index = upLoad.FileName.LastIndexOf('.');
                _FileName = "blog" + id.ToString() + upLoad.FileName.Substring(index);
                string _path = Path.Combine(Server.MapPath("~/UpLoad/Blog"), _FileName);
                upLoad.SaveAs(_path);
                Blog images = db.Blogs.FirstOrDefault(x => x.id == id);
                images.imageTitle = _FileName;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        
        public ActionResult EditBlog(int id)
        {
            var blog = db.Blogs.Find(id);

            if (blog == null)
            {
                return HttpNotFound();
            }

            return View(blog);
        }

        [HttpPost]
        public ActionResult EditBlog(Blog blog, HttpPostedFileBase upLoad)
        {
            if (ModelState.IsValid)
            {
                var existingblog = db.Blogs.Find(blog.id);
                if (existingblog == null)
                {
                    return HttpNotFound();
                }
                existingblog.titleBlog = blog.titleBlog;
                existingblog.textBlog = blog.textBlog;
                if (upLoad != null && upLoad.ContentLength > 0)
                {
                    if (!string.IsNullOrEmpty(existingblog.imageTitle))
                    {
                        var imagePath = Path.Combine(Server.MapPath("~/UpLoad/Blog"), existingblog.imageTitle);
                        if (System.IO.File.Exists(imagePath))
                        {
                            System.IO.File.Delete(imagePath);
                        }
                    }
                    string _FileName = "blog" + existingblog.id.ToString() + Path.GetExtension(upLoad.FileName);
                    string _path = Path.Combine(Server.MapPath("~/UpLoad/Blog"), _FileName);
                    upLoad.SaveAs(_path);
                    existingblog.imageTitle = _FileName;
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(blog);
        }
        public ActionResult Delete(int id)
        {
            var blogs = db.Blogs.Find(id);
            if (blogs != null)
            {
                db.Blogs.Remove(blogs);
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
                    if (int.TryParse(id, out int blogtId))
                    {
                        var blogs = db.Blogs.Find(blogtId);
                        if (blogs != null)
                        {
                            db.Blogs.Remove(blogs);
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