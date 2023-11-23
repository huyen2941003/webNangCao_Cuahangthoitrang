using Baocao_chuyende.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Baocao_chuyende.Controllers
{
    public class ProductController : Controller
    {
        private Web_NangcaoEntities db = new Web_NangcaoEntities();
        // GET: Product
        public class ProductViewModel
        {
            public List<Product> Products { get; set; }
            public Product ProductDetail { get; set; }
            public List<Category> Categories { get; set; }
            public List<Brand> Brands { get; set; }
            public List<ProductDetail> ProductDetails { get; set; }
            public List<ProductDetail> ImageProduct { get; set; }
            public ProductDetail ProductList { get; set; }
            public List<Size> Sizes { get; set; }
            public List<Color> Colors { get; set; }
            public List<TypeProduct> TypeProducts { get; set; }
        }
        public ActionResult Index()
        {
            var viewModel = new ProductViewModel
            {
                Products = db.Products.ToList(),
                Categories = db.Categories.ToList(),
                Brands = db.Brands.ToList(),
                ProductDetails = db.ProductDetails.ToList(),
                Sizes = db.Sizes.ToList(),
                TypeProducts = db.TypeProducts.ToList(),
            };
            return View(viewModel);
        }
        public ActionResult DetailsProduct(int id)
        {
            var product = db.Products.FirstOrDefault(p => p.id == id);
            var productDetails = db.ProductDetails.Where(pd => pd.idProduct == id).ToList();

            if (product == null)
            {
                return HttpNotFound();
            }

            var viewModel = new ProductViewModel
            {
                Categories = db.Categories.ToList(),
                Brands = db.Brands.ToList(),
                Products = db.Products.ToList(),
                ImageProduct =db.ProductDetails.ToList(),
                ProductDetails = productDetails,
                ProductDetail = product,
                Sizes = db.Sizes.ToList(),
                Colors = db.Colors.ToList(),
                TypeProducts = db.TypeProducts.ToList(),
            };

            return View(viewModel);
        }
        public ActionResult Search(string search)
        {
            var viewModel = new ProductViewModel
            {
                Categories = db.Categories.ToList(),
                Brands = db.Brands.ToList(),
                ProductDetails = db.ProductDetails.ToList(),
                Sizes = db.Sizes.ToList(),
                TypeProducts = db.TypeProducts.ToList(),
            };

            if (!string.IsNullOrEmpty(search))
            {
                viewModel.Products = db.Products
                    .Where(p => p.nameProduct.Contains(search))
                    .ToList();
            }
            else
            {
                viewModel.Products = db.Products.ToList();
            }
            return View("Index", viewModel);
        }
        public ActionResult SearchByBrand(string brandName)
        {
            var viewModel = new ProductViewModel
            {
                Categories = db.Categories.ToList(),
                Brands = db.Brands.ToList(),
                ProductDetails = db.ProductDetails.ToList(),
                Sizes = db.Sizes.ToList(),
                TypeProducts = db.TypeProducts.ToList(),
            };

            if (!string.IsNullOrEmpty(brandName))
            {
                viewModel.Products = db.Products
                    .Where(p => p.Brand.nameBrands == brandName)
                    .ToList();
            }
            else
            {
                viewModel.Products = db.Products.ToList();
            }
            return View("Index", viewModel);
        }
        public ActionResult SearchByCategory(string categoryName)
        {
            var viewModel = new ProductViewModel
            {
                Categories = db.Categories.ToList(),
                Brands = db.Brands.ToList(),
                ProductDetails = db.ProductDetails.ToList(),
                Sizes = db.Sizes.ToList(),
                TypeProducts = db.TypeProducts.ToList(),
            };

            if (!string.IsNullOrEmpty(categoryName))
            {
                viewModel.Products = db.Products
                    .Where(p => p.Category.nameCategory == categoryName)
                    .ToList();
            }
            else
            {
                viewModel.Products = db.Products.ToList();
            }
            return View("Index", viewModel);
        }
        public ActionResult SearchByCategoryAndType(int categoryId, int typeProductId)
        {
            var viewModel = new ProductViewModel
            {
                Categories = db.Categories.ToList(),
                Brands = db.Brands.ToList(),
                ProductDetails = db.ProductDetails.ToList(),
                Sizes = db.Sizes.ToList(),
                TypeProducts = db.TypeProducts.ToList(),
            };

            if (categoryId != 0 && typeProductId != 0)
            {
                viewModel.Products = db.Products
                    .Where(p => p.idCategory == categoryId && p.idType == typeProductId)
                    .ToList();
            }
            else
            {
                viewModel.Products = db.Products.ToList();
            }
            return View("Index", viewModel);
        }

    }
}