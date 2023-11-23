using Baocao_chuyende.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Baocao_chuyende.Controllers
{
    public class HomeController : Controller
    {
        private Web_NangcaoEntities db = new Web_NangcaoEntities();

        public class IndexViewModel
        {
            public List<Product> Products { get; set; }
            public List<Category> Categories { get; set; }
            public List<Brand> Brands { get; set; }
            public List<ContactU> ContactUs { get; set; }
            public List<Blog> Blogs { get; set; }
            public List<AboutU> AboutUs { get; set; }
            public List<VideoHome> VideoHomes { get; set; }
            public List<ProductDetail> ProductDetails { get; set; }
            public List<Size> Sizes { get; set; }
            public ContactU ContactU { get; set; }
            public List<TypeProduct> TypeProducts { get; set; }
        }
        public ActionResult Index()
        {
            var viewModel = new IndexViewModel
            {
                Products = db.Products.ToList(),
                Categories = db.Categories.ToList(),
                Brands = db.Brands.ToList(),
                AboutUs = db.AboutUs.ToList(),
                VideoHomes = db.VideoHomes.ToList(),
                ContactUs = db.ContactUs.ToList(),
                Blogs = db.Blogs.ToList(),
                ProductDetails = db.ProductDetails.ToList(),
                Sizes = db.Sizes.ToList(),
                TypeProducts = db.TypeProducts.ToList(),
            };
            return View(viewModel);
        }
    }
}