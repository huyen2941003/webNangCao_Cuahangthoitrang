using Baocao_chuyende.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Baocao_chuyende.Controllers
{
    public class BlogController : Controller
    {
        private Web_NangcaoEntities db = new Web_NangcaoEntities();

        public class BlogViewModel
        {
            public List<Product> Products { get; set; }
            public List<Category> Categories { get; set; }
            public List<Brand> Brands { get; set; }
            public List<Blog> Blogs { get; set; }
            public Blog BlogDetail { get; set; }
            public List<TypeProduct> TypeProducts { get; set; }
        }

        public ActionResult ListBlog()
        {
            var viewModel = new BlogViewModel
            {
                Products = db.Products.ToList(),
                Categories = db.Categories.ToList(),
                Brands = db.Brands.ToList(),
                Blogs = db.Blogs.ToList(),
                TypeProducts= db.TypeProducts.ToList()
            };
            return View(viewModel);
        }
        public ActionResult DetailsBlog(int id)
        {
            var blog = db.Blogs.FirstOrDefault(b => b.id == id);
            if (blog == null)
            {
                return HttpNotFound();
            }
            var viewModel = new BlogViewModel
            {
                Products = db.Products.ToList(),
                Categories = db.Categories.ToList(),
                Brands = db.Brands.ToList(),
                Blogs = db.Blogs.ToList(),
                BlogDetail = blog,
                TypeProducts = db.TypeProducts.ToList(),
            };

            return View(viewModel);
        }
    }
}