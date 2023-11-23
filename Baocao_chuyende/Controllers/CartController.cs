using Baocao_chuyende;
using Baocao_chuyende.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Baocao_chuyende.Controllers
{
    public class CartController : Controller
    {
        private Models.Web_NangcaoEntities db = new Models.Web_NangcaoEntities();

        public class CartViewModel
        {
            public List<Product> Products { get; set; }
            public List<Category> Categories { get; set; }
            public List<Brand> Brands { get; set; }
            public List<TypeProduct> TypeProducts { get; set; }
            public List<ProductDetail> ProductDetails { get; set; }
            public List<Size> Sizes { get; set; }
            public List<Color> Colors { get; set; }
            public List<Cart> Carts { get; set; }

        }
        public ActionResult Cart()
        {
            var viewModel = new CartViewModel
            {
                Products = db.Products.ToList(),
                Categories = db.Categories.ToList(),
                Brands = db.Brands.ToList(),
                TypeProducts = db.TypeProducts.ToList(),
            };
            return View(viewModel);
        }
        public ActionResult AddToCart(int? id)
        {
            if (id != null)
            {
                var objproduct = db.Products.FirstOrDefault(x => x.id == id);
                if (objproduct != null)
                {
                    List<Models.CartModel> lstcard = null;
                    if (Session["Cart"] != null)
                    {
                        lstcard = (List<Models.CartModel>)Session["Cart"];
                    }
                    else
                    {
                        lstcard = new List<Models.CartModel>();
                    }

                    Models.CartModel check = lstcard.FirstOrDefault(m => m.ProductId == id);
                    if (check != null)
                    {
                        check.Quantity = check.Quantity + 1;
                        check.Amount = (double)(check.Quantity * check.ProductDetailss.price);
                    }
                    else
                    {
                        Models.CartModel newCart = new Models.CartModel
                        {
                            ProductId = (int)id,
                            ProductDetailss = objproduct,
                            Quantity = 1,
                            Amount = (double)objproduct.price,
                            Note = "",
                        };
                    }
                    Session["Cart"] = lstcard;
                }
            }
            return RedirectToAction("Cart");
        }
    }
}