using Baocao_chuyende.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Baocao_chuyende.Areas.Admin.Controllers
{
    [Authorize]
    public class BlogController : Controller
    {
        public class BlogViewModel
        {
            public List<Blog> Blogs { get; set; }
            public List<IGrouping<int?, ImageBlog>> GroupedImageBlogs { get; set; }
        }

        Web_NangcaoEntities db = new Web_NangcaoEntities();
        // GET: Admin/Blog
        public ActionResult Blog()
        {
            List<Blog> blogs = db.Blogs.ToList();
            List<ImageBlog> imageBlogs = db.ImageBlogs.ToList();
            var groupedImageBlogs = imageBlogs.GroupBy(imageBlog => imageBlog.idBlog).ToList();
            var viewModel = new BlogViewModel
            {
                Blogs = blogs,
                GroupedImageBlogs = groupedImageBlogs
            };
            return View(viewModel);
        }


        //Blog

        [HttpGet]
        public ActionResult CreateBlog()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateBlog(Blog blog)
        {
            if (ModelState.IsValid)
            {
                db.Blogs.Add(blog);
                db.SaveChanges();
                return RedirectToAction("Blog");
            }
            return View(blog);
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
        public ActionResult EditBlog(Blog blog)
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
                existingblog.imageTitle = blog.imageTitle;

                db.SaveChanges();
                return RedirectToAction("Blog");
            }

            return View(blog);
        }
        public ActionResult DeleteBlog(int id)
        {
            var blogs = db.Blogs.Find(id);

            if (blogs == null)
            {
                return HttpNotFound();
            }

            return View(blogs);
        }
        [HttpPost]
        [ActionName("DeleteBlog")]
        public ActionResult DeleteConfirmedBlog(int id)
        {
            var blogs = db.Blogs.Find(id);

            if (blogs == null)
            {
                return HttpNotFound();
            }

            db.Blogs.Remove(blogs);
            db.SaveChanges();
            return RedirectToAction("Blog");
        }



        //BlogDetails


    }
}